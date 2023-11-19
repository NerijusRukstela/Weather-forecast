namespace ISun.Application.Interfaces;

public interface IDataInsertProcessService
{
    public Task PeriodicallyBackgroundJob();
}