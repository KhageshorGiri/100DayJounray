using ClientPro.Domain.Entities;
using ClientPro.Domain.IRepositories;
using ClientPro.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace ClientPro.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {

        private readonly ClientDbContext _dbContext;

        public ClientRepository(ClientDbContext context)
        {
            _dbContext = context;
        }

        public async Task<Client> AddAsync(Client client)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    _dbContext.Clients.Add(client);
                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return client;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<Client> DeleteAsync(Client client)
        {
            using (var trainsaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    _dbContext.Clients.Remove(client);
                    await _dbContext.SaveChangesAsync();
                    await trainsaction.CommitAsync();

                    return client;
                }
                catch (Exception ex)
                {
                    await trainsaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<IEnumerable<Client>> GetAllClientAsync()
        {
            return await _dbContext.Clients.AsNoTracking().ToListAsync();
        }

        public async Task<Client> GetClientAsync(int id)
        {
            return await _dbContext.Clients.Where(x=>x.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<Client> UpdateAsync(Client client)
        {
           using(var transaction = await _dbContext.Database.BeginTransactionAsync())
           {
                try
                {
                    _dbContext.Clients.Update(client);
                    await  _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return client;
                }
                catch(Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
           }
        }
    }
}
