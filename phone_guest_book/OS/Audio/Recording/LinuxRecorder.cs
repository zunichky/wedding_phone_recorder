﻿namespace phone_guest_book.OS.Audio.Recording
{
    internal class LinuxRecorder : UnixRecorderBase
    {
        protected override string GetBashCommand(string fileName)
        {
            string command = $"arecord --device=\"hw:1,0\" -vv --format=cd ";

            if (Path.GetExtension(fileName).ToLower().Equals(".mp3"))
            {
                command += "--file-type raw | lame -r - ";
            }

            return command += fileName;
        }
    }
}
