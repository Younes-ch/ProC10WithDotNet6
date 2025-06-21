Console.WriteLine(" Fun With Async ===>");

var message = await DoWorkAsync();

Console.WriteLine($"0 - {message}");
var message1 = await DoWorkAsync().ConfigureAwait(false);
Console.WriteLine($"1 - {message1}");
Console.ReadLine();

static async Task<string> DoWorkAsync()
{
    return await Task.Run(() =>
    {
        Thread.Sleep(5_000);
        return "Done with work!";
    });
}