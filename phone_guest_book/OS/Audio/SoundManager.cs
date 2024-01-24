using phone_guest_book.OS.Audio.Recording;
using phone_guest_book.OS.Audio.Sound;

namespace phone_guest_book.os.audio;
/*
 *Manager for sounds/recordings
 */

public class SoundManger
{
    private List<ISound> _currentSounds { get; set; } = [];
    private List<IRecording> _currentRecordings { get; set; } = [];
    //TODO: Settings (volume, etc)
    public void PlaySound(NAudioSound sound)
    {
        if (!sound.IsPlaying())
        {
            sound.Play();
            sound.SoundFinished += OnPlaybackStopped;
            _currentSounds.Add(sound);
        }
    }

    public void StopPlaying()
    {
        foreach (var sound in _currentSounds)
        {
            if (sound.IsPlaying())
                sound.Stop();
        }
    }
    
    public void StopPlaying(ISound sound)
    {   
        if (sound.IsPlaying()) 
            sound.Stop();
    }

    private void OnPlaybackStopped(object? sender, EventArgs e)
    {
        // Remove Sound from active list
        // RemoveSound(e.soundObgject)
    }
    
    /// <summary>
    /// Will return true if any sound is playing
    /// </summary>
    /// <returns></returns>
    public bool IsSoundPlaying()
    {
        return _currentSounds.Any(curSound => curSound.IsPlaying());
    }
    
    /// <summary>
    /// Will return true if the given sound is playing
    /// </summary>
    /// <returns></returns>
    public bool IsSoundPlaying(ISound curSound)
    {
        return curSound.IsPlaying();
    }

    public NAudioRecording NewRecording(string path)
    {
        var recording = new NAudioRecording(path);
        _currentRecordings.Add(recording);
        return recording;
    }
    public void StartRecording(IRecording recording)
    {
        recording.Start();
    }

    public void StopRecording(IRecording recording)
    {
        recording.Stop();
        _currentRecordings.Remove(recording);
    }

    public bool IsRecordingActive()
    {
        return _currentRecordings.Any(curRecording => curRecording.IsRecording());
    }
    
    private void OnRecordingStopped(object? sender, EventArgs e)
    {
        // Remove Sound from active list
        // RemoveSound(e.soundObgject)
    }
}