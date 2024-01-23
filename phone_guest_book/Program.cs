using phone_guest_book.os.audio;

SoundManger soundManger = new SoundManger();
var welcomeSound = soundManger.PlaySound($"sounds/welcome.wav");

var recording = soundManger.NewRecording(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "NAudio"));
soundManger.StartRecording(recording);

// TODO: Convert these to tests
Thread.Sleep(200);
if (!soundManger.IsSoundPlaying(welcomeSound)) Console.WriteLine("Error: Audio finished too soon");
Thread.Sleep(200);
if (!soundManger.IsSoundPlaying()) Console.WriteLine("Error: Audio finished too soon");
Thread.Sleep(1000);
welcomeSound.Pause();
Thread.Sleep(50);
if (soundManger.IsSoundPlaying()) Console.WriteLine("Error: Audio shouldn't be playing");
Thread.Sleep(2000);
welcomeSound.Resume();
Thread.Sleep(50);
if (!soundManger.IsSoundPlaying()) Console.WriteLine("Error: Audio finished too soon");

while(soundManger.IsSoundPlaying())
{
    Thread.Sleep(500);

}

// Keep recording for another 10 seconds for testing purposes
Thread.Sleep(10000);
soundManger.StopRecording(recording);

