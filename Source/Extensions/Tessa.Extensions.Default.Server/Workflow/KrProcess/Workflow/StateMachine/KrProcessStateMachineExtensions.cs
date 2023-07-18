using System;
using System.Collections.Generic;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Scope;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.StateMachine
{
    public static class KrProcessStateMachineExtensions
    {
        private const string ProcessRunnerStateInfoKey = nameof(ProcessRunnerStateInfoKey);

        public static void SetRunnerState(
            this IKrScope krScope,
            Guid processID,
            KrProcessState state)
        {
            GetStateDict(krScope)[processID] = state;
        }

        public static void SetDefaultState(
            this IKrScope krScope,
            Guid processID)
        {
            GetStateDict(krScope).Remove(processID);
        }

        public static KrProcessState GetRunnerState(
            this IKrScope krScope,
            Guid processID)
        {
            return TryGet(krScope, processID);
        }

        public static bool IsDefaultProcessState(
            this IKrScope krScope,
            Guid processID)
        {
            return TryGet(krScope, processID) == KrProcessState.Default;
        }

        private static Dictionary<Guid, KrProcessState> GetStateDict(IKrScope krScope)
        {
            if (!krScope.Info.TryGetValue(ProcessRunnerStateInfoKey, out var stateObj)
                || !(stateObj is Dictionary<Guid, KrProcessState> states))
            {
                states = new Dictionary<Guid, KrProcessState>();
                krScope.Info[ProcessRunnerStateInfoKey] = states;
            }

            return states;
        }

        private static KrProcessState TryGet(
            IKrScope krScope,
            Guid processID)
        {
            var dict = GetStateDict(krScope);
            return dict.TryGetValue(processID, out var state) 
                ? state 
                : KrProcessState.Default;
        }
        
    }
}