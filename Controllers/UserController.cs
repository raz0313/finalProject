using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace finalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value32321", "value2" };
        }
        [HttpGet("pullCoachList")]
       public List<statusCoach> pullCoachList()
        {
            string sql = "SELECT phone , fullName , Status FROM coachUser";
            List<statusCoach> statusCoachs = new List<statusCoach>();
           
            DB.pullFromDB(sql, (cmd) => { },
                (dr) => statusCoachs.Add(new statusCoach(dr)));
            return statusCoachs;

        }



        [HttpGet("status")]
        public Boolean status(string phone , int statusNum)
        { 
            string sql = "SELECT COUNT (status)  FROM coachUser WHERE phone = @phone AND status = @status ";
          return DB.ValidateUser(sql, (cmd) => {
              cmd.Parameters.AddWithValue("@phone", phone);
              cmd.Parameters.AddWithValue("@status", statusNum);
              
              
              });


        }

        [HttpGet("singupCoach")]
        public Boolean Add(string phone , string password, string name)
        {

            if (DB.ValidateUser("SELECT COUNT (phone) FROM coachUser WHERE password = @password AND phone = @Phone",
                (cmd) =>
                {
                    cmd.Parameters.AddWithValue("@Phone", Convert.ToInt32(phone));
                    cmd.Parameters.AddWithValue("@password", password);

                }))
            {
                return false;
            }
            else
            {
              
                string sql = "INSERT INTO coachUser (phone , password , fullName ,status)  VALUES(@Phone , @password , @name , @status)";
            return  (  DB.ExecuteCommand(sql, (cmd) => {
                    cmd.Parameters.AddWithValue("@Phone", phone);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@status", 0);
                })==1);
            }            
        }

        [HttpGet("allUsers")]
        public List<userList> allUsers()
        {
            List<userList> Allusers = new List<userList>();
            string sql = "SELECT phoneUser , name , phoneCoach , fullName  FROM trainingUser , coachUser WHERE coachUser.phone = trainingUser.phoneCoach ";
            DB.pullFromDB(sql,(cmd)=> { } ,
                 (dr) => Allusers.Add(new userList(dr))
                );
            return Allusers;
        }

        [HttpGet("deleteCoach")]
        public Boolean deleteCoach(string phone)
        {
            string sql = "DELETE FROM coachUser WHERE phone = @phone";
           return (DB.ExecuteCommand(sql, (cmd) => cmd.Parameters.AddWithValue("@phone", phone)) ==1);
        }

        [HttpGet("updateCoach")]
        public Boolean updateCoach(int phone, int status) {
            string sql = "UPDATE coachUser  SET status =  @status WHERE phone = @phone";
            return (DB.ExecuteCommand(sql, (cmd) => {
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@status", status);
                
                }) ==1);

        }

        [HttpGet("chackUser")]
        public List<chackUsers> chackUser(string phone)
        {
            List<chackUsers> chach = new List<chackUsers>();
            string sql = "SELECT trainingUser.phoneUser , name , phoneCoach , fullName , date, shack , comment FROM trainingUser , coachUser , trainingRes WHERE coachUser.phone = trainingUser.phoneCoach AND  trainingRes.phoneUser = trainingUser.phoneUser AND trainingUser.phoneCoach =@phone AND  date IN (SELECT  MAX(date)  FROM trainingRes GROUP BY phoneUser)";
            DB.pullFromDB(sql,(cmd)=>cmd.Parameters.AddWithValue("@phone", phone),
                (dr)=> chach.Add(new chackUsers(dr))
                );
            return chach;
        }
        [HttpGet("pullStatus")]
        public List<statusCoach> pullStatuses()
        {
            List<statusCoach> statusCoachs = new List<statusCoach>();
            string sql = "SELECT phone , fullName ,status FROM coachUser WHERE status < 1";
            DB.pullFromDB(sql , (cmd)=> { },
                (dr)=>  statusCoachs.Add(new statusCoach(dr)) );
            return statusCoachs;
        }
        [HttpGet("pullUsers")]
        public List<userList> pullUsers(string Phone)
        {
            List<userList> users = new List<userList>();
            string sql = "SELECT phoneUser , name , phoneCoach , fullName FROM trainingUser , coachUser WHERE coachUser.phone = trainingUser.phoneCoach AND phoneCoach= @phone";
            DB.pullFromDB(sql, (cmd) => cmd.Parameters.AddWithValue("@phone", Phone),
                 (dr) => users.Add(new userList(dr))
                );
            return users;

        }
        [HttpPost("post")]
        public Boolean Addmeasurement([FromBody] Measurements m , string phoneUser)
        {
            string sql = "INSERT INTO trainingRes (phoneUser,date, Weight,water,fat,muscle,bodyStructure,BMR,BodyAge,FatBelly,BoneMass,chest,stomach,Legs,shack) VALUES (@phoneUser,@date, @Weight,@water,@fat,@muscle,@bodyStructure,@BMR,@BodyAge,@FatBelly,@BoneMass,@chest,@stomach,@Legs,@shack)";
          return ( DB.ExecuteCommand(sql, (cmd) => {
                cmd.Parameters.AddWithValue("@phoneUser",phoneUser);
                cmd.Parameters.AddWithValue("@date", m.date);
                cmd.Parameters.AddWithValue("@Weight", m.Weight);
                cmd.Parameters.AddWithValue("@water", m.water);
                cmd.Parameters.AddWithValue("@fat", m.fat);
                cmd.Parameters.AddWithValue("@muscle", m.muscle);
                cmd.Parameters.AddWithValue("@bodyStructure", m.bodyStructure);
                cmd.Parameters.AddWithValue("@BMR", m.BMR);
                cmd.Parameters.AddWithValue("@BodyAge", m.BodyAge);
                cmd.Parameters.AddWithValue("@FatBelly", m.FatBelly);
                cmd.Parameters.AddWithValue("@BoneMass", m.BoneMass);

                cmd.Parameters.AddWithValue("@chest", m.chest);
                cmd.Parameters.AddWithValue("@stomach", m.stomach);
                cmd.Parameters.AddWithValue("@Legs", m.Legs);
                cmd.Parameters.AddWithValue("@shack", m.shack);


            })==1);

        }




        [HttpGet("loginUser")]
        public Boolean loginUser(string phoneUser)
        {
            string sql = "SELECT COUNT (phoneUser) FROM trainingUser WHERE phoneUser= @Phone";
          return  DB.ValidateUser(sql,(cmd)=>
          {
              cmd.Parameters.AddWithValue("@phone", phoneUser);
          }


           );

        }

        [HttpGet("loginCoach")]
        public Boolean loginCoach(string phonecoach , string password)
        {

            return DB.ValidateUser("SELECT COUNT (phone) FROM coachUser WHERE password =  @password  AND status > 0 AND phone =  @Phone",
                (cmd) =>
                {
                    cmd.Parameters.AddWithValue("@Phone",phonecoach);
                    cmd.Parameters.AddWithValue("@password", password);

                });
        }

        [HttpGet("pullRes")]
        public List<Measurements> pullRes(string phoneUser)
        {
            List<Measurements> measurements = new List<Measurements>();
            string sql = "SELECT * FROM trainingRes WHERE phoneUser = @phone";
            DB.pullFromDB(sql, (cmd) => cmd.Parameters.AddWithValue("@phone", phoneUser),
                (dr) => measurements.Add(new Measurements(dr))
                );
            return measurements;
        }

        [HttpGet("addUser")]
        public Boolean singupUser(string phone , string name , string coachPhone)
        {
            if (DB.ValidateUser("SELECT phoneUser FROM trainingUser WHERE phoneUser= @Phone"
               , (cmd) =>
                {
                    cmd.Parameters.AddWithValue("@Phone", phone);
                })) {
                return false;

            }
            else
            {

                string sql = "INSERT INTO trainingUser (phoneUser , name , phoneCoach)  VALUES(@Phone , @name , @phoneCoach )";
                return (DB.ExecuteCommand(sql, (cmd) => {
                    cmd.Parameters.AddWithValue("@Phone", phone);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@phoneCoach", coachPhone);
                }) == 1);

            }
        }



    }
}
