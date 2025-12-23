using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebApplication.Models;
using WebApplication.Models.Data;

namespace WebApplication.Controllers
{
    /// <summary>
    /// Customer management controller for .NET Core
    /// </summary>
    public class CustomerController : Controller
    {
        private readonly Db _db;

        public CustomerController(IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(configuration);
            _db = new Db(configuration);
        }

        // GET: Customer
        public IActionResult Index()
        {
            return View();
        }

        // GET: Customer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CustomerCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                using (SqlConnection con = _db.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_CreateCustomer", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        // SP parameter mapping (8 params total)
                        cmd.Parameters.AddWithValue("@UserEmail", model.Email ?? string.Empty);
                        cmd.Parameters.AddWithValue("@UserPassword", model.UserPassword ?? string.Empty);
                        cmd.Parameters.AddWithValue("@UserType", "Customer");
                        cmd.Parameters.AddWithValue("@Phone", model.Phone ?? string.Empty);
                        cmd.Parameters.AddWithValue("@AddressLine", model.AddressLine ?? string.Empty);
                        cmd.Parameters.AddWithValue("@CustomerType", model.CustomerType ?? string.Empty);

                        // Person ise FullName = FirstName + LastName
                        if (model.CustomerType == "Person")
                        {
                            string fullName = $"{model.FirstName} {model.LastName}".Trim();
                            cmd.Parameters.AddWithValue("@FullNameOrCompany", fullName);
                            cmd.Parameters.AddWithValue("@IdOrTaxNo", model.NationalIdNo ?? string.Empty);
                        }
                        else // Company
                        {
                            cmd.Parameters.AddWithValue("@FullNameOrCompany", model.CompanyName ?? string.Empty);
                            cmd.Parameters.AddWithValue("@IdOrTaxNo", model.CompanyTaxNo ?? string.Empty);
                        }

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                TempData["SuccessMessage"] = "Müşteri başarıyla oluşturuldu!";
                return RedirectToAction(nameof(Index));
            }
            catch (SqlException ex)
            {
                ModelState.AddModelError(string.Empty, $"Veritabanı hatası: {ex.Message}");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Hata: {ex.Message}");
            }

            return View(model);
        }
    }
}