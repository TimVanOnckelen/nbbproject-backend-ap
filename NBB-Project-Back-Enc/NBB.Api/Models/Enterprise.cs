namespace NBB.Api.Models
{
    public class Enterprise
    {
        public int Id { get; set; }
        public string EnterpriseNumber { get; set; }
        public string EnterpriseName { get; set; }
        public Address Address { get; set; }
        public string AccountingDataURL { get; set; }

        public List<FinancialData> FinancialDataArray { get; set; }

    }
}
