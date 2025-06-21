namespace AutoLot.Dal.BulkImport;
public class ProcessBulkImport
{
    private const string ConnectionString = @"Data Source=.,5433;User Id=sa;Password=P@ssw0rd;Initial Catalog=AutoLot;Encrypt=False;";
    private static SqlConnection _sqlConnection = null;

    public static void ExecuteBulkImport<T>(IEnumerable<T> records, string tableName)
    {
        OpenConnection();
        using SqlConnection conn = _sqlConnection;
        SqlBulkCopy bc = new(conn)
        {
            DestinationTableName = tableName
        };

        var dataReader = new MyDataReader<T>(records.ToList(), _sqlConnection, "dbo", tableName);

        try
        {
            bc.WriteToServer(dataReader);
        }
        catch (Exception ex)
        {
            //Should do something here
        }
        finally
        {
            CloseConnection();
        }
    }

    private static void OpenConnection()
    {
        _sqlConnection = new SqlConnection
        {
            ConnectionString = ConnectionString
        };
        _sqlConnection.Open();
    }

    private static void CloseConnection()
    {
        if (_sqlConnection?.State != ConnectionState.Closed)
        {
            _sqlConnection?.Close();
        }
    }
}
