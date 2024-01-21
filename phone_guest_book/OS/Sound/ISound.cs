namespace phone_guest_book.os.sound;

public interface ISound
{
    void Play();
    void Stop();
    bool IsPlaying();

    event EventHandler SoundFinished;
}