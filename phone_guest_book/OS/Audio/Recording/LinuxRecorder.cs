namespace phone_guest_book.OS.Audio.Recording
{
    internal class LinuxRecorder : UnixRecorderBase
    {
        protected override string GetBashCommand(string fileName)
        {
            // libcamera-vid --codec libav --vflip --width 1920 --height 1080 --bitrate 7000000 --libav-audio --audio-device alsa_input.usb-Logitech_Logi_USB_Headset-00.mono-fallback -o video.mp4
            //string command = $"arecord -vv --format=cd ";
            string command =  "libcamera-vid --codec libav --vflip --width 1920 --height 1080 --bitrate 7000000 --libav-audio --audio-device alsa_input.usb-Logitech_Logi_USB_Headset-00.mono-fallback -o ";
            if (Path.GetExtension(fileName).ToLower().Equals(".mp3"))
            {
                command += "--file-type raw | lame -r - ";
            }

            return command + fileName;
        }
    }
}
