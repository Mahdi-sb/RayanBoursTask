using RayanBours.Domain.Context;
using RayanBours.Domain.Model;

namespace RayanBours.Services
{
    public class CompanyServices
    {
        private readonly DatabaseContext _databaseContext;

        public CompanyServices(DatabaseContext databaseContext)
        {
            this._databaseContext = databaseContext;
        }

        public async Task AddCompany(string symbol)
        {
            await _databaseContext.Companies.AddAsync(new Company(Guid.NewGuid(), symbol)
            );
            await SaveChangeAsync();
        }

        public async Task SaveChangeAsync()
        {
            await _databaseContext.SaveChangesAsync();
        }
    }
}