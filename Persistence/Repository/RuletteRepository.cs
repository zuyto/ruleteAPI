using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using rulete.Helpers;
using rulete.Models;
using rulete.Persistence.IRepository;
using rulete.Persistence.MapRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Enum = rulete.Helpers.Enum;

namespace rulete.Persistence.Repository
{
    public class RuletteRepository : IRuletteRepository
    {
        protected readonly ConnectionManager connectionManager;
        protected readonly AppSettings localSettings;
        protected const string insertRulette = "SpInsertRulette";
        protected const string updateRulette = "SpUpdateRulette";
        protected const string insertBet = "SpInsertBet";
        protected const string getObjectRulette = "SpGetObjRulette";
        protected const string getRulette = "SpGetRulette";

        public RuletteRepository(IOptions<AppSettings> appSettings)
        {
            connectionManager = new ConnectionManager(appSettings);
            localSettings = appSettings.Value;
        }
        public GenericAnswer GetObjRulettes(RuletteModel dataRulette)
        {
            GenericAnswer response;
            SqlParameter[] parameters =
                { new SqlParameter {Value = dataRulette.idRulette, ParameterName = RuletteMapping.idRulette } };
            response = connectionManager.GetObject<RuletteModel>(getObjectRulette, parameters);

            return response;
        }
        public GenericAnswer GetRulettes()
        {
            GenericAnswer response;
            SqlParameter[] parameters = null;
            response = connectionManager.GetList<RuletteModel>(getRulette, parameters);

            return response;
        }
        public GenericAnswer CreateRulette(RuletteModel dataRulette)
        {
            GenericAnswer response = new GenericAnswer();
            SqlParameter[] parameters =
                { new SqlParameter {Value = Enum.Status.Create, ParameterName = RuletteMapping.ruletteStatus },
                  new SqlParameter {SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output, ParameterName = RuletteMapping.OutIdRulette }
                };
            string messageError = string.Empty;
            connectionManager.AcctionRun(insertRulette, ref messageError, parameters);
            if (Convert.ToInt32(parameters[parameters.Length - 1].Value.ToString()) > 0)
            {
                dataRulette.idRulette = Convert.ToInt32(parameters[parameters.Length - 1].Value.ToString());
            }
            if (!string.IsNullOrEmpty(messageError))
            {
                response.error = messageError;
                response.successful = false;

                return response;
            }
            response.error = messageError;
            response.entity = dataRulette;
            response.successful = true;

            return response;
        }
        public GenericAnswer OpenRulette(RuletteModel dataRulette)
        {
            GenericAnswer response = new GenericAnswer();
            SqlParameter[] parameters =
                {
                  new SqlParameter {Value = dataRulette.idRulette, ParameterName = RuletteMapping.idRulette },
                  new SqlParameter {Value = Enum.Status.Open, ParameterName = RuletteMapping.ruletteStatus },

                };
            string messageError = string.Empty;
            connectionManager.AcctionRun(updateRulette, ref messageError, parameters);
            if (!string.IsNullOrEmpty(messageError))
            {
                response.error = messageError;
                response.successful = false;

                return response;
            }
            response.error = "Apertura Exitosa";
            response.entity = dataRulette;
            response.successful = true;

            return response;
        }
        public GenericAnswer OpenBet(RuletteModel dataRulette, BetModel dataBet, GamblerModel dataGambler)
        {
            GenericAnswer response = new GenericAnswer();
            string colorBet = string.Empty;
            if (dataBet.cashBet > Convert.ToInt32(Enum.Cash.MaxCash))
            {
                response.error = "Supera el monto máximo de apuesta = " + Enum.Cash.MaxCash;

                return response;
            }

            SqlParameter[] parameters =
                {
                  new SqlParameter {Value = dataRulette.idRulette, ParameterName = RuletteMapping.idRulette },
                  new SqlParameter {Value = dataGambler.idGambler, ParameterName = GamblerMapping.idGambler },
                  new SqlParameter {Value = dataBet.cashBet, ParameterName = BetMapping.cashBet },
                  new SqlParameter {Value = dataBet.numberBet, ParameterName = BetMapping.numberBet },
                  new SqlParameter {Value = dataBet.colorBet, ParameterName = BetMapping.colorBet },

                };
            string messageError = string.Empty;
            connectionManager.AcctionRun(updateRulette, ref messageError, parameters);
            if (!string.IsNullOrEmpty(messageError))
            {
                response.error = messageError;
                response.successful = false;

                return response;
            }
            response.error = "Apertura Exitosa";
            response.entity = dataRulette;
            response.successful = true;

            return response;
        }
        public GenericAnswer ClosedRulette(RuletteModel dataRulette)
        {
            GenericAnswer response = new GenericAnswer();
            SqlParameter[] parameters =
                {
                  new SqlParameter {Value = dataRulette.idRulette, ParameterName = RuletteMapping.idRulette },
                  new SqlParameter {Value = Enum.Status.Closed, ParameterName = RuletteMapping.ruletteStatus },

                };
            string messageError = string.Empty;
            connectionManager.AcctionRun(updateRulette, ref messageError, parameters);
            if (!string.IsNullOrEmpty(messageError))
            {
                response.error = messageError;
                response.successful = false;

                return response;
            }
            GenericAnswer getBet = GetObjRulettes(dataRulette);
            if (!getBet.successful)
            {
                response.error = getBet.error;
                response.successful = false;
                return response;
            }
            response.error = getBet.error;
            response.entity = getBet.entity;
            response.successful = true;

            return response;
        }

    }
}
