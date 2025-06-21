Console.WriteLine("Fun with Entity Framework Core");


static void SampleSaveChanges()
{
    //The factory is not meant to be used like this, but it’s demo code :-)
    var context = new ApplicationDbContextFactory().CreateDbContext(null);

    //make some changes


    context.SaveChanges();
}

static void UsingSavePoints()
{
    //The factory is not meant to be used like this, but it’s demo code :-)
    var context = new ApplicationDbContextFactory().CreateDbContext(null);
    using var trans = context.Database.BeginTransaction();

    try
    {
        //Create, change, delete stuff

        trans.CreateSavepoint("check point 1");
        context.SaveChanges();
        trans.Commit();
    }
    catch (Exception)
    {
        trans.RollbackToSavepoint("check point 1");
    }
}

static void TransactionWithExecutionStrategies()
{
    //The factory is not meant to be used like this, but it’s demo code :-)
    var context = new ApplicationDbContextFactory().CreateDbContext(null);
    var strategy = context.Database.CreateExecutionStrategy();

    strategy.Execute(() =>
    {
        using var trans = context.Database.BeginTransaction();
        try
        {
            //actionToExecute();
            trans.Commit();
            Console.WriteLine("Insert succeeded");
        }
        catch (Exception ex)
        {
            trans.Rollback();
            Console.WriteLine($"Insert failed: {ex.Message}");
        }
    });
}