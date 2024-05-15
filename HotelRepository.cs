
using Microsoft.EntityFrameworkCore;
using testMobilePark.Interfaces;

namespace HotelWebApi
{
    public class NewsRepository : INewsRepository
    {
        private readonly NewsDb _context;
        public NewsRepository(NewsDb context) => _context = context;
        public async Task<List<News>> GetNewsAsync()
        {
            return await _context.News.ToListAsync();
        }
        public async Task<News?> GetNewAsync(int hotelId)
        {
            return await _context.News.FindAsync([hotelId]);
        }
        public async Task InsertNewAsync(News news)
        {
            await _context.News.AddAsync(news);
        }
        //public async Task UpdateHotelAsync(News news)
        //{
        //    var NewsFromDb = await _context.News.FindAsync([hotel.Id]);
        //    if (NewsFromDb is null) return;
        //    NewsFromDb.Keyword= hotel.Name;
        //    NewsFromDb.Longitude = hotel.Longitude;
        //    NewsFromDb.Latitude = hotel.Latitude;

        //}
        //public async Task DeleteHotelAsync(int hotelId)
        //{
        //    var hotelFromDb = await _context.News.FindAsync([hotelId]);
        //    if (hotelFromDb is null) return;
        //    _context.News.Remove(hotelFromDb);
        //}

        public async Task SaveAsync()=> await _context.SaveChangesAsync();

        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        

    }
}
