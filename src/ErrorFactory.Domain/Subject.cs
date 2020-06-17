namespace ErrorFactory.Domain
{
    public class Subject
    {
        public Subject(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; }
        public string Name { get; }
    }
}