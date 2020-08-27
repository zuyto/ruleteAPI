

using System.Security.Cryptography.X509Certificates;

namespace rulete.Helpers
{
    public static class Enum
    {
        public enum Status
        { 
            Open = 1,
            Closed = 0,
            Create = -1
        }

        public enum Cash
        {
            MaxCash = 10000
        }
        public enum TypeBet
        {
            Number,
            Color
        }
        public enum Color
        {
            Red,
            Black
        }
       
    }
}
