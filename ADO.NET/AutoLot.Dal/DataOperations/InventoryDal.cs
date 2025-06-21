namespace AutoLot.Dal.DataOperations;
public class InventoryDal : IDisposable
{
    private readonly string _connectionString;
    private SqlConnection _sqlConnection = null;
    bool _disposed = false;
    public InventoryDal() : this(@"Data Source=.,5433;User Id=sa;Password=P@ssw0rd;Initial Catalog=AutoLot;Encrypt=False;")
    { }

    public InventoryDal(string connectionString) => _connectionString = connectionString;

    public List<CarViewModel> GetAllInventory()
    {
        OpenConnection();
        var inventory = new List<CarViewModel>();

        string sql = @"SELECT i.Id, i.Color, i.PetName, m.Name as Make FROM Inventory i INNER JOIN Makes m ON m.Id = i.MakeId;";

        using var command = new SqlCommand(sql, _sqlConnection);

        var dataReader = command.ExecuteReader(CommandBehavior.CloseConnection);

        while (dataReader.Read())
        {
            inventory.Add(new CarViewModel()
            {
                Id = (int)dataReader["Id"],
                Color = (string)dataReader["Color"],
                PetName = (string)dataReader["PetName"],
                Make = (string)dataReader["Make"],
            });
        }

        dataReader.Close();
        return inventory;
    }

    public CarViewModel GetCar(int carId)
    {
        OpenConnection();
        CarViewModel car = null;

        SqlParameter param = new SqlParameter
        {
            ParameterName = "@carId",
            Value = carId,
            Direction = ParameterDirection.Input,
            SqlDbType = SqlDbType.Int
        };

        string sql = @"SELECT i.Id, i.Color, i.PetName, m.Name as Make
                       FROM Inventory i INNER JOIN Makes m ON m.Id = i.Id
                       WHERE i.Id = @carId;";

        var command = new SqlCommand(sql, _sqlConnection);

        command.Parameters.Add(param);

        using var dataReader = command.ExecuteReader(CommandBehavior.CloseConnection);

        while (dataReader.Read())
        {
            car = new CarViewModel
            {
                Id = (int)dataReader["Id"],
                Color = (string)dataReader["Color"],
                PetName = (string)dataReader["PetName"],
                Make = (string)dataReader["Make"],
            };
        }

        dataReader.Close();
        return car;
    }

    public string LookUpPetName(int carId)
    {
        OpenConnection();

        string carPetName;

        // Input param.
        SqlParameter carIdParam = new()
        {
            ParameterName = "@carId",
            SqlDbType = SqlDbType.Int,
            Value = carId,
            Direction = ParameterDirection.Input
        };

        // Output param.
        SqlParameter petNameParam = new()
        {
            ParameterName = "@petName",
            SqlDbType = SqlDbType.NVarChar,
            Size = 50,
            Direction = ParameterDirection.Output
        };

        // Establish name of stored proc.
        using var command = new SqlCommand("GetPetName", _sqlConnection);

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddRange([carIdParam, petNameParam]);

        // Execute the stored proc.
        command.ExecuteNonQuery();

        // Return output param.
        carPetName = (string)command.Parameters["@petName"].Value;

        CloseConnection();

        return carPetName;
    }

    public void InsertAuto(string color, int makeId, string petName)
    {
        OpenConnection();

        var sql = $@"INSERT INTO Inventory(MakeId, Color, PetName) VALUES ('{makeId}', '{color}', '{petName}');";

        using var command = new SqlCommand(sql, _sqlConnection);

        command.ExecuteNonQuery();

        CloseConnection();
    }

    public void InsertAuto(Car car)
    {
        OpenConnection();

        SqlParameter makeParameter = new()
        {
            ParameterName = "@MakeId",
            Value = car.MakeId,
            SqlDbType = SqlDbType.Int,
            Direction = ParameterDirection.Input
        };

        SqlParameter colorParameter = new()
        {
            ParameterName = "@Color",
            Value = car.Color,
            SqlDbType = SqlDbType.NVarChar,
            Size = 50,
            Direction = ParameterDirection.Input
        };

        SqlParameter petNameParameter = new()
        {
            ParameterName = "@PetName",
            Value = car.PetName,
            SqlDbType = SqlDbType.NVarChar,
            Size = 50,
            Direction = ParameterDirection.Input
        };

        string sql = "INSERT INTO Inventory (MakeId, Color, PetName) VALUES (@MakeId, @Color, @PetName)";

        using var command = new SqlCommand(sql, _sqlConnection);

        command.Parameters.AddRange([makeParameter, colorParameter, petNameParameter]);

        int result = command.ExecuteNonQuery();

        CloseConnection();
    }

