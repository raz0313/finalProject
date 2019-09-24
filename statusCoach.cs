using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace finalProject
{
    public class statusCoach
    {
        public string phone { get; set; }
        public string fullName { get; set; }
        public statusCoach(SqlDataReader dr)
        {
            phone = dr.GetString(0);
            fullName = dr.GetString(1);

        }
    }
}
