namespace Domain
{
    public interface ITutorialRepository
    {
        IEnumerable<Tutorial> GetTutorials();
        IList<Tutorial> GetLastFive();
        Tutorial GetById(int id);
        IEnumerable<Tutorial> GetTitle(string title);
        void Insert(Tutorial tutorial);
        void Update(Tutorial tutorial);
        void Delete(Tutorial tutorial);

    }
}
