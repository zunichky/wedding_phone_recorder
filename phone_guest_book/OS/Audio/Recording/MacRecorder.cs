namespace phone_guest_book.OS.Audio.Recording
{
    internal class MacRecorder : UnixRecorderBase
    {
        protected override string GetBashCommand(string fileName)
        {
            return $"ffmpeg -f avfoundation -i \":1\" {fileName}";
        }
    }
}
