using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.Models.Data;

namespace WebApplication.Controllers
{
    public class UserController : Controller
    {
        Db db = new Db();

        public ActionResult Index()
        {
            List<UserModel> users = new List<UserModel>();

            using (SqlConnection con = db.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM User_", con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    users.Add(new UserModel
                    {
                        UserId = (int)dr["UserId"],
                        Email = dr["Email"].ToString(),
                        UserPassword = dr["UserPassword"].ToString(),
                        UserType = dr["UserType"].ToString(),
                        CreatedDate = (System.DateTime)dr["CreatedDate"]
                    });
                }
            }

            return View(users);
        }
    }
}