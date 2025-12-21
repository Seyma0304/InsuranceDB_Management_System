using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebApplication.Models;
using WebApplication.Models.Data;

namespace WebApplication.Controllers
{
    public class UserController : Controller
    {
        private readonly Db _db;

        public UserController(IConfiguration configuration)
        {
            _db = new Db(configuration);
        }

        public IActionResult Index()
        {
            List<UserModel> users = new List<UserModel>();

            using (SqlConnection con = _db.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM User_", con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    users.Add(new UserModel
                    {
                        UserId = (int)dr["UserId"],
                        Email = dr["Email"].ToString() ?? string.Empty,
                        UserPassword = dr["UserPassword"].ToString() ?? string.Empty,
                        UserType = dr["UserType"].ToString() ?? string.Empty,
                        CreatedDate = (DateTime)dr["CreatedDate"]
                    });
                }
            }

            return View(users);
        }
    }
}