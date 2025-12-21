using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebApplication.Models;
using WebApplication.Models.Data;

namespace WebApplication.Controllers
{
    public class CustomerController : Controller
    {
        private readonly Db _db;

        public CustomerController(IConfiguration configuration)
        {
            _db = new Db(configuration);
        }

        // GET: Customer
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CustomerCreateModel model)
        {
            using (SqlConnection con = _db.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_CreateCustomer", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // SP parameter mapping (8 params total)
                cmd.Parameters.AddWithValue("@UserEmail", model.Email);
                cmd.Parameters.AddWithValue("@UserPassword", model.UserPassword);
                cmd.Parameters.AddWithValue("@UserType", "Customer");

                cmd.Parameters.AddWithValue("@Phone", model.Phone);
                cmd.Parameters.AddWithValue("@AddressLine", model.AddressLine);
                cmd.Parameters.AddWithValue("@CustomerType", model.CustomerType);

                // Person ise FullName = FirstName + LastName
                if (model.CustomerType == "Person")
                {
                    string fullName = model.FirstName + " " + model.LastName;

                    cmd.Parameters.AddWithValue("@FullNameOrCompany", fullName);
                    cmd.Parameters.AddWithValue("@IdOrTaxNo", model.NationalIdNo);
                }
                else // Company
                {
                    cmd.Parameters.AddWithValue("@FullNameOrCompany", model.CompanyName);
                    cmd.Parameters.AddWithValue("@IdOrTaxNo", model.CompanyTaxNo);
                }

                con.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }
    }
}