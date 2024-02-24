# Wedding Phone Recorder

# Hardware
* Raspberry pi (3b)
* western electric 554 rotary phone 
* microphone

# Setup
Get Audio Device ->  arecord -L  Ex) hw:CARD=Device
Test Recording -> arecord -vv -fdat foo.wav
Test Sound -> speaker-test -t wav -c 2 -l 5
## TODOs
* Folder structure for recordings
* Add Recording
* Error Handling
* Video
* Add pip packages to requirements.txt

## TODOs Linux
* Crash recovery
* Requirements install
