using System.Text;

Console.WriteLine("***** Fun with FileStreams *****\n");

using (var fs = File.Open("myMessage.dat", FileMode.Create))
{
    string msg = "Hello!";
    var msgAsByteArray = Encoding.UTF8.GetBytes(msg);

    fs.Write(msgAsByteArray, 0, msgAsByteArray.Length);

    fs.Position = 0;

    Console.Write("Your message as an array of bytes: ");
    var bytesFromFile = new byte[msgAsByteArray.Length];
    for (int i = 0; i < msgAsByteArray.Length; i++)
    {
        bytesFromFile[i] = (byte)fs.ReadByte();
        Console.Write(bytesFromFile[i] + " ");
    }

    Console.Write("\nDecoded Message: ");
    Console.WriteLine(Encoding.UTF8.GetString(bytesFromFile));
    Console.ReadLine();
}

File.Delete("myMessage.dat");

