using phone_guest_book.os.audio;
using phone_guest_book.OS.Audio.Sound;

namespace Tests.OS.Audio;

[TestFixture]
[TestOf(typeof(SoundManger))]
public class SoundMangerTest
{
    
    [Test]
    public void Initialize()
    {
        SoundManger soundManger = new SoundManger();
        // TODO: Convert to use mock, not NAudioSound
        /*
        var welcomeSound = new NAudioSound($"sounds/welcome.wav");
        soundManger.PlaySound(welcomeSound);

        Assert.That(soundManger.IsSoundPlaying(welcomeSound), Is.True);
        Assert.That(soundManger.IsSoundPlaying(), Is.True);
        welcomeSound.Pause();
        Assert.That(soundManger.IsSoundPlaying(), Is.False);
        welcomeSound.Resume();
        Assert.That(soundManger.IsSoundPlaying(), Is.True);
        */
    }
}