using System.Net;

namespace NBB.Api.Models
{
    public class Enterprise
    {
        public string ReferenceNumber { get; set; }
        public string DepositDate { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string ModelType { get; set; }
        public string DepositType { get; set; }
        public string Language { get; set; }
        public string Currency { get; set; }
        public string EnterpriseNumber { get; set; }
        public string EnterpriseName { get; set; }
        public Address Address { get; set; }
        public string LegalForm { get; set; }
        public string LegalSituation { get; set; }
        public bool FullFillLegalValidation { get; set; }
        public string ActivityCode { get; set; }
        public string GeneralAssemblyDate { get; set; }
        public string AccountingDataURL { get; set; }
        public string DataVersion { get; set; }
        public string ImprovementDate { get; set; }
        public string CorrectedData { get; set; }

        public FinancialData FinancialData { get; set; }

    }
}
