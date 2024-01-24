using phone_guest_book.Utilities;

namespace phone_guest_book.OS.Audio.Sound
{
    internal class MacPlayer : UnixPlayerBase, IPlayer
    {
        protected override string GetBashCommand(string fileName)
        {
            return "afplay";
        }

        public override Task SetVolume(byte percent)
        {
            if (percent > 100)
                throw new ArgumentOutOfRangeException(
                    nameof(percent), "Percent can't exceed 100");

            var tempProcess = BashUtil.StartBashProcess(
                $"osascript -e \"set volume output volume {percent}\"");
            tempProcess.WaitForExit();

            return Task.CompletedTask;
        }
    }
}
