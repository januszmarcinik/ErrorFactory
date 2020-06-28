using ErrorFactory.Domain;

namespace ErrorFactory.Tests
{
    public static class FakeSubjects
    {
        public static readonly Subject[] List = 
        {
            new Subject(1, "Inżynieria systemów informatycznych"),
            new Subject(2, "Metody wytwarzania oprogramowania"),
            new Subject(3, "Sztuczne sieci neuronowe"),
            new Subject(4, "Wzorce projektowe")
        };
    }
}