using System;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    public sealed class KrPreprocessorProvider: IKrPreprocessorProvider
    {
        private readonly Lazy<IKrPreprocessor> procedurePreprocessor = 
            new Lazy<IKrPreprocessor>(() => new KrProcedurePreprocessor());

        /// <summary>
        /// Получить препроцессор для методов с возвращаемым значением
        /// Каждый вызов создает новый объект, т.к. KrFunctionPreprocessor обладает внутренним состоянием.
        /// </summary>
        /// <returns></returns>
        public IKrPreprocessor AcquireFunctionPreprocessor()
        {
            return new KrFunctionPreprocessor();
        }

        /// <summary>
        /// Получить препроцессор для методов без возвращаемых значений
        /// Препроцессоры не имеют своего состояния, поэтому создается лишь один элемент 
        /// </summary>
        /// <returns></returns>
        public IKrPreprocessor AcquireProcedurePreprocessor()
        {
            return this.procedurePreprocessor.Value;
        }
    }
}