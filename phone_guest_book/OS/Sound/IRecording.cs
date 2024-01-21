namespace phone_guest_book.os.sound;

public interface IRecording
{
    void Start(string path);
    void Stop();
    void Save();
}