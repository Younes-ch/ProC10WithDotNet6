using System.Net;
using System.Text;

var theEBook = "";
_ = GetBookAsync();

Console.WriteLine("Downloading book...");
Console.ReadLine();

async Task GetBookAsync()
{
    HttpClient client = new();
    theEBook = await client.GetStringAsync("https://www.gutenberg.org/files/1342/1342-0.txt");
    Console.WriteLine("Download complete.");
    GetStats();
}

void GetStats()
{
    var words = theEBook.Split([' ', '\u000A', ',', '.', ';', ':', '-', '?', '/'], StringSplitOptions.RemoveEmptyEntries);
    string[] tenMostCommon = null;
    var longestWord = string.Empty;

    Parallel.Invoke(() =>
    {
        tenMostCommon = FindTenMostCommon(words);
    },
    () =>
    {
        longestWord = FindLongestWord(words);
    });

    StringBuilder bookStats = new("Ten Most Common Words:\n");
    foreach (var word in tenMostCommon)
    {
        bookStats.AppendLine(word);
    }
    bookStats.AppendLine($"\nLongest Word: {longestWord}");
    Console.WriteLine(bookStats.ToString(), "Book Info");
}

string FindLongestWord(string[] words)
{
    return words.OrderByDescending(w => w.Length).FirstOrDefault() ?? string.Empty;
}

string[] FindTenMostCommon(string[] words)
{
    return words.Where(w => w.Length > 6)
        .GroupBy(w => w)
        .Select(g => new { Word = g.Key, Count = g.Count() })
        .OrderByDescending(g => g.Count)
        .Take(10)
        .Select(g => $"{g.Word} ({g.Count})")
        .ToArray();
}