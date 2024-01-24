namespace phone_guest_book;
using phone_guest_book.OS.Hardware;


public class Runner
{
    private PinEvent PREVIOUS_PIN_STATUS = PinEvent.None;
    
    public void Start()
    {
        GpioHandler gpio = new GpioHandler(6);
    }

    private void stateChange_callback(object? sender, GpioHandler.PinChangeEventArgs e)
    {
        PinEvent currentPinState = e.PinStatus;
        if (PREVIOUS_PIN_STATUS == currentPinState)
        {
            return;
        }

        // When a person picks up the phone
        if (currentPinState == PinEvent.Rising)
        {
            Console.WriteLine("Phone Picked Up");
            //play_welcome_sound()
            //record_sound()
        }
        // When a person ends the phone call
        else
        {
            Console.WriteLine("Phone Ended");
            //cleanup()
            //reset()
        }

        PREVIOUS_PIN_STATUS = currentPinState;

        /*global PREVIOUS_PHONE_STATUS
            current_phone_status = GPIO.input(channel)

        if (PREVIOUS_PHONE_STATUS == current_phone_status):
        return

        # When a person picks up the phone
                if (current_phone_status == GPIO.HIGH):
                print("Phone Picked Up")
                play_welcome_sound()
                record_sound()
        # When a person ends the phone call
                else:
                print("Phone Ended")
                cleanup()
                reset()

                PREVIOUS_PHONE_STATUS = current_phone_status*/
    }
}