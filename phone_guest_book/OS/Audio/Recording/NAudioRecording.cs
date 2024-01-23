using NAudio.Wave;

namespace phone_guest_book.OS.Audio.Recording;

public class NAudioRecording : IRecording
{
    public event EventHandler? RecordingFinished;
    private string OutputFolder { get; set; }
    private string Filename { get; set; } = "recorded.wav";
    private WaveFileWriter Writer {get; set; } = null!;
    private WaveInEvent WaveIn { get; set; } = null!;
    private short MaxRecordingLength { get; set; } = 120;
    private bool RecordingActive { get; set; }

    public NAudioRecording(string savePath)
    {
        OutputFolder = Path.Combine(savePath);
        Directory.CreateDirectory(OutputFolder);
    }

    public void Start()
    {
        WaveIn = new WaveInEvent();
        Writer = new WaveFileWriter(Path.Combine(OutputFolder, Filename), WaveIn.WaveFormat); 
        WaveIn.StartRecording();
        RecordingActive = true;
        Console.WriteLine("Recording: Started");
        WaveIn.DataAvailable += (s, a) =>
        {
            Writer.Write(a.Buffer, 0, a.BytesRecorded);
            if (Writer.Position > WaveIn.WaveFormat.AverageBytesPerSecond * MaxRecordingLength)
            {
                WaveIn.StopRecording();
            }
        };
        
        WaveIn.RecordingStopped += (s, a) =>
        {
            Console.WriteLine("Recording: Stopped");
            RecordingFinished?.Invoke(this, null!);
            RecordingActive = false;
            Cleanup();
        };
    }
    
    public void Stop()
    {
        WaveIn.StopRecording();
    }

    private void Cleanup()
    {
        Writer.Dispose(); 
        Writer = null!;
        WaveIn.Dispose();
    }

    public bool IsRecording()
    {
        return RecordingActive;
    }
}