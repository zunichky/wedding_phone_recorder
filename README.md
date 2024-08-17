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

## Service
* /etc/systemd/system/telephone.service
* (one time only) - sudo systemctl enable telephone.service 
* Start/Stop: sudo systemctl [start|stop] telephone.service 

## Release
* On Device: dotnet publish -o ~/telephone_publish/
* Cross Compile: dotnet publish --runtime linux-arm64
* Self container: dotnet publish --runtime linux-arm64 --self-contained
