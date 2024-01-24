namespace phone_guest_book.Utilities;

public static class Utilities
{
    /// <summary>
    /// Given a folder path, will increment 1 to the last filename
    /// Ex) folder/0001.txt, folder/0002.txt
    /// Would return 0003
    /// Will start at 0001 if empty
    /// </summary>
    /// <param name="folderPath"></param>
    /// <returns></returns>
    public static string GetUniqueFileName(string folderPath)
    {
        var dirInfo = new DirectoryInfo(folderPath);
        var allFiles = dirInfo.GetFiles("*.*", SearchOption.TopDirectoryOnly);
        var latestFile = allFiles.MaxBy(x => x.FullName);
        var fileName = Path.GetFileNameWithoutExtension(latestFile?.FullName);

        int curFileName = 0;
        try
        {
            if (fileName != null) curFileName = Int32.Parse(fileName) + 1;
        }
        catch (FormatException)
        {
            curFileName = 1;
        }
        
        return curFileName.ToString("D5");
    }
}