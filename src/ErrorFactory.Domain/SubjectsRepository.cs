using System.Collections.Generic;
using System.Linq;

namespace ErrorFactory.Domain
{
    public class SubjectsRepository : ISubjectsRepository
    {
        private readonly List<Subject> _subjects;

        public SubjectsRepository(IEnumerable<Subject> subjects)
        {
            _subjects = subjects.ToList();
        }

        public Subject GetById(int id) => 
            _subjects.SingleOrDefault(x => x.Id == id);

        public IEnumerable<Subject> GetAll() => 
            _subjects.ToList();

        public int GetNextId() =>
            _subjects
                .Select(x => x.Id)
                .DefaultIfEmpty(0)
                .Max(x => x) + 1;

        public void Add(Subject subject) =>
            _subjects.Add(subject);

        public void Remove(int id) => 
            _subjects.RemoveAll(x => x.Id == id);
    }
}