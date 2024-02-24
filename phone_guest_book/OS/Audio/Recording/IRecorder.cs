namespace phone_guest_book.OS.Audio.Recording
{
    public interface IRecorder
    {
        event EventHandler RecordingFinished;

        bool Recording { get; }

        Task Record(string fileName);
        Task Stop();
        Task SetVolume(int percent);

    }
}
