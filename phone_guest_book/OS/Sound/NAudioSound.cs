using NAudio.Wave;

namespace phone_guest_book.os.sound;


public class NAudioSound(string path) : ISound
{
    public event EventHandler? SoundFinished;
    private WaveOutEvent? _outputManager { get; set; }
    private AudioFileReader? _audioFile { get; set; }
    private string _path { get; set; } = path;

    public void Play()
    {
        // TODO: Error Checking
        _audioFile = new AudioFileReader(_path);
        _outputManager = new WaveOutEvent();
        _outputManager.PlaybackStopped += OnPlaybackStopped;
        _outputManager.Init(_audioFile);
        _outputManager.Play();
        
        /*while (_outputManager.PlaybackState == PlaybackState.Playing)
        {
            Thread.Sleep(1000);
        }*/
    }
    private void OnPlaybackStopped(object? sender, StoppedEventArgs e)
    {
        _audioFile?.Close();
        _outputManager?.Dispose();
        SoundFinished?.Invoke(sender: this, e);  
    }
    public bool IsPlaying()
    {
        return _outputManager is { PlaybackState: PlaybackState.Playing };
    }

    public void Stop()
    {
        if (IsPlaying())
        {
            _outputManager?.Stop();
        }
    }
}