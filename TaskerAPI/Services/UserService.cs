using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskerAPI.Data;
using System.Threading.Tasks;
using TaskerAPI.Model;

namespace TaskerAPI.Services
{
    public class UserService : IUserService
    {
        private readonly AuthDbContext _context;
        private readonly IConfiguration _configuration;

        public UserService(AuthDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<string> RegisterAsync(string firstName, string lastName, string email, string password)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email_ID == email);
            if (existingUser != null)
            {
                return "User exists";
            }
            else
            {
                var user = new Users
                {
                    First_Name = firstName,
                    Last_Name = lastName,
                    Email_ID = email,
                    Password = BCrypt.Net.BCrypt.HashPassword(password),
                    Created_Date = DateTime.UtcNow,
                    Is_Active = true
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return "Registered";
            }
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var user = _context.Users.SingleOrDefault(u => u.Email_ID == email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return null;
            }
            return Convert.ToString(user.ID);
        }

        public async Task<List<Users>> GetUsersAsync(int ID)
        {   
            if(ID == 0)
            {
                var users = await _context.Users.ToListAsync();
                return users;
            }
            else
            {
                var user_ = await _context.Users.Where(u => u.ID == ID).ToListAsync();
                return user_;
            }
            
        }
        public async Task<string> UpdateUserdata(int ID,string firstname,string lastname,string email,string password)
        {
            if (ID == 0)
            {
                return null;           
            }
            else
            {
                var user =  _context.Users.Where(u => u.ID == ID).FirstOrDefault();
                if (user != null)
                {
                    user.First_Name = firstname;
                    user.Last_Name = lastname;
                    user.Email_ID = email;
                    user.Password = BCrypt.Net.BCrypt.HashPassword(password);
                    user.Modified_Date = DateTime.UtcNow;
                    user.Is_Active = true;

                     _context.Users.Update(user);
                     await  _context.SaveChangesAsync();
 
                    return "Updated";
                }

                return null;
                
            }

        }
    }
}
