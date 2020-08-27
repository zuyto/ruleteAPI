

using System;

namespace rulete.Models
{
    public class BetModel
    {
        public int idBet { get; set; }
        public float cashBet { get; set; }
        public string colorBet { get; set; }
        public int numberBet { get; set; }
        public int idGambler { get; set; }
        public int idRulette { get; set; }
        public DateTime betDate { get; set; }
    }
}
