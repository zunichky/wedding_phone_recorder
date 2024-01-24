using phone_guest_book.OS.Audio.Sound;

namespace Tests.OS.Audio.Sound;

[TestFixture]
[TestOf(typeof(NAudioSound))]
public class NAudioSoundTest
{
    private NAudioSound sound { get; set; }

    [SetUp]
    public void Setup()
    {
        sound = new NAudioSound("sounds/welcome.wav");
    }
    
    [Test]
    public void Initialization()
    {
        Assert.IsFalse(sound.IsPlaying());
    }
    [Test]
    public void PlaySound()
    {
        sound.Play();
        Assert.IsTrue(sound.IsPlaying());
    }
    [Test]
    public void StopSound()
    {
        sound.Play();
        Assert.IsTrue(sound.IsPlaying());
        sound.Stop();
        Assert.IsFalse(sound.IsPlaying());
    }
}