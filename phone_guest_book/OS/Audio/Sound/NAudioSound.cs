using NAudio.Wave;

namespace phone_guest_book.OS.Audio.Sound;


public class NAudioSound(string path) : ISound
    {
        public event EventHandler? SoundFinished;
        private WaveOutEvent? CurrentSound { get; set; }
        private AudioFileReader? AudioFile { get; set; }
        private string Path { get; set; } = path;

        public void Play()
        {
            // TODO: Error Checking
            AudioFile = new AudioFileReader(Path);
            CurrentSound = new WaveOutEvent();
            CurrentSound.PlaybackStopped += OnPlaybackStopped;
            CurrentSound.Init(AudioFile);
            CurrentSound.Play();
            Console.WriteLine("Sound: Started");
        }
        private void OnPlaybackStopped(object? sender, StoppedEventArgs e)
        {
            AudioFile?.Close();
            CurrentSound?.Dispose();
            SoundFinished?.Invoke(sender: this, e);
            Console.WriteLine("Sound: Stopped");
        }
        public bool IsPlaying()
        {
            return CurrentSound is { PlaybackState: PlaybackState.Playing };
        }

        public void Pause()
        {
            if (IsPlaying()) CurrentSound?.Pause();
            Console.WriteLine("Sound: Paused");
        }
        
        public void Resume()
        {
            if (!IsPlaying()) CurrentSound?.Play();
            Console.WriteLine("Sound: Resumed");

        }

        public void Stop()
        {
            if (IsPlaying())
            {
                CurrentSound?.Stop();
            }
        }
    }