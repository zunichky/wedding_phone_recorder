namespace phone_guest_book.OS.Hardware;
using System.Device.Gpio;

public class GpioHandler
{
    public event EventHandler? PinChangedStatus;
    private PinEvent _previousStatus = PinEvent.None;
    public GpioHandler(int pin)
    {
        using var controller = new GpioController();
        controller.OpenPin(pin, PinMode.InputPullDown);
        controller.RegisterCallbackForPinValueChangedEvent(
            pin,
            PinEventTypes.Falling | PinEventTypes.Rising,
            OnPinEvent);
    }
    
    private void OnPinEvent(object sender, PinValueChangedEventArgs args)
    {
        var pinArgs = new PinChangeEventArgs
        {
            PinStatus = (PinEvent)args.ChangeType
        };
        
        PinChangedStatus?.Invoke(this, pinArgs);
    }
    
    public class PinChangeEventArgs : EventArgs
    {
        public PinEvent PinStatus { get; set; }
    }
    
}
public enum PinEvent
{
    None = 0,
    Rising = 1, 
    Falling = 2,
}