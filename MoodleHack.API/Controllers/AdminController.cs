using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MoodleHack.Infrastructure.Data;

namespace MoodleHack.API.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public AdminController(AppDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<IActionResult> GetAllAccounts(string token)
        {
            if (_configuration["TOKEN"] == token)
                return Ok(await _dbContext.Users.AsQueryable().Where(x => x.IsActive)
                    .ToArrayAsync());
            return Forbid();
        }
        //Remove all
        //Get all accounts with roles
        //Sync all accounts
    }
}