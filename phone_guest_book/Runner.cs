namespace phone_guest_book;
using phone_guest_book.OS.Hardware;


public class Runner
{
    private PinEvent PREVIOUS_PIN_STATUS = PinEvent.None;
    private GpioHandler handler;
    private DateTime _lastInterrupt = DateTime.Now;
    
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

            DateTime currentDateTime = DateTime.Now;
            string formattedDateTime = currentDateTime.ToString("HH:mm:ss");
            // When a person picks up the phone
            if (currentPinState == PinEvent.Rising)
            {
                Console.WriteLine(formattedDateTime + " Phone Picked Up");
                //play_welcome_sound()
                //record_sound()
            }
            // When a person ends the phone call
            else
            {
                Console.WriteLine(formattedDateTime + " Phone Ended");
                //cleanup()
                //reset()
            }

            PREVIOUS_PIN_STATUS = currentPinState;
        }
    }
}