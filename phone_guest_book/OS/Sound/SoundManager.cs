namespace phone_guest_book.os.sound;

public class SoundManger
{
    private List<ISound> _currentSounds { get; set; } = [];

    public void PlaySound(ISound sound)
    {
        if (!sound.IsPlaying()) sound.Play();
        sound.SoundFinished += OnPlaybackStopped;
        _currentSounds.Add(sound);
        //TODO: Return Status Code
    }

    public bool RemoveSound(string path)
    {
        return false;
    }
    public bool RemoveSound(ISound sound)
    {
        return false;
    }

    private void OnPlaybackStopped(object? sender, EventArgs e)
    {
        // Remove Sound from active list
        // RemoveSound(e.soundObgject)
    }
    
    public bool IsSoundPlaying()
    {
        return _currentSounds.Any(curSound => curSound.IsPlaying());
    }
}