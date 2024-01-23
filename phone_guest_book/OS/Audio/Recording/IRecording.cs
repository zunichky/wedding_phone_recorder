namespace phone_guest_book.OS.Audio.Recording;

public interface IRecording
{
    void Start();
    void Stop();
    bool IsRecording();
    public event EventHandler? RecordingFinished;
}