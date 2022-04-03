﻿using System.Text;
using System.Text.RegularExpressions;
using Domain;

namespace Infra
{
    public class TutorialRepositoryFile : ITutorialRepository
    {
        public List<Tutorial> _tutorials = new List<Tutorial>();

        private readonly string _direc;
        private readonly string _fil = "TutorialRegister.txt";

        public TutorialRepositoryFile()
        {

            _direc = Directory.GetCurrentDirectory();
            CreateFiles();
            ReadFiles();
        }


        public void ReadFiles()
        {
            var tutorials = new List<Tutorial>();

            var path = $@"{_direc}\{_fil}";
            if (File.Exists(path))
            {
                using (
                    var openFile = File.OpenRead(path))
                {
                    using (var readText = new StreamReader(openFile))
                    {
                        while (readText.EndOfStream == false)
                        {
                            var read = readText.ReadLine();

                            if (read != null)
                            {
                                string[] aux = read.Split(';');
                                DateTime maxDate = DateTime.ParseExact(aux[3], "dd/MM/yyyy", null);
                                tutorials.Add(
                                    new Tutorial(
                                        int.Parse(aux[0]),
                                        aux[1],
                                        aux[2],
                                        maxDate,
                                        int.Parse(aux[4])
                                    // public Tutorial(int id, string title, string instructor, DateTime maxDate, int totalHours)
                                    )
                                    );
                            }
                        }

                        _tutorials = tutorials;
                    }
                }
            }
        }
        private void CreateFiles()
        {
            var path = $@"{_direc}\{_fil}";

            if (!File.Exists(path))
            {
                File.Create(path);
            }
        }


        public void Save()
        {
            string fileRoute = $@"{_direc}\{_fil}";
            var way = new List<string>();

            foreach (Tutorial tutorial in _tutorials)
            {
                way.Add(tutorial.ToCsv());
            }

            if (File.Exists(fileRoute))
            {
                File.WriteAllLines(fileRoute, way, Encoding.UTF8);
                ReadFiles();
            }
        }


        public void Insert(Tutorial tutorial)
        {
            var count = _tutorials.Any() ? _tutorials.Max(x => x.Id) + 1 : 1;
            tutorial.Id = count;
            _tutorials.Add(tutorial);
            Save();

        }

        public void Update(Tutorial tutorial)
        {
            var result = _tutorials.FirstOrDefault(x => x.Id == tutorial.Id);
            if (result != null)
            {
                _tutorials.Remove(result);
                _tutorials.Add(tutorial);
                Save();
            }
        }

        public void Delete(Tutorial tutorial)
        {
            var result = GetById(tutorial.Id);

            if (result != null)
            {
                _tutorials.Remove(result);
                Save();
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