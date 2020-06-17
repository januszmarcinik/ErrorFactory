using System.Collections.Generic;

namespace ErrorFactory.Domain
{
    public interface ISubjectsRepository
    {
        Subject GetById(int id);

        IEnumerable<Subject> GetAll();

        int GetNextId();

        void Add(Subject subject);

        void Remove(int id);
    }
}