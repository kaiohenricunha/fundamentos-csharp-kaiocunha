using Domain;
using System.Globalization;
using System.Text.RegularExpressions;

namespace WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ITutorialRepository _repository;
        private const string pressButtons = "Press any button to show the main menu ...";

        public Worker(ITutorialRepository repository)
        {
            _repository = repository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;

            string option;


            do
            {
                Console.WriteLine("");
                ShowMenu();

                option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        AddTutorial();
                        break;
                    case "2":
                        SearchTutorial();
                        break;
                    case "3":
                        UpdateTutorial();
                        break;
                    case "4":
                        DeleteTutorial();
                        break;
                    case "5":
                        ListTutorials();
                        break;
                    case "6":
                        Console.WriteLine("\nExit...Are you sure you want to exit the application? (type 'yes' or 'no'):");
                        var answer = Console.ReadLine().ToLower();

                        if (Regex.IsMatch(answer, "yes", RegexOptions.IgnoreCase))
                        {
                            Environment.Exit(0);
                        }
                        break;
                    default:
                        Console.Write("\nInvalid option! Choose a valid option. ");
                        break;
                }
                Console.WriteLine(pressButtons);
                Console.ReadKey();
            }
            while (option != "6");
        }

        void ListTutorials()
        {
            var resultList = _repository.GetTutorials();

            if (resultList.Any())
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("==== Tutorials List ==");
                Console.ResetColor();
                foreach (var tutorial in resultList)
                {
                    Console.WriteLine($"\n{tutorial.GetResumeData()} - Price: {tutorial.Price}");
                }
            }
            else
            {
                Console.WriteLine("===================================");
                Console.WriteLine("\nNo tutorials found!");
            }
        }

        void ShowFiveRegister()
        {
            var resultList = _repository.GetLastFive().ToList();

            if (resultList.Any())
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("==== Last Tutorials Registered ====");
                Console.ResetColor();
                foreach (var tutorial in resultList)
                {
                    Console.WriteLine($"{tutorial.GetResumeData()}");
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("===================================");
                Console.WriteLine("\nNo tutorials found!");
                Console.WriteLine("===================================");
                Console.ResetColor();
            }
        }
        void SearchTutorial()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("==== Search tutorial ==== ");
            Console.ResetColor();
            Console.WriteLine("\n== Type in the tutorial title(or part of it): ");
            var partialName = Console.ReadLine();

            var resultList = _repository.GetTitle(partialName);

            if (!resultList.Any())
            {
                Console.WriteLine("==========");
                Console.WriteLine("\nNo tutorials found!");
                return;
            }

            Console.Write("==========");
            Console.WriteLine("\nResults: ");

            foreach (var item in resultList)
            {
                Console.WriteLine($"\t {item}");
            }
        }

        void AddTutorial()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("===== Add Tutorial ====");
            Console.ResetColor();
            Console.WriteLine("\t==== Type: ====");

            Console.WriteLine("[ id ]: ");
            var id = int.Parse(Console.ReadLine());

            Console.WriteLine("[ Title ] type the title of the tutorial:");
            var title = Console.ReadLine().ToUpper();

            Console.WriteLine("[ Instructor ]: ");
            var instructor = Console.ReadLine().ToUpper();

            Console.WriteLine("[ Maximum Free Subscription Date ] Format dd/MM/yyyy: ");
            var maxDate = Console.ReadLine();

            Console.Write("[ Total Hours ]:");
            var totalHours = int.Parse(Console.ReadLine());

            DateTime date;

            if (DateTime.TryParseExact(maxDate, "dd/MM/yyyy", null, DateTimeStyles.None, out date))
            {
                var tutorial = new Tutorial(id, title, instructor, date, totalHours);
                _repository.Insert(tutorial);

                Console.WriteLine("=====================");
                Console.WriteLine("\nTutorial added!");
            }
        }

        void UpdateTutorial()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("==== Update Tutorial ====");
            Console.ResetColor();
            Console.WriteLine("\tFirst search for the tutorial to be edited: ");
            var partialName = Console.ReadLine();

            var resultList = _repository.GetTitle(partialName);

            if (!resultList.Any())
            {
                Console.WriteLine("=====================");
                Console.WriteLine("\nResults: ");
                Console.WriteLine("\nNo results found");
                return;
            }

            Console.WriteLine("=====================");
            Console.WriteLine("\nResults: ");

            foreach (var item in resultList)
            {
                Console.WriteLine($"{item.GetResumeData()}");
            }

            Console.WriteLine("\n[ id to be edited ]:");
            int.TryParse(Console.ReadLine(), out int id);

            var resultTutorial = resultList.FirstOrDefault(p => p.Id == id);

            if (resultTutorial == null)
            {
                Console.WriteLine("=====================");
                Console.WriteLine("\nNo results found");
                return;
            }

            Console.WriteLine("\n[Title to be edited]:");
            var title = Console.ReadLine().ToUpper();

            Console.WriteLine("\n[Instructor to be edited]: ");
            var instructor = Console.ReadLine().ToUpper();

            Console.WriteLine("\n[Max Free Subscription Date] format dd/MM/yyyy:");
            string maxDate = Console.ReadLine();

            Console.Write("\n[Total hours to be edited]: ");
            var totalHours = int.Parse(Console.ReadLine());

            if (DateTime.TryParseExact(maxDate, "dd/MM/yyyy", null, DateTimeStyles.None, out DateTime date))
            {
                resultTutorial.Title = title;
                resultTutorial.Instructor = instructor;
                resultTutorial.MaxDate = date;
                resultTutorial.TotalHours = totalHours;
                _repository.Update(resultTutorial);

                Console.WriteLine("=====================");
                Console.WriteLine("\nTutorial edited!");
            }

        }
        void DeleteTutorial()
        {

            var resultList = _repository.GetTutorials();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("====Delete Tutorial====");
            Console.ResetColor();
            Console.WriteLine("\n[Id of the tutorial you want to remove]: ");

            int.TryParse(Console.ReadLine(), out int id);

            var resultTutorial = resultList.FirstOrDefault(p => p.Id == id);

            if (resultTutorial == null)
            {
                Console.WriteLine("===========================");
                Console.WriteLine("\nNo results found");
                return;
            }

            Console.WriteLine($"\nAre you sure you want to delete the tutorial below?(Type [yes] or [no])\n{resultTutorial}");

            var answer = Console.ReadLine()?.ToLower();

            if (Regex.IsMatch(answer, "yes", RegexOptions.IgnoreCase))
            {

                _repository.Delete(resultTutorial);
                Console.WriteLine("===================");
                Console.WriteLine("\nTutorial deleted!");
            }
        }
        void ShowMenu()
        {
            Console.Clear();
            ShowFiveRegister();
            Console.Title = "AT - Kaio H. Cunha";
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("======================================");
            Console.WriteLine("--- Music Tutorials Manager ---");
            Console.WriteLine("======================================");
            Console.ResetColor();
            Console.WriteLine("[ 1 ] Add tutorial");
            Console.WriteLine("[ 2 ] Search or detail tutorial");
            Console.WriteLine("[ 3 ] Edit tutorial");
            Console.WriteLine("[ 4 ] Delete tutorial");
            Console.WriteLine("[ 5 ] List tutorial");
            Console.WriteLine("[ 6 ] Exit");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("--------------------------------------");
            Console.ResetColor();
            Console.WriteLine("\nChoose one of the above options: ");
        }

    }
}