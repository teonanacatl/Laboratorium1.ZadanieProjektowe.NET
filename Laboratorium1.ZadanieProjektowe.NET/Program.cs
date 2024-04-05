using System.Globalization;

namespace Laboratorium1.ZadanieProjektowe.NET
{
    class Program
    {
        static readonly ManagerZadan Manager = new();
        static string _sciezka = "zadania.json";

        static void Main()
        {
            bool continueProgram = true;
            while (continueProgram)
            {
                string? wybor = PobierzWyborKonsoli();
                continueProgram = PrzetworzWybor(wybor);
            }
        }
        
        static string? PobierzWyborKonsoli()
        {
            Console.WriteLine("1. Dodaj zadanie");
            Console.WriteLine("2. Usuń zadanie");
            Console.WriteLine("3. Wyświetl zadania");
            Console.WriteLine("4. Zapisz zadania do pliku");
            Console.WriteLine("5. Wczytaj zadania z pliku");
            Console.WriteLine("6. Edytuj zadanie");
            Console.WriteLine("7. Zmień status zadania");
            Console.WriteLine("8. Sortuj zadania");
            Console.WriteLine("9. Wyjdź");

            return Console.ReadLine();
        }
        
        static bool PrzetworzWybor(string? wybor)
        {
            if (wybor == "9")
            {
                return false;
            }
    
            try
            {
                switch (wybor)
                {
                    case "1":
                        DodajZadanie();
                        break;
                    case "2":
                        UsunZadanie();
                        break;
                    case "3":
                        WyswietlZadania();
                        break;
                    case "4":
                        ZapiszDoPlikuJson();
                        break;
                    case "5":
                        WczytajZPlikuJson();
                        break;
                    case "6":
                        EdytujZadanie();
                        break;
                    case "7":
                        ZmienStatusZadania();
                        break;
                    case "8":
                        SortujZadania();
                        break;
                    default:
                        Console.WriteLine("Nieznane polecenie.");
                        break;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Wprowadzono nieprawidłowy format danych. Spróbuj ponownie.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
            }

            return true;
        }

        static void DodajZadanie()
        {
            Zadanie zadanie = new Zadanie
            {
                Id = PobierzId(),
                Nazwa = PobierzNazwe(),
                Opis = PobierzOpis(),
                DataZakonczenia = PobierzDateZakonczenia(),
                CzyWykonane = PobierzStatusWykonania()
            };

            Manager.DodajZadanie(zadanie);
        }

        static int PobierzId()
        {
            int id;
            bool poprawneDane;
            do
            {
                Console.Write("Podaj ID: ");
                poprawneDane = int.TryParse(Console.ReadLine(), out id) && id >= 0;
                if (poprawneDane)
                {
                    if (!Manager.CzyIdJestUnikalne(id))
                    {
                        Console.WriteLine("ID zadania musi być unikalne. Spróbuj ponownie.");
                        poprawneDane = false;
                    }
                }
                else
                {
                    Console.WriteLine("ID zadania musi być liczbą całkowitą i nieujemną. Spróbuj ponownie.");
                }
            } while (!poprawneDane);

            return id;
        }

        static string PobierzNazwe()
        {
            string nazwa;
            do
            {
                Console.Write("Podaj nazwę: ");
                nazwa = Console.ReadLine() ?? string.Empty;
                if (string.IsNullOrWhiteSpace(nazwa))
                {
                    Console.WriteLine("Nazwa jest wymagana. Spróbuj ponownie.");
                }
            } while (string.IsNullOrWhiteSpace(nazwa));

            return nazwa;
        }

        static string PobierzOpis()
        {
            string opis;
            do
            {
                Console.Write("Podaj opis: ");
                opis = Console.ReadLine() ?? string.Empty;
                if (string.IsNullOrWhiteSpace(opis))
                {
                    Console.WriteLine("Opis jest wymagany. Spróbuj ponownie.");
                }
            } while (string.IsNullOrWhiteSpace(opis));

            return opis;
        }

        static DateTime PobierzDateZakonczenia()
        {
            DateTime dataZakonczenia;
            bool poprawneDane;
            do
            {
                Console.Write("Podaj datę zakończenia (yyyy-mm-dd): ");
                poprawneDane = DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataZakonczenia);
                if (!poprawneDane)
                {
                    Console.WriteLine("Data musi być w formacie yyyy-mm-dd. Spróbuj ponownie.");
                }
            } while (!poprawneDane);

            return dataZakonczenia;
        }

        static bool PobierzStatusWykonania()
        {
            bool poprawneDane;
            string odpowiedz;
            do
            {
                Console.Write("Czy zadanie jest wykonane? (tak/nie): ");
                odpowiedz = Console.ReadLine()?.ToLower() ?? string.Empty;
                poprawneDane = odpowiedz == "tak" || odpowiedz == "nie";
                if (!poprawneDane)
                {
                    Console.WriteLine("Odpowiedź musi być 'tak' lub 'nie'. Spróbuj ponownie.");
                }
            } while (!poprawneDane);

            return odpowiedz == "tak";
        }

        static void UsunZadanie()
        {
            Console.Write("Podaj ID zadania do usunięcia: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Manager.UsunZadanie(id);
            }
        }

        static void WyswietlZadania()
        {
            Manager.WyswietlZadania();
        }

        static void ZapiszDoPlikuJson()
        {
            Manager.ZapiszDoPlikuJson(_sciezka);
        }

        static void WczytajZPlikuJson()
        {
            Manager.WczytajZPlikuJson(_sciezka);
        }

        static void EdytujZadanie()
        {
            Console.Write("Podaj ID zadania do edycji: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Zadanie noweDane = new Zadanie();
                Console.Write("Podaj nową nazwę: ");
                noweDane.Nazwa = Console.ReadLine();
                Console.Write("Podaj nowy opis: ");
                noweDane.Opis = Console.ReadLine();
                Console.Write("Podaj nową datę zakończenia (yyyy-mm-dd): ");
                if (DateTime.TryParse(Console.ReadLine(), CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dataZakonczenia))
                {
                    noweDane.DataZakonczenia = dataZakonczenia;
                }
                Console.Write("Czy zadanie jest teraz wykonane? (tak/nie): ");
                noweDane.CzyWykonane = Console.ReadLine()?.ToLower() == "tak";
                Manager.EdytujZadanie(id, noweDane);
            }
        }

        static void ZmienStatusZadania()
        {
            Console.Write("Podaj ID zadania do zmiany statusu: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                bool nowyStatus = Console.ReadLine()?.ToLower() == "tak";
                Manager.ZmienStatusZadania(id, nowyStatus);
            }
        }

        static void SortujZadania()
        {
            Console.Write("Podaj kryterium sortowania (data, nazwa, status): ");
            string? kryterium = Console.ReadLine();
            if (kryterium != null)
            {
                Manager.SortujZadania(kryterium);
            }
        }
    }
}