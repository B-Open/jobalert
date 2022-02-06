using Shared.Models;
using Shared.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IDbTransaction _transaction;
        private readonly IUserRepository _userRepository;

        public AccountController(ILogger<AccountController> logger, IDbTransaction transaction, IUserRepository userRepository)
        {
            _logger = logger;
            _transaction = transaction;
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public async Task Register()
        {
            await Task.Delay(1);
            throw new System.NotImplementedException();
        }

        [HttpPost("login")]
        public async Task Login()
        {
            await Task.Delay(1);
            throw new System.NotImplementedException();
        }

        [HttpGet("me")]
        public async Task Get()
        {
            await Task.Delay(1);
            throw new System.NotImplementedException();
        }
    }
}
