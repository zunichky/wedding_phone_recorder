using System.Diagnostics;
using phone_guest_book.Utilities;

namespace phone_guest_book.OS.Audio.Recording
{
    internal abstract class UnixRecorderBase : IRecorder
    {
        private Process? _process = null;
        private string _filepath = ""

        public event EventHandler RecordingFinished;

        public bool Recording { get; set; }

        protected abstract string GetBashCommand(string fileName);

        public async Task Record(string filePath)
        {
            _filepath = filePath;
            await Stop();
            var bashToolName = GetBashCommand(filePath);
            _process = BashUtil.StartBashProcess(
                $"{bashToolName} '{filePath}'");
            _process.EnableRaisingEvents = true;
            _process.Exited += HandleRecordingFinished;
            _process.ErrorDataReceived += HandleRecordingFinished;
            _process.Disposed += HandleRecordingFinished;
            Recording = true;
        }

        public Task Stop()
        {
            if (_process != null)
            {
                BashUtil.StartBashProcess("kill -SIGINT " + _process.Id);
                int count = 0;
                
                //wait a total of 2 seconds for the process to finish gracefully
                while(count < 20)
                {
                    if (_process.HasExited)
                    {
                        break;
                    }

                    Thread.Sleep(100);
                    count++;
                }

                _process.Kill();
                _process.Dispose();
                _process = null;
            }

            Recording = false;
            BashUtil.StartBashProcess("sync " + _filepath);

            return Task.CompletedTask;
        }
        
        public abstract Task SetVolume(int percent);

        private void HandleRecordingFinished(object sender, EventArgs e)
        {
            if (Recording)
            {
                Recording = false;
                RecordingFinished?.Invoke(this, e);
            }
        }
    }
}
