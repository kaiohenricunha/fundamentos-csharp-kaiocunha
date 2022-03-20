using System;
using System.Collections.Generic;
using System.Linq;

namespace _01_MusicaTutoriais
{
    internal class TutorialRepository
    {
        private static List<Tutorial> tutorialList = new List<Tutorial>();

        public static List<Tutorial> GetAll()
        {
            return tutorialList;
        }

        public static List<Tutorial> GetBySearch(string searchString)
        {
            List<Tutorial> retorno = null;

            if (!String.IsNullOrEmpty(searchString))
            {
                retorno = tutorialList.Where(p => p.GetTitulo().Contains(searchString)).ToList();
            }

            if (retorno == null) retorno = new List<Tutorial>();

            return retorno;
        }

        public static void Create(Tutorial tutorial)
        {
            tutorialList.Add(tutorial);
        }
    }
}
