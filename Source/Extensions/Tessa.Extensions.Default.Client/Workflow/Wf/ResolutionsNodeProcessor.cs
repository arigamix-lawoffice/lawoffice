using System;
using System.Linq;
using Tessa.UI.WorkflowViewer.Layouts;
using Tessa.UI.WorkflowViewer.Processors;
using Tessa.UI.WorkflowViewer.Shapes;
using Unity;

namespace Tessa.Extensions.Default.Client.Workflow.Wf
{
    public sealed class ResolutionsNodeProcessor : NodeProcessor
    {
        public ResolutionsNodeProcessor(IUnityContainer container)
            : base(container)
        {
        }

        protected override void PlaceNodes()
        {
            if (Layout.Nodes.Count == 0)
            {
                return;
            }

            INode root = this.Layout.Nodes.First(n => this.Layout.Connections.All(c => c.To != n));
            double tmp;

            //Далее мы будем пытаться минимизировать YIndex, поэтому сейчас мы "обнуляем" его
            //устанавливая равным бесконечности
            foreach (INode node in Layout.Nodes)
            {
                node.YIndex = double.PositiveInfinity;
            }

            SetXIndexes(Layout, root);
            PlaceNodesRecursively(Layout, root, out tmp);

            //Делаем сдвиг сетки если ноды с большими/маленькими предустановленными размерами
            //напр. ромбик, в который уходят все согласования параллельного этапа
            double shift = 0;

            foreach (INode node in Layout.Nodes.OrderBy(x => x.XIndex))
            {
                node.Padding = 15;
                node.Top = 120 + node.YIndex * 100;
                node.Left = 50 + node.XIndex * 320 + shift;

                if (node.Width.Equals(0.0))
                {
                    // Ширина ноды определяется автоматически с учетом размера содержимого
                    //node.Width = 144;
                }
                else
                {
                    //пока что такой простой обработки достаточно
                    shift -= 144 - node.Width;
                }
            }
        }

        //На мой вкус само то
        private const double ChildNodeShift = .7;

        private static void PlaceNodesRecursively(INodeLayout layout, INode node, out double maxChildY, double y = 0.0)
        {
            double shiftIfNeeded = node.IsChildNode
                ? layout.Connections.Any(c1 => c1.To == node //Находим стрелку, входящую в ноду
                    // и у родительской по отношению к данной ноде нет обсчитанных потомков
                    && !layout.Connections.Any(c2 => c2.From == c1.From
                        //&& c2.To != node 
                        && c2.To.YIndex < double.PositiveInfinity)
                    // и у родительской по отношению к данной ноде нет потомков без пометки IsChildNode
                    && !layout.Connections.Any(c2 => c2.From == c1.From
                        && !c2.To.IsChildNode))
                    ? ChildNodeShift
                    : 0
                : 0;

            maxChildY = y + shiftIfNeeded;
            node.YIndex = maxChildY;

            foreach (IConnection connection in layout.Connections.Where(c => c.From == node)
                .OrderBy(f => f.To.IsChildNode))
            {
                INode child = layout.Nodes.First(n => n == connection.To);

                double nextY;
                PlaceNodesRecursively(layout,
                    child,
                    out nextY,
                    maxChildY++);

                //Если дочерняя была вставлена с единичным отступом, то все последующие дочерние
                //должны быть минимум на 1 ед ниже обработанной
                if (Math.Abs((nextY - node.YIndex) - ChildNodeShift) < .001)
                {
                    nextY += 1;
                }
                maxChildY = Math.Max(nextY, maxChildY);
            }
        }

        private static void SetXIndexes(INodeLayout layout, INode node, double x = 0)
        {
            //Положение текущей перебираемой ноды
            //Если нода проходится несколько раз - напр. ромбик-объединение после параллельного согласования 
            //и его дочерние ноды - нужно взять самое правое положение.
            node.XIndex = Math.Max(x, node.XIndex);

            //Для всех дочерних нод сделаем тоже самое
            foreach (IConnection connection in layout.Connections.Where(c => c.From == node))
            {
                INode child = layout.Nodes.First(n => n == connection.To);
                SetXIndexes(layout, child, x + 1);
            }
        }
    }
}
