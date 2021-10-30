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
    public class CompanyController : ControllerBase
    {
        private readonly ILogger<CompanyController> _logger;
        private readonly IDbTransaction _transaction;
        private readonly ICompanyRepository _companyRepository;

        public CompanyController(ILogger<CompanyController> logger, IDbTransaction transaction, ICompanyRepository companyRepository)
        {
            _logger = logger;
            _transaction = transaction;
            _companyRepository = companyRepository;
        }

        [HttpGet()]
        public async Task<IEnumerable<Company>> Get()
        {
            _logger.LogInformation("company API invoked");

            IEnumerable<Company> companies;
            try
            {
                companies = await _companyRepository.Get();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            _transaction.Commit();

            return companies;
        }
    }
}
