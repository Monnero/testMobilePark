namespace testMobilePark.Interfaces
{
    public interface INewsRepository : IDisposable
    {
        Task<List<News>> GetNewsAsync();
        Task<News?> GetNewAsync(int newId);
        Task InsertNewAsync(News newObj);
        Task SaveAsync();
    }
}
