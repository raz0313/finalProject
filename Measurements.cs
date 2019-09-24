using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace finalProject
{
    public class Measurements
    {
        public string phoneUser { get; set; }
        public DateTime date { get; set; }
        public int Weight { get; set; }
        public int water { get; set; }
        public int fat { get; set; }
        public int muscle { get; set; }
        public int bodyStructure { get; set; }
        public int BMR { get; set; }
        public int BodyAge { get; set; }
        public int FatBelly { get; set; }
        public int BoneMass { get; set; }
        public int chest { get; set; }
        public int stomach { get; set; }
        public int Legs { get; set; }
        public int shack { get; set; }

        public Measurements()
        {

        }
        public Measurements(SqlDataReader dr)
        {
            phoneUser = dr.GetString(0);
            date = dr.GetDateTime(1);
            Weight = dr.GetInt32(2);
            water = dr.GetInt32(3);
            fat = dr.GetInt32(4);
            muscle = dr.GetInt32(5);
            bodyStructure = dr.GetInt32(6);
            BMR = dr.GetInt32(7);
            BodyAge = dr.GetInt32(8);
            FatBelly = dr.GetInt32(9);
           BoneMass  = dr.GetInt32(10);

            chest = dr.GetInt32(11);
            stomach = dr.GetInt32(12);
            Legs = dr.GetInt32(13);
            shack = dr.GetInt32(14);
        }
    }
}
