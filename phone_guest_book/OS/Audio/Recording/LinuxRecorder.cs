using phone_guest_book.Utilities;

namespace phone_guest_book.OS.Audio.Recording
{
    internal class LinuxRecorder : UnixRecorderBase
    {
        protected override string GetBashCommand(string fileName)
        {
            //string command = $"arecord -vv --format=cd ";
            string command =  "libcamera-vid -t 0 -c --codec libav -v 0 " +
                              "--vflip --width 1920 --height 1080 --bitrate 7000000 " +
                              "--libav-audio --audio-source alsa --audio-device hw:CARD=Device --audio-channels 1 " +
                              "-o " + fileName;

            if (Path.GetExtension(fileName).ToLower().Equals(".mp3"))
            {
                command += "--file-type raw | lame -r - ";
            }

            return command;
        }
        
        public override Task SetVolume(int percent)
        {
            // Current microphone has a volume of 0 - 65000
            if (percent > 100)
                throw new ArgumentOutOfRangeException(
                    nameof(percent), "Percent can't exceed 100");
            // ReSharper disable once PossibleLossOfFraction
            int scaledPercent = (int)((float)percent / 100 * 65000);
            var tempProcess = BashUtil.StartBashProcess(
                $"pacmd set-source-volume 4 {scaledPercent}");
            tempProcess.WaitForExit();

            return Task.CompletedTask;
        }
    }
}
