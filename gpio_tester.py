import RPi.GPIO as GPIO
import pygame

PREVIOUS_PHONE_STATUS=-1
SOUND_PLAYING=None

def stop_sound():
    if ((SOUND_PLAYING is not None) and (SOUND_PLAYING.get_busy())):
        SOUND_PLAYING.stop()
        print("Stopping sound")

def play_welcome_sound():
    global SOUND_PLAYING
    sound = pygame.mixer.Sound('sounds/welcome.wav')
    SOUND_PLAYING = sound.play()

def record_sound():
    print("recording started")

def reset():
    stop_sound()

def cleanup():
    print("save files")

def phone_callback(channel):
    global PREVIOUS_PHONE_STATUS
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
    
    PREVIOUS_PHONE_STATUS = current_phone_status
 
try:
    pygame.mixer.init()
    GPIO.setmode(GPIO.BCM)
    GPIO.setup(6, GPIO.IN, pull_up_down=GPIO.PUD_DOWN)
    GPIO.add_event_detect(6, GPIO.BOTH, callback=phone_callback)
 
    # TODO: Button or other inputs to trigger an exit
    message = input('\nPress any key to exit.\n')
 
finally:
    GPIO.cleanup()
 
print("Goodbye!")