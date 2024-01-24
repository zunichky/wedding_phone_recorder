using phone_guest_book.os.audio;
using phone_guest_book.OS.Audio;
using phone_guest_book.OS.Audio.Sound;

namespace phone_guest_book;
using phone_guest_book.OS.Hardware;


public class Runner
{
    private PinEvent PREVIOUS_PIN_STATUS = PinEvent.None;
    private GpioHandler handler;
    private SoundManger _soundManager = new SoundManger();

    public void Start()
    {
        handler = new GpioHandler(6);

        while (true)
        {
            PinEvent currentPinState = handler.GetCurrentStatus();
            if (PREVIOUS_PIN_STATUS == currentPinState)
            {
                Thread.Sleep(50);
                continue;
            }
            
            string formattedDateTime = DateTime.Now.ToString("HH:mm:ss");
            // When a person picks up the phone
            if (currentPinState == PinEvent.Rising)
            {
                Console.WriteLine(formattedDateTime + " Phone Picked Up");
                PlayWelcomeSound();
                StartRecording();
                //record_sound()
            }
            // When a person ends the phone call
            else
            {
                Console.WriteLine(formattedDateTime + " Phone Ended");
                Cleanup();
                Reset();
            }

            PREVIOUS_PIN_STATUS = currentPinState;
        }
    }

    private void PlayWelcomeSound()
    { 
        _soundManager.PlaySound("sounds/welcome.wav");
    }

    private void StartRecording()
    {
        var homeDirectory = Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile);
        var finalPath = Path.Combine(homeDirectory, "Recordings");
        Directory.CreateDirectory(finalPath);
        _soundManager.NewRecording(Utilities.Utilities.GetUniqueFileName(homeDirectory));
    }

    private void Cleanup()
    {
        _soundManager.StopAllRecordings();
    }
    private void Reset()
    { 
        _soundManager.StopPlaying();
    }
}