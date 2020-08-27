using rulete.Helpers;
using rulete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rulete.Persistence.IRepository
{
    public interface IRuletteRepository
    {
        GenericAnswer GetObjRulettes(RuletteModel dataRulette);
        GenericAnswer GetRulettes();
        GenericAnswer CreateRulette(RuletteModel dataRulette);
        GenericAnswer OpenRulette(RuletteModel dataRulette);
        GenericAnswer OpenBet(RuletteModel dataRulette, BetModel dataBet, GamblerModel dataGambler);
        GenericAnswer ClosedRulette(RuletteModel dataRulette);
    }
}
