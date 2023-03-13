namespace NBB.Api.Models
{
    public class Onderneming
    {
        public string OndernemingsNummer { get; set; }
        public string Naam { get; set; }
        public Address Adres { get; set; }
        public DateTime DatumNeerlegging { get; set; }
        public long EigenVermogen { get; set; }
        public long Schulden { get; set; }
        public long Bedrijfswinst { get; set; }
    }
}
