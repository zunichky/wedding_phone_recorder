// See https://aka.ms/new-console-template for more information

using phone_guest_book.os.sound;

SoundManger soundManger = new SoundManger();
soundManger.PlaySound(new NAudioSound($"sounds/welcome.wav"));

while (soundManger.IsSoundPlaying())
{
    Console.WriteLine("Waiting for sound to finish");
    Thread.Sleep(500);
}