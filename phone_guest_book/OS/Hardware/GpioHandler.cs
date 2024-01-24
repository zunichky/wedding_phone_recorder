namespace phone_guest_book.OS.Hardware;
using System.Device.Gpio;

public class GpioHandler
{
    private PinEvent _currentStatus = PinEvent.None;
    private GpioController controller;
    
    public GpioHandler(int pin)
    {
        // TODO: close the controller
        controller = new GpioController();
        controller.OpenPin(pin, PinMode.InputPullDown);
        controller.RegisterCallbackForPinValueChangedEvent(
            pin,
            PinEventTypes.Falling | PinEventTypes.Rising,
            OnPinEvent);
    }
    public PinEvent GetCurrentStatus()
    {
        return _currentStatus;
    }
    
    private void OnPinEvent(object sender, PinValueChangedEventArgs args)
    {
        _currentStatus = (PinEvent)args.ChangeType;
    }
    
}
public enum PinEvent
{
    None = 0,
    Rising = 1, 
    Falling = 2,
}