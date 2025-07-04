﻿namespace AutoLot.Dal.Tests.Base;

public abstract class BaseTest : IDisposable
{
    protected readonly IConfiguration Configuration;
    protected readonly ApplicationDbContext Context;
    protected readonly ITestOutputHelper OutputHelper;

    protected BaseTest(ITestOutputHelper outputHelper)
    {
        Configuration = TestHelpers.GetConfiguration();
        Context = TestHelpers.GetContext(Configuration);
        OutputHelper = outputHelper;
    }

    protected void ExecuteInATransaction(Action actionToExecute)
    {
        var strategy = Context.Database.CreateExecutionStrategy();
        strategy.Execute(() =>
        {
            using var transaction = Context.Database.BeginTransaction();
            actionToExecute();
            transaction.Rollback();
        });
    }

    protected void ExecuteInASharedTransaction(Action<IDbContextTransaction> actionToExecute)
    {
        var strategy = Context.Database.CreateExecutionStrategy();
        strategy.Execute(() =>
        {
            using var trans =
            Context.Database.BeginTransaction(IsolationLevel.ReadUncommitted);
            actionToExecute(trans);
            trans.Rollback();
        });
    }

    public virtual void Dispose()
    {
        Context.Dispose();
    }
}
