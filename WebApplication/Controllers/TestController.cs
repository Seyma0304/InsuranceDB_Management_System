using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebApplication.Models.Data;

namespace WebApplication.Controllers
{
    public class TestController : Controller
    {
        private readonly Db _db;

        public TestController(IConfiguration configuration)
        {
            _db = new Db(configuration);
        }

        // Test URL: http://localhost:5000/Test/DatabaseConnection
        public IActionResult DatabaseConnection()
        {
            try
            {
                using (SqlConnection con = _db.GetConnection())
                {
                    con.Open();
                    
                    var serverVersion = con.ServerVersion;
                    var database = con.Database;
                    
                    ViewBag.Message = "✅ Bağlantı başarılı!";
                    ViewBag.ServerVersion = serverVersion;
                    ViewBag.Database = database;
                    ViewBag.Status = "success";
                    
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "❌ Bağlantı hatası!";
                ViewBag.Error = ex.Message;
                ViewBag.Status = "error";
            }

            return View();
        }
    }
}
