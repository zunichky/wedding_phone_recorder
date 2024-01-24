using phone_guest_book.OS.Audio;
using phone_guest_book.OS.Audio.Recording;
using phone_guest_book.OS.Audio.Sound;

namespace phone_guest_book.os.audio;
/*
 *Manager for sounds/recordings
 */

public class SoundManger
{
    private List<Player> CurrentSounds { get; set; } = [];
    private List<Recorder> CurrentRecordings { get; set; } = [];
    //TODO: Settings (volume, etc)
    public void PlaySound(string fileName)
    {
        Player curSound = new Player();
        if (!curSound.Playing)
        {
            curSound.Play(fileName);
            curSound.PlaybackFinished += OnPlaybackStopped;
            CurrentSounds.Add(curSound);
        }
    }

    public void StopPlaying()
    {
        foreach (var sound in CurrentSounds)
        {
            if (sound.Playing)
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
        // RemoveSound(e.soundObject)
    }
    
    /// <summary>
    /// Will return true if any sound is playing
    /// </summary>
    /// <returns></returns>
    public bool IsSoundPlaying()
    {
        return CurrentSounds.Any(curSound => curSound.Playing);
    }
    
    /// <summary>
    /// Will return true if the given sound is playing
    /// </summary>
    /// <returns></returns>
    public bool IsSoundPlaying(ISound curSound)
    {
        return curSound.IsPlaying();
    }

    public Recorder NewRecording(string path)
    {
        var recording = new Recorder();
        recording.Record(path);
        CurrentRecordings.Add(recording);
        return recording;
    }
    public void StartRecording(IRecording recording)
    {
        recording.Start();
    }

    public void StopRecording(Recorder recording)
    {
        recording.Stop();
        CurrentRecordings.Remove(recording);
    }

    public void StopAllRecordings()
    {
        foreach (var recording in CurrentRecordings)
        {
            if (recording.Recording)
                recording.Stop();
        }
    }

    public bool IsRecordingActive()
    {
        return CurrentRecordings.Any(curRecording => curRecording.Recording);
    }
    
    private void OnRecordingStopped(object? sender, EventArgs e)
    {
        // Remove Sound from active list
        // RemoveSound(e.soundObject)
    }
}