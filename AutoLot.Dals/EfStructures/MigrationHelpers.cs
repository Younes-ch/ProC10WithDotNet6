namespace AutoLot.Dals.EfStructures;

public static class MigrationHelpers
{
    public static void CreateCustomerOrderView(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql(@"exec (N'
            CREATE VIEW [dbo].[CustomerOrderView]
            AS 
            SELECT c.FirstName, c.LastName, i.Color, i.PetName, i.DateBuilt, i.IsDrivable,
                    i.Price, i.Display, m.Name AS Make
            FROM dbo.Orders o
            INNER JOIN dbo.Customers c ON c.Id = o.CustomerId
            INNER JOIN dbo.Inventory i ON i.Id = o.CarId
            INNER JOIN dbo.Makes m ON m.Id = i.MakeId
        ')");
    }

    public static void DropCustomerOrderView(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("EXEC (N' DROP VIEW [dbo].[CustomerOrderView] ')");
    }

    public static void CreateSproc(MigrationBuilder migrationsBuilder)
    {
        migrationsBuilder.Sql(@"exec (N'
            CREATE PROCEDURE [dbo].[GetPetName]
                @carId int,
                @petName nvarchar(50) output
            AS
                SELECT @petName = PetName from dbo.Inventory where Id = @carId
        ')");
    }

    public static void DropSproc(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("EXEC (N' DROP PROCEDURE [dbo].[GetPetName]')");
    }

    public static void CreateFunctions(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql(@"exec (N'
            CREATE FUNCTION [dbo].[udf_GetCarsForMake] (@makeId int) 
            RETURNS TABLE
            AS 
            RETURN
            (
                SELECT Id, IsDrivable, DateBuilt, Color, PetName, MakeId, TimeStamp,
                Display, Price
                FROM Inventory WHERE MakeId = @makeId
            )
        ')");

        migrationBuilder.Sql(@"exec (N'
            CREATE FUNCTION [dbo].[udf_CountMakes] (@makeId int)
            RETURNS int
            AS 
            BEGIN
                DECLARE @Result int
                SELECT @Result = COUNT(MakeId) FROM dbo.Inventory WHERE MakeId = @makeId
                RETURN @Result
            END
        ')");
    }

    public static void DropFunctions(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("EXEC (N'DROP FUNCTION [dbo].[udf_GetCarsForMake]')");
        migrationBuilder.Sql("EXEC (N'DROP FUNCTION [dbo].[udf_CountMakes]')");
    }
}
