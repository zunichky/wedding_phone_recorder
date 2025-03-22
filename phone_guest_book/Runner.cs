using phone_guest_book.os.audio;
using phone_guest_book.OS.Audio;
using phone_guest_book.OS.Audio.Sound;
using phone_guest_book.Utilities;

namespace phone_guest_book;
using phone_guest_book.OS.Hardware;


public class Runner
{
    private PinEvent PREVIOUS_PIN_STATUS = PinEvent.None;
    private GpioHandler? handler;
    private SoundManger _soundManager = new();
    private DirectoryInfo? _dataDirectory;

    public async void Start()
    {
        handler = new GpioHandler(6);

        var usbDrive = FileUtil.FindUsbDrive("telephone");
        if (usbDrive != null)
        {
            _dataDirectory = usbDrive.RootDirectory;
            Console.WriteLine("Using USB");
        }
        else
        {
            _dataDirectory = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
        }

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
        _soundManager.PlaySound(Path.Combine(_dataDirectory.FullName, "sounds/welcome.wav"), 60);
    }

    private async Task StartRecording()
    {
        //var homeDirectory = Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile);
        var finalFolder = Path.Combine(_dataDirectory.FullName, "recordings");
        Directory.CreateDirectory(finalFolder);
        var fullPath = Path.Combine(finalFolder, Utilities.Utilities.GetUniqueFileName(finalFolder) + ".mp4");

        await _soundManager.NewRecording(fullPath, 80);
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