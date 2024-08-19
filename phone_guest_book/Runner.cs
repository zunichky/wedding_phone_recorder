using phone_guest_book.os.audio;
using phone_guest_book.OS.Audio;
using phone_guest_book.OS.Audio.Sound;

namespace phone_guest_book;
using phone_guest_book.OS.Hardware;


public class Runner
{
    private PinEvent PREVIOUS_PIN_STATUS = PinEvent.None;
    private GpioHandler? handler;
    private SoundManger _soundManager = new SoundManger();

    public async void Start()
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
                await StartRecording();
                //record_sound()
            }
            // When a person ends the phone call
            else
            {
                Console.WriteLine(formattedDateTime + " Phone Ended");
                await Cleanup();
                await Reset();
            }

            PREVIOUS_PIN_STATUS = currentPinState;
        }
    }

    private void PlayWelcomeSound()
    { 
        _soundManager.PlaySound("sounds/welcome.wav", 50);
    }

    private async Task StartRecording()
    {
        var homeDirectory = Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile);
        var finalFolder = Path.Combine(homeDirectory, "Recordings");
        Directory.CreateDirectory(finalFolder);
        var fullPath = Path.Combine(finalFolder, Utilities.Utilities.GetUniqueFileName(finalFolder) + ".mp4");

        await _soundManager.NewRecording(fullPath, 75);
    }

    private async Task Cleanup()
    {
        await _soundManager.StopAllRecordings();
    }
    private async Task Reset()
    { 
        await _soundManager.StopPlaying();
    }
}