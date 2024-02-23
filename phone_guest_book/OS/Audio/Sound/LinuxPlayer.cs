using System.Diagnostics;
using phone_guest_book.Utilities;

namespace phone_guest_book.OS.Audio.Sound
{
    internal class LinuxPlayer : UnixPlayerBase, IPlayer
    {
        protected override string GetBashCommand(string fileName)
        {
            if (Path.GetExtension(fileName).ToLower().Equals(".mp3"))
            {
                return "mpg123 -q";
            }
            else
            {
                return "aplay -q";
            }
        }

        public override Task SetVolume(byte percent)
        {
            if (percent > 100)
                throw new ArgumentOutOfRangeException(
                    nameof(percent), "Percent can't exceed 100");

            var tempProcess = BashUtil.StartBashProcess(
                $"amixer -M set 'Master' {percent}%");
            tempProcess.WaitForExit();

            return Task.CompletedTask;
        }
    }
}
