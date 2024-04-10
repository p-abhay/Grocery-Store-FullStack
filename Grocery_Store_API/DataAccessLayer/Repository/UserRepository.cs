using DataAccessLayer.DbContexts;
using DataAccessLayer.EFModels;
using DataAccessLayer.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ProductDbContext _productDbContext;
        public UserRepository(ProductDbContext productDbContext)
        {
            _productDbContext = productDbContext;
        }

        public async Task<IEnumerable<UserEFModel>> GetAllUsersAsync()
        {
            return await _productDbContext.Users.ToListAsync();
        }

        public async Task<UserEFModel?> GetByEmailAsync(string email)
        {
            var user = await _productDbContext.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
            return user;
        }

        public async Task<UserEFModel> SignUpAsync(UserEFModel user)
        {
            await _productDbContext.Users.AddAsync(user);
            await _productDbContext.SaveChangesAsync();
            return user;
        }
    }
}
