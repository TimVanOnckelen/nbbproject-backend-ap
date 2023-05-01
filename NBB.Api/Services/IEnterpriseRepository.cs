using NBB.Api.Models;

namespace NBB.Api.Services
{
    public interface IEnterpriseRepository
    {
        IEnumerable<Enterprise> GetAll();
        Enterprise Get(string id);
        void Add(Enterprise enterprise);
        void Delete(Enterprise enterprise);
        void Update (Enterprise  enterprise);
    }
}
