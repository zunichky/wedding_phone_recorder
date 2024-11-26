namespace phone_guest_book.Utilities
{
    internal static class FileUtil
    {
        private const string TempDirName = "temp";

        public static string CheckFileToPlay(string originalFileName)
        {
            var fileNameToReturn = originalFileName;
            if (originalFileName.Contains(" "))
            {
                Directory.CreateDirectory(TempDirName);
                fileNameToReturn = TempDirName + Path.DirectorySeparatorChar +
                    Path.GetFileName(originalFileName).Replace(" ", "");
                File.Copy(originalFileName, fileNameToReturn);
            }

            return fileNameToReturn;
        }

        public static void ClearTempFiles()
        {
            if (Directory.Exists(TempDirName))
                Directory.Delete(TempDirName, true);
        }

        public static DriveInfo? FindUsbDrive(string usbName = "")
        {
            string[] drives = Environment.GetLogicalDrives();
            foreach (var drive in drives)
            {
                try
                {
                    DriveInfo driveInfo = new DriveInfo(drive);
                    if (driveInfo.VolumeLabel == usbName)
                    {
                        return driveInfo;
                    }
                }
                catch
                {
                    Console.WriteLine("Exception on drive info");
                }
            }
            return null;
        }
    }
}
