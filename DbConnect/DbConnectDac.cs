using DbConnect.DbContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace DbConnect
{
    public class DbConnectDac
    {
        public DbConnectDac()
        { }

        public IEnumerable<HistoryCalTemp> GetDbData(string targetDate)
        {
            BaseDataAccess access = new BaseDataAccess();

            List<DbParameter> paramList = new List<DbParameter>();
            paramList.Add(new SqlParameter() { ParameterName = "@Dt", Value = targetDate });
            DbDataReader reader = access.GetDataReader("GetTempByDate", paramList);
            List<HistoryCalTemp> valList = new List<HistoryCalTemp>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    HistoryCalTemp tempData = new HistoryCalTemp();
                    tempData.Dt = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("Date")));//("Date", DateTime.Now, Convert.ToDateTime);
                    tempData.High = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("HighestTemp")));//("HighestTemp", 0, Convert.ToDecimal);
                    tempData.Low = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("LowestTemp")));//("LowestTemp", 0, Convert.ToDecimal);
                    valList.Add(tempData);
                }
                reader.Close();
            }           
            return valList;
        }

    }

    public class HistoryCalTemp
    {

        public DateTime Dt { get; set; }

        public Decimal High { get; set; }

        public Decimal Low { get; set; }
    }
    
}
