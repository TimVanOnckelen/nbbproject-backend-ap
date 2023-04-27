using NBB.Api.Entities;
using NBB.Api.Models;

namespace NBB.Api.Services
{
    public class EfNBBRepository : IEnterpriseRepository
    {
        private NBBDBContext _context;

        public EfNBBRepository(NBBDBContext context)
        {
            _context = context;
        }

        public IEnumerable<Enterprise> GetAll()
        {
            return _context.Enterprises.ToList();
        }

        public Enterprise Get(string id)
        {
            return _context.Enterprises.FirstOrDefault(x => x.EnterpriseNumber == id);
        }

        public void Add(Enterprise enterprise)
        {
            _context.Enterprises.Add(enterprise);
            _context.SaveChanges();
        }

        public void Delete(Enterprise enterprise)
        {
            _context.Enterprises.Remove(enterprise);
            _context.SaveChanges();
        }

        public void Update(Enterprise enterprise)
        {
            var toUpdate = _context.Enterprises.FirstOrDefault(x => x.EnterpriseNumber == enterprise.EnterpriseNumber);
            toUpdate.FinancialData = enterprise.FinancialData;
            _context.Enterprises.Update(toUpdate);
            _context.SaveChanges();
        }
    }
}
