namespace datafetch.progress
{
    public class NullProgress : IProgress
    {
        public void StartTask(string name, int maxTicks)
        {
        }

        public void Progress(string message)
        {
        }
    }
}