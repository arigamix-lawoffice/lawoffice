namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Events
{
    public interface IKrEventPolicy: IExtensionPolicy
    {
        bool IsAllowed(
            string eventType);
    }
}