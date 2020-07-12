namespace ErrorFactory.Domain
{
    public class SubjectsRepositorySingleton
    {
        private static ISubjectsRepository _instance;

        public static ISubjectsRepository GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SubjectsRepository(new[]
                {
                    new Subject(1, "Inżynieria systemów informatycznych"),
                    new Subject(2, "Metody wytwarzania oprogramowania"),
                    new Subject(3, "Sztuczne sieci neuronowe"),
                    new Subject(4, "Wzorce projektowe")
                });
            }

            return _instance;
        }
    }
}