using phone_guest_book.OS.Audio.Recording;
using phone_guest_book.OS.Audio.Sound;

namespace phone_guest_book.os.audio;
/*
 *Manager for sounds/recordings
 */

public class SoundManger
{
    private List<ISound> CurrentSounds { get; set; } = [];
    private List<IRecording> CurrentRecordings { get; set; } = [];
    //TODO: Settings (volume, etc)
    public void PlaySound(NAudioSound sound)
    {
        if (!sound.IsPlaying())
        {
            sound.Play();
            sound.SoundFinished += OnPlaybackStopped;
            CurrentSounds.Add(sound);
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
        return CurrentSounds.Any(curSound => curSound.IsPlaying());
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
        CurrentRecordings.Add(recording);
        return recording;
    }
    public void StartRecording(IRecording recording)
    {
        recording.Start();
    }

    public void StopRecording(IRecording recording)
    {
        recording.Stop();
        CurrentRecordings.Remove(recording);
    }

    public bool IsRecordingActive()
    {
        return CurrentRecordings.Any(curRecording => curRecording.IsRecording());
    }
    
    private void OnRecordingStopped(object? sender, EventArgs e)
    {
        // Remove Sound from active list
        // RemoveSound(e.soundObgject)
    }
}