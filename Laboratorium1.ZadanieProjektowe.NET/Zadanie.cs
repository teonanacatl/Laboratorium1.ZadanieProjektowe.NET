namespace Laboratorium1.ZadanieProjektowe.NET
{
    public class Zadanie
    {
        public int Id { get; init; }
        public string? Nazwa { get; set; }
        public string? Opis { get; set; }
        public DateTime DataZakonczenia { get; set; }
        public bool CzyWykonane { get; set; }
    }
}