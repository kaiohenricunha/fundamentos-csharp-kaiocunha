namespace Domain
{
    public class Tutorial
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Instructor { get; set; }
        public DateTime MaxDate { get; set; }
        public bool Free { get; set; }
        public double Price { get; set; }
        public int TotalHours { get; set; }
        public DateTime RegisterDate { get; set; }


        public Tutorial(int id, string title, string instructor, DateTime maxDate, int totalHours)
        {
            Id = id;
            Title = title ?? throw new ArgumentException("The course title wasn't informed.");
            Instructor = instructor ?? throw new ArgumentException("Course instructor wasn't informed");
            MaxDate = maxDate;
            TotalHours = totalHours;
            RegisterDate = RegistrationDate();
            IsFree(maxDate);
        }

        public DateTime RegistrationDate()
        {
            DateTime registerDate = DateTime.Now;
            return registerDate;
        }

        public string GetResumeData() => string.Format("Id: {0} Title: {1} Instructor: {2}", Id, Title, Instructor);

        public DateTime GetDataRegister()
        {
            return RegisterDate;
        }

        public void IsFree(DateTime maxDate)
        {
            string format = "dd/MM/yyyy";

            DateTime date = DateTime.Today;
            string date_string = date.ToString("dd/MM/yyyy");
            DateTime date_updated = DateTime.ParseExact(date_string, format, null);

            string freePeriodString = maxDate.ToString("dd/MM/yyyy");
            DateTime freeDate = DateTime.ParseExact(freePeriodString, format, null);

            int compare = DateTime.Compare(date_updated, freeDate);

            if (compare < 0)
            {
                var diff_days = maxDate - date_updated;
                Free = true;
                Console.WriteLine("Price: R$ {0}", Price);
                Console.WriteLine("Free subscription for {0} ends in {1} days.", Title, diff_days.Days);
            }

            if (compare > 0 || compare == 0)
            {
                if (compare > 0)
                {
                    double price = 25;
                    Price = price;
                    Console.WriteLine("Price: R$ {0}", Price);
                    Console.WriteLine("Free subscription for {0} is over.", Title);
                }

                else
                {
                    Console.WriteLine("Price: R$ {0}", Price);
                    Console.WriteLine("Free subscription period for {0} ends today.", Title);
                }
            }

        }

        public override string ToString()
        {
            return $"[Id]:{Id} " +
                   $"[Title]:{Title} " +
                   $"[Instructor]:{Instructor} " +
                   $"[Free until]:{MaxDate: dd//MM/yyyy} " +
                   $"[Free]:{Free} " +
                   $"[Price]:{Price} " +
                   $"[Total Hours]:{TotalHours}";
        }

        public string ToCsv()
        {
           return $"{Id};" +
                  $"{Title};" +
                  $"{Instructor};" +
                  $"{MaxDate:dd/MM/yyyy};" +
                  $"{Free};" +
                  $"{Price};" +
                  $"{TotalHours};";
        }
    }
}