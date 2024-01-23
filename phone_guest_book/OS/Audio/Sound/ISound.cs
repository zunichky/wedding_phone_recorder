namespace phone_guest_book.OS.Audio.Sound;
public interface ISound
{
    void Play();
    void Stop();
    void Pause();
    void Resume();
    bool IsPlaying();

    event EventHandler SoundFinished;
}