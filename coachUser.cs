using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace finalProject
{
    public class coachUser
    {

        public string phone { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public int status { get; set; }

        public coachUser()
        {

        }
        public coachUser(SqlDataReader dr)
        {
            phone = dr.GetString(0);
            password = dr.GetString(1);
            name = dr.GetString(2);
            status = dr.GetInt32(3);


        }
    }
}