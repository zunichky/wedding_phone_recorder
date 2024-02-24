﻿using phone_guest_book.Utilities;

namespace phone_guest_book.OS.Audio.Recording
{
    internal class WindowsRecorder : IRecorder
    {
        private string _fileName;

        public bool Recording { get; set; }

        public event EventHandler RecordingFinished;

        public async Task Record(string fileName)
        {
            try
            {
                if (Recording)
                {
                    await Stop();
                }

                _fileName = fileName;

                Recording = true;
                await WindowsUtil.ExecuteMciCommand("Close All");
                await WindowsUtil.ExecuteMciCommand("open new Type waveaudio Alias recsound");
                await WindowsUtil.ExecuteMciCommand("set recsound time format ms bitspersample 16 channels 2 samplespersec 48000 bytespersec 192000 alignment 4");
                await WindowsUtil.ExecuteMciCommand("record recsound");
            }
            catch
            {
                HandleRecordingFinished();
                throw;
            }
        }

        public async Task Stop()
        {
            if (!Recording)
                return;

            try
            {
                await WindowsUtil.ExecuteMciCommand($"save recsound {_fileName}");
                await WindowsUtil.ExecuteMciCommand("close recsound");
                
            }
            finally
            {
                HandleRecordingFinished();
            }
        }
        
        public Task SetVolume(int percent)
        {
            throw new NotImplementedException(); 
        }
        private void HandleRecordingFinished()
        {
            if (Recording)
            {
                Recording = false;
                RecordingFinished?.Invoke(this, new EventArgs());
            }
        }

    }
}
