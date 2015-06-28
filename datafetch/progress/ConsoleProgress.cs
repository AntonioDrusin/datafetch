using ShellProgressBar;

namespace datafetch.progress
{
    public class ConsoleProgress : IProgress
    {
        private ProgressBar _progressBar;

        public void StartTask(string name, int maxTicks)
        {
            _progressBar = new ProgressBar(maxTicks, name);
        }

        public void Progress(string message)
        {
            _progressBar.Tick(message);
        }
    }
}