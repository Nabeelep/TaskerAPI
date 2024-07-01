using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskerAPI.Model;

namespace TaskerAPI.Services
{
    public interface IUserService
    {
        Task<string> RegisterAsync(string firstName, string lastName, string email, string password);
        Task<string> LoginAsync(string email, string password);
        Task<List<Users>> GetUsersAsync(int ID);
        Task<string> UpdateUserdata(int ID, string firstname, string lastname, string email, string password);
    }
}