using System.Data.Common;
using System.Data.Odbc;
#if PC
using System.Data.OleDb;
#endif
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using DataProviderFactory;

Console.WriteLine("***** Fun with Data Provider Factories *****\n");

var (provider, connectionString) = GetProviderFromConfiguration();
DbProviderFactory factory = GetDbProviderFactory(provider);
Console.WriteLine($"Your factory object is a: {factory.GetType().Name}");

using DbConnection connection = factory.CreateConnection();
Console.WriteLine($"Your connection object is a: {connection.GetType().Name}");
connection.ConnectionString = connectionString;

ShowConnectionStatus((SqlConnection)connection);
connection.Open();
ShowConnectionStatus((SqlConnection)connection);

DbCommand command = factory.CreateCommand();
Console.WriteLine($"Your command object is a: {command.GetType().Name}");
command.Connection = connection;
command.CommandText = "SELECT i.Id, m.Name FROM Inventory i INNER JOIN Makes m ON m.Id = i.MakeId";

using DbDataReader dataReader = command.ExecuteReader();
Console.WriteLine($"Your data reader object is a: {dataReader.GetType().Name}");

Console.WriteLine("\n***** Current Inventory *****");
while (dataReader.Read())
{
    Console.WriteLine($"-> Car #{dataReader["Id"]} is a {dataReader["Name"]}.");
    //for (int i = 0; i < dataReader.FieldCount; i++)
    //{
    //    Console.Write(i != dataReader.FieldCount - 1 
    //        ? $"{dataReader.GetName(i)} = {dataReader.GetValue(i)}, "
    //        : $"{dataReader.GetName(i)} = {dataReader.GetValue(i)} ");
    //}
    //Console.WriteLine();
}

Console.ReadLine();

return;

DbProviderFactory GetDbProviderFactory(DataProviderEnum dataProviderEnum) => dataProviderEnum switch
{
    DataProviderEnum.SqlServer => SqlClientFactory.Instance,
    DataProviderEnum.Odbc => OdbcFactory.Instance,
#if PC
    DataProviderEnum.OleDb => OleDbFactory.Instance,
#endif
    _ => null
};

(DataProviderEnum provider, string connectionString) GetProviderFromConfiguration()
{
    var config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", true, true)
        .Build();

    var providerName = config["ProviderName"];
    if (Enum.TryParse(providerName, out DataProviderEnum provider))
    {
        return (provider, config[$"{providerName}:ConnectionString"]);
    }

    throw new Exception("Invalid data provider value supplied.");
}

static void ShowConnectionStatus(SqlConnection connection)
{
    // Show various stats about current connection object.
    Console.WriteLine("***** Info about your connection *****");
    Console.WriteLine($"Database location: {connection.DataSource}");
    Console.WriteLine($"Database name: {connection.Database}");
    Console.WriteLine($"Timeout: {connection.ConnectionTimeout}");
    Console.WriteLine($"Connection state: {connection.State}");
}