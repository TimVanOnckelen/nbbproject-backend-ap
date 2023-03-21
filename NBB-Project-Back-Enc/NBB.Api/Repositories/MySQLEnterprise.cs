using NBB.Api.Models;
using NBB.Api.services;

namespace NBB.Api.Repository
{
    public class MySQLEnterprise : IRepository
    {
        private EnterpriseDbContext _context;

        public MySQLEnterprise(EnterpriseDbContext context)
        {
            _context = context;
        }

        public void Add(Enterprise onderneming)
        {
            _context.Enterprise.Add(onderneming);
            _context.SaveChanges();
        }

        public void Delete(Enterprise onderneming)
        {
            var toDelete = Get(onderneming.EnterpriseNumber);
            _context.Enterprise.Remove(toDelete);
            _context.SaveChanges();
        }

        public Enterprise Get(string ondernemingsnummer)
        {
            return _context.Enterprise.FirstOrDefault(x => x.EnterpriseNumber == ondernemingsnummer);
        }

        public IEnumerable<Enterprise> GetAll()
        {
            return _context.Enterprise;
        }

        public void Update(Enterprise onderneming)
        {
            var toUpdate = Get(onderneming.EnterpriseNumber);
            toUpdate.EnterpriseName = onderneming.EnterpriseName;
            toUpdate.AccountingDataURL = onderneming.AccountingDataURL;
        }
    }
}
