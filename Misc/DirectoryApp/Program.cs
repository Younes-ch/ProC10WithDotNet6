Console.WriteLine("***** Fun with Directory(Info) *****\n");
//DisplayImageFiles();
DisplayDrives();
Console.ReadLine();


static void DisplayImageFiles()
{
    DirectoryInfo dir = new(string.Format(@"C{0}{1}Windows{1}Web{1}Wallpaper", Path.VolumeSeparatorChar, Path.DirectorySeparatorChar));

    // Get all files with a *.jpg extension.
    FileInfo[] imageFiles = dir.GetFiles("*.jpg", SearchOption.AllDirectories);

    // How many were found?
    Console.WriteLine("Found {0} *.jpg files\n", imageFiles.Length);

    // Now print out info for each file.
    foreach (FileInfo f in imageFiles)
    {
        Console.WriteLine("***************************");
        Console.WriteLine("Full name: {0}", f.FullName);
        Console.WriteLine("File name: {0}", f.Name);
        Console.WriteLine("File size: {0}", f.Length);
        Console.WriteLine("Creation: {0}", f.CreationTime);
        Console.WriteLine("Attributes: {0}", f.Attributes);
        Console.WriteLine("***************************\n");
    }
}

static void DisplayDrives()
{
    //string[] drives = Directory.GetLogicalDrives();
    var typedDrives = DriveInfo.GetDrives();

    //foreach(var drive in drives) Console.WriteLine($"--> {drive}");
    foreach(var drive in typedDrives)
    {
        Console.WriteLine("***************************");
        Console.WriteLine("File name: {0}", drive.Name);
        Console.WriteLine("VolumeLabel: {0}", drive.VolumeLabel);
        Console.WriteLine("RootDirectory: {0}", drive.RootDirectory);
        Console.WriteLine("Total size: {0}", drive.TotalSize);
        Console.WriteLine("TotalFreeSpace: {0}", drive.TotalFreeSpace);
        Console.WriteLine("AvailableFreeSpace: {0}", drive.AvailableFreeSpace);
        Console.WriteLine("DriveFormat: {0}", drive.DriveFormat);
        Console.WriteLine("DriveType: {0}", drive.DriveType);
        Console.WriteLine("IsReady: {0}", drive.IsReady);
        Console.WriteLine("***************************\n");
    }
}
