using Newtonsoft.Json;

namespace Laboratorium1.ZadanieProjektowe.NET
{
    public class ManagerZadan
    {
        private List<Zadanie> _zadania;

        public ManagerZadan()
        {
            _zadania = new List<Zadanie>();
        }

        public void DodajZadanie(Zadanie zadanie)
        {
            _zadania.Add(zadanie);
        }

        public void UsunZadanie(int id)
        {
            var zadanieDoUsuniecia = _zadania.Find(z => z.Id == id);
            if (zadanieDoUsuniecia != null)
            {
                _zadania.Remove(zadanieDoUsuniecia);
            }
            else
            {
                Console.WriteLine("Nie znaleziono zadania o podanym ID.");
            }
        }
        public void EdytujZadanie(int id, Zadanie noweDane)
            {
                var zadanieDoEdycji = _zadania.Find(z => z.Id == id);
                if (zadanieDoEdycji != null)
                {
                    zadanieDoEdycji.Nazwa = noweDane.Nazwa;
                    zadanieDoEdycji.Opis = noweDane.Opis;
                    zadanieDoEdycji.DataZakonczenia = noweDane.DataZakonczenia;
                    zadanieDoEdycji.CzyWykonane = noweDane.CzyWykonane;
                }
                else
                {
                    Console.WriteLine("Nie znaleziono zadania o podanym ID.");
                }
            }

        public void WyswietlZadania()
        {
            foreach (var zadanie in _zadania)
            {
                Console.WriteLine($"ID: {zadanie.Id}\n" +
                                  $"\tNazwa: {zadanie.Nazwa}\n" +
                                  $"\tOpis: {zadanie.Opis}\n" +
                                  $"\tData zakonczenia: {zadanie.DataZakonczenia:yyyy-MM-dd}\n" +
                                  $"\tCzy wykonane: {zadanie.CzyWykonane}\n");
            }
        }
        
        public void ZmienStatusZadania(int id, bool nowyStatus)
        {
            var zadanieDoZmiany = _zadania.Find(z => z.Id == id);
            if (zadanieDoZmiany != null)
            {
                zadanieDoZmiany.CzyWykonane = nowyStatus;
            }
            else
            {
                Console.WriteLine("Nie znaleziono zadania o podanym ID.");
            }
        }
        
        public void SortujZadania(string kryterium)
        {
            switch (kryterium.ToLower())
            {
                case "data":
                    _zadania = _zadania.OrderBy(z => z.DataZakonczenia).ToList();
                    break;
                case "nazwa":
                    _zadania = _zadania.OrderBy(z => z.Nazwa).ToList();
                    break;
                case "status":
                    _zadania = _zadania.OrderBy(z => z.CzyWykonane).ToList();
                    break;
                default:
                    Console.WriteLine("Nieznane kryterium sortowania.");
                    break;
            }
        }
        
        public bool CzyIdJestUnikalne(int id)
        {
            return !_zadania.Exists(z => z.Id == id);
        }
        
        public void ZapiszDoPlikuJson(string sciezka)
        {
            string json = JsonConvert.SerializeObject(_zadania);
            File.WriteAllText(sciezka, json);
        }

        public void WczytajZPlikuJson(string sciezka)
        {
            string json = File.ReadAllText(sciezka);
            var deserializedZadania = JsonConvert.DeserializeObject<List<Zadanie>>(json);
            if (deserializedZadania != null)
            {
                _zadania = deserializedZadania;
            }
            else
            {
                Console.WriteLine("Failed to deserialize JSON.");
            }
        }
    }
}