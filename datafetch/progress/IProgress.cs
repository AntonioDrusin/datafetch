namespace datafetch.progress
{
    public interface IProgress
    {
        void StartTask(string name, int maxTicks);
        void Progress(string message);
    }
}