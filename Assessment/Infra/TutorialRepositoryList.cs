using System.Text.RegularExpressions;
using Domain;

namespace Infra
{
    public sealed class TutorialRepositoryList : ITutorialRepository
    {
        private static readonly List<Tutorial> _tutorials = new List<Tutorial>();

        public void Insert(Tutorial tutorial)
        {
            var count = _tutorials.Any() ? _tutorials.Max(x => x.Id) + 1 : 1;
            tutorial.Id = count;
            _tutorials.Add(tutorial);
        }

        public void Update(Tutorial tutorial)
        {
            var result = _tutorials.FirstOrDefault(x => x.Id == tutorial.Id);
            if (result != null)
            {
                _tutorials.Remove(result);
                _tutorials.Add(tutorial);
            }
        }

        public void Delete(Tutorial tutorial)
        {
            var result = GetById(tutorial.Id);

            if (result != null)
            {
                _tutorials.Remove(result);

            }
        }

        public IEnumerable<Tutorial> GetTutorials()
        {
            return _tutorials;
        }

        public Tutorial GetById(int id)
        {
            return _tutorials.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Tutorial> GetTitle(string title)
        {

            return _tutorials.Where(x => Regex.IsMatch(
                x.Title, title, RegexOptions.IgnoreCase) || Regex.IsMatch(
                x.Instructor, title, RegexOptions.IgnoreCase));
        }

        public IList<Tutorial> GetLastFive()
        {
            return _tutorials.OrderByDescending(x => x.GetDataRegister()).Take(5).ToList();
        }
    }
}
