namespace phone_guest_book.OS.Audio.Sound
{
    public interface IPlayer
    {
        event EventHandler PlaybackFinished;

        bool Playing { get; }
        bool Paused { get; }

        Task Play(string fileName);
        Task Pause();
        Task Resume();
        Task Stop();
        Task SetVolume(byte percent);
    }
}
