using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Tessa.UI.Cards;
using Tessa.UI.Cards.Controls;

namespace Tessa.Extensions.Default.Client.UI
{
    public abstract class BreadthFirstControlVisitor
    {
        protected abstract void VisitControl(
            IControlViewModel controlViewModel);

        protected abstract void VisitBlock(
            IBlockViewModel blockViewModel);

        public void Visit(
            ICardModel cardModel)
        {
            var queue = new Queue<object>();
            foreach (var block in cardModel.Blocks)
            {
                EnqueueBlock(block.Value, queue);
            }
            foreach (var form in cardModel.Forms)
            {
                EnqueueForm(form, queue);
            }

            this.VisitInternal(queue);
        }

        public void Visit(
            IControlViewModel rootControl)
        {
            var queue = new Queue<object>();
            if (rootControl is TabControlViewModel tabControl)
            {
                EnqueueTabs(tabControl, queue);
            }
            else if (rootControl is ContainerViewModel containerControl)
            {
                EnqueueForm(containerControl.Form, queue);
            }

            this.VisitInternal(queue);
        }

        public void Visit(
            IFormWithBlocksViewModel rootForm)
        {
            var queue = new Queue<object>();
            EnqueueForm(rootForm, queue);
            this.VisitInternal(queue);
        }

        public void Visit(
            IBlockViewModel rootBlock)
        {
            var queue = new Queue<object>();
            EnqueueBlock(rootBlock, queue);

            this.VisitInternal(queue);
        }

        private void VisitInternal(Queue<object> queue)
        {
            while (queue.Count != 0)
            {
                var item = queue.Dequeue();
                if (item is IControlViewModel controlViewModel)
                {
                    this.VisitControl(controlViewModel);

                    if (controlViewModel is TabControlViewModel tabViewModel)
                    {
                        EnqueueTabs(tabViewModel, queue);
                    }
                    else if (controlViewModel is ContainerViewModel containerControl)
                    {
                        EnqueueForm(containerControl.Form, queue);
                    }
                }
                else if (item is IBlockViewModel blockViewModel)
                {
                    this.VisitBlock(blockViewModel);
                    EnqueueBlock(blockViewModel, queue);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void EnqueueTabs(TabControlViewModel tabControl, Queue<object> queue)
        {
            foreach (var tabControlTab in tabControl.Tabs)
            {
                EnqueueForm(tabControlTab, queue);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void EnqueueForm(
            IFormWithBlocksViewModel formViewModel,
            Queue<object> queue)
        {
            foreach (var blockViewModel in formViewModel.Blocks)
            {
                queue.Enqueue(blockViewModel);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void EnqueueBlock(
            IBlockViewModel blockViewModel,
            Queue<object> queue)
        {
            foreach (var controlViewModel in blockViewModel.Controls)
            {
                queue.Enqueue(controlViewModel);
            }
        }


    }
}