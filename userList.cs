using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace finalProject
{
    public class userList
    {
        public string phoneUser { get; set; }
        public string name { get; set; }
        public string phoneCoach { get; set; }
        public string fullName { get; set; }

        public userList(SqlDataReader dr)
        {
            phoneUser = dr.GetString(0);
            name = dr.GetString(1);
            phoneCoach = dr.GetString(2);
            fullName = dr.GetString(3);
        }
    }
}
