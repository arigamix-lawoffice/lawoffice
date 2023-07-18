namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow
{
    public interface IKrProcessRunnerProvider
    {
        IKrProcessRunner GetRunner(
            string runnerName);
    }
}