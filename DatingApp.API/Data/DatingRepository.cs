using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class DatingRepository : IDatingRepository
    {
        private readonly DataContext _context;

        public DatingRepository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<IEnumerable<User>> FetchUsers(int? UserId=null)
        {
            var users = new List<User>();
            if (UserId.HasValue)
            {
                users.Add(await _context.Users.Include(x=>x.Photos).FirstOrDefaultAsync(x=>x.Id==UserId));
            }
            else
            {
                users = await _context.Users.Include(x=>x.Photos).ToListAsync();
            }
            return users;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}