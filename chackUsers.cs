using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace finalProject
{
    public class chackUsers
    {
        public string phoneUser { get; set; }
        public string name { get; set; }
        public string phoneCoach { get; set; }
        public string fullName { get; set; }
        public DateTime date { get; set; }
        public int shack { get; set; }
        public string comment { get; set; }

        public chackUsers(SqlDataReader dr)
        {
            phoneUser = dr.GetString(0);
            name = dr.GetString(1);
            phoneCoach = dr.GetString(2);
            fullName = dr.GetString(3);
            date = dr.GetDateTime(4);
            shack = dr.GetInt32(5);
            comment = dr.GetStringOrNull(6);
        }
    }
}
