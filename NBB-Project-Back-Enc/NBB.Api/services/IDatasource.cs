
namespace NBB.Api.Services
{
    public interface IDatasource
    {
            IEnumerable<Contract> GetAll();
            Contract Get(int id);
            void Add(Contract contract);
            void Delete(Contract contract);
            void Update(Contract contract);
    }
}