    public void UpdateCarPetName(int carId, string newPetName)
    {
        OpenConnection();

        SqlParameter paramId = new()
        {
            ParameterName = "@carId",
            Value = carId,
            Direction = ParameterDirection.Input,
            SqlDbType = SqlDbType.Int
        };

        SqlParameter paramName = new()
        {
            ParameterName = "@petName",
            Value = newPetName,
            SqlDbType = SqlDbType.NVarChar,
            Size = 50,
            Direction = ParameterDirection.Input
        }
    ;

        var sql = $@"UPDATE Inventory SET PetName = @petName WHERE Id = @carId;";

        using var command = new SqlCommand(sql, _sqlConnection);

        command.Parameters.AddRange([paramId, paramName]);

        command.ExecuteNonQuery();

        CloseConnection();
    }

    public void DeleteCar(int carId)
    {
        OpenConnection();

        SqlParameter param = new()
        {
            ParameterName = "@carId",
            Value = carId,
            Direction = ParameterDirection.Input,
            SqlDbType = SqlDbType.Int
        };

        var sql = $@"DELETE FROM Inventory WHERE Id = @carId;";

        using var command = new SqlCommand(sql, _sqlConnection);

        command.Parameters.Add(param);

        try
        {
            command.ExecuteNonQuery();
        }
        catch (SqlException ex)
        {
            Exception error = new Exception("Sorry! That car is on order!", ex);
            throw error;
        }

        CloseConnection();
    }

    public void ProcessCreditRisk(bool throwEx, int customerId)
    {
        OpenConnection();

        // First, look up current name based on customer ID.
        string fName;
        string lName;
        var cmdSelect = new SqlCommand("SELECT * FROM Customers WHERE Id = @customerId", _sqlConnection);
        SqlParameter paramId = new()
        {
            ParameterName = "@customerId",
            SqlDbType = SqlDbType.Int,
            Value = customerId,
            Direction = ParameterDirection.Input
        };
        cmdSelect.Parameters.Add(paramId);

        using (var dataReader = cmdSelect.ExecuteReader())
        {
            if (dataReader.HasRows)
            {
                dataReader.Read();
                fName = (string)dataReader["FirstName"];
                lName = (string)dataReader["LastName"];
            }
            else
            {
                CloseConnection();
                return;
            }
        }

        cmdSelect.Parameters.Clear();

        // Create command objects that represent each step of the operation.
        SqlCommand cmdUpdate = new("UPDATE Customers SET LastName = LastName + ' (CreditRisk) ' WHERE Id = @customerId", _sqlConnection);
        cmdUpdate.Parameters.Add(paramId);

        var cmdInsert = new SqlCommand("INSERT INTO CreditRisks (CustomerId, FirstName, LastName) VALUES( @CustomerId, @FirstName, @LastName)", _sqlConnection);
        SqlParameter parameterId2 = new()
        {
            ParameterName = "@CustomerId",
            SqlDbType = SqlDbType.Int,
            Value = customerId,
            Direction = ParameterDirection.Input
        };
        SqlParameter parameterFirstName = new()
        {
            ParameterName = "@FirstName",
            Value = fName,
            SqlDbType = SqlDbType.NVarChar,
            Size = 50,
            Direction = ParameterDirection.Input
        };
        SqlParameter parameterLastName = new()
        {
            ParameterName = "@LastName",
            Value = lName,
            SqlDbType = SqlDbType.NVarChar,
            Size = 50,
            Direction = ParameterDirection.Input
        };
        cmdInsert.Parameters.AddRange([parameterId2, parameterFirstName, parameterLastName]);

        // We will get this from the connection object.
        SqlTransaction tx = null;
        try
        {
            tx = _sqlConnection.BeginTransaction();

            // Enlist the commands into this transaction.
            cmdInsert.Transaction = tx;
            cmdUpdate.Transaction = tx;

            // Execute the commands.
            cmdInsert.ExecuteNonQuery();
            cmdUpdate.ExecuteNonQuery();

            // Simulate error.
            if (throwEx)
            {
                throw new Exception("Sorry! Database error! Tx failed...");
            }

            // Commit it!
            tx.Commit();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            // Any error will roll back transaction. Using the new conditional access operator to check for null.
            tx?.Rollback();
        }
        finally
        {
            CloseConnection();
        }
    }

    private void OpenConnection()
    {
        _sqlConnection = new SqlConnection(_connectionString);

        _sqlConnection.Open();
    }

    private void CloseConnection()
    {
        if (_sqlConnection?.State != ConnectionState.Closed) _sqlConnection?.Close();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            _sqlConnection.Dispose();
        }
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
