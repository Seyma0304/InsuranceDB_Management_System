using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using WebApplication.Models;
using WebApplication.Models.Data;

namespace WebApplication.Controllers
{
    public class ContractController : Controller
    {
        private readonly Db _db;

        public ContractController(IConfiguration configuration)
        {
            _db = new Db(configuration);
        }

        // GET: Contract/Operations - Main Contract Operations Page
        public IActionResult Operations()
        {
            var viewModel = new ContractOperationsViewModel
            {
                TariffList = GetTariffList(),
                PolicyList = new List<PolicyModel>()
            };
            
            return View(viewModel);
        }

        // GET: Contract/Create - Create New Contract Page
        public IActionResult Create()
        {
            var viewModel = new CreateContractViewModel
            {
                CustomerList = GetCustomerList(),
                TariffList = GetTariffListItems(),
                StartDate = DateTime.Now
            };
            
            return View(viewModel);
        }

        // POST: Contract/Create
        [HttpPost]
        public IActionResult Create(CreateContractViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.CustomerList = GetCustomerList();
                model.TariffList = GetTariffListItems();
                return View(model);
            }

            try
            {
                using (SqlConnection con = _db.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("sp_CreateContract", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CustomerId", model.CustomerId);
                    cmd.Parameters.AddWithValue("@TariffId", model.TariffId);
                    cmd.Parameters.AddWithValue("@StartDate", model.StartDate);
                    cmd.Parameters.AddWithValue("@Premium", model.Premium);

                    // OUTPUT parameter for new ContractId
                    SqlParameter outputParam = new SqlParameter("@NewContractId", SqlDbType.Int);
                    outputParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outputParam);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    int newContractId = (int)outputParam.Value;
                    TempData["SuccessMessage"] = $"Sözleşme başarıyla oluşturuldu! Sözleşme No: {newContractId}";
                    
                    return RedirectToAction("Operations");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Hata: {ex.Message}");
                model.CustomerList = GetCustomerList();
                model.TariffList = GetTariffListItems();
                return View(model);
            }
        }

        // GET: Contract/GetTariffNames - API endpoint to get tariff names using sp_getTariffName
        [HttpGet]
        public JsonResult GetTariffNames()
        {
            List<string> tariffNames = new List<string>();

            try
            {
                using (SqlConnection con = _db.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("sp_getTariffName", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string tariffName = reader["TariffName"].ToString() ?? "";
                        if (!string.IsNullOrEmpty(tariffName))
                        {
                            tariffNames.Add(tariffName);
                        }
                    }

                    reader.Close();
                    con.Close();
                }

                return Json(new { success = true, data = tariffNames });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // GET: Contract/GetContractNumbers - API endpoint to get contract numbers using sp_getContractNo
        [HttpGet]
        public JsonResult GetContractNumbers()
        {
            List<string> contractNumbers = new List<string>();

            try
            {
                using (SqlConnection con = _db.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("sp_getContractNo", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string contractNo = reader["ContractNo"].ToString() ?? "";
                        if (!string.IsNullOrEmpty(contractNo))
                        {
                            contractNumbers.Add(contractNo);
                        }
                    }

                    reader.Close();
                    con.Close();
                }

                return Json(new { success = true, data = contractNumbers });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // GET: Contract/GetContractsByTariff - Example endpoint that uses selected tariff name
        // This demonstrates how to use the selected tariff as a parameter for other operations
        [HttpGet]
        public JsonResult GetContractsByTariff(string tariffName)
        {
            if (string.IsNullOrEmpty(tariffName))
            {
                return Json(new { success = false, message = "Tarife adı belirtilmedi" });
            }

            List<object> contracts = new List<object>();

            try
            {
                using (SqlConnection con = _db.GetConnection())
                {
                    // Example: Call a stored procedure that accepts tariff name
                    // Replace with your actual stored procedure name
                    SqlCommand cmd = new SqlCommand("sp_GetContractsByTariffName", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TariffName", tariffName);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        contracts.Add(new
                        {
                            contractId = reader["ContractId"],
                            contractNo = reader["ContractNo"].ToString(),
                            customerName = reader["CustomerName"].ToString(),
                            startDate = ((DateTime)reader["StartDate"]).ToString("dd.MM.yyyy"),
                            premium = reader["Premium"]
                        });
                    }

                    reader.Close();
                    con.Close();
                }

                return Json(new { success = true, data = contracts, tariffName = tariffName });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // POST: Contract/Search - Search contracts by filters using sp_getContractOperation
        [HttpPost]
        public IActionResult SearchContracts(string? tariffName, string? contractNo)
        {
            List<PolicyModel> policies = new List<PolicyModel>();

            try
            {
                // At least one parameter should be provided
                if (!string.IsNullOrEmpty(tariffName) || !string.IsNullOrEmpty(contractNo))
                {
                    using (SqlConnection con = _db.GetConnection())
                    {
                        SqlCommand cmd = new SqlCommand("sp_getContractOperation", con);
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add parameters as nullable
                        cmd.Parameters.AddWithValue("@TariffName", 
                            string.IsNullOrEmpty(tariffName) ? (object)DBNull.Value : tariffName);
                        
                        cmd.Parameters.AddWithValue("@ContractNo", 
                            string.IsNullOrEmpty(contractNo) ? (object)DBNull.Value : contractNo);

                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            policies.Add(new PolicyModel
                            {
                                PolicyId = (int)reader["PolicyId"],
                                PolicyNo = reader["PolicyNo"].ToString() ?? "",
                                StartDate = (DateTime)reader["StartDate"],
                                EndDate = (DateTime)reader["EndDate"],
                                PremiumAmount = (decimal)reader["PremiumAmount"],
                                TariffName = reader["TariffName"].ToString() ?? "",
                                TariffCode = reader["TariffCode"].ToString() ?? "",
                                ContractName = reader["ContractName"].ToString() ?? "",
                                ContractNo = reader["ContractNo"].ToString() ?? ""
                            });
                        }

                        reader.Close();
                        con.Close();
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Lütfen Tarife veya Sözleşme No seçin.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Arama sırasında hata: {ex.Message}";
            }

            var viewModel = new ContractOperationsViewModel
            {
                TariffList = GetTariffList(),
                PolicyList = policies,
                SelectedTariffName = tariffName,
                SearchContractNo = contractNo
            };

            return View("Operations", viewModel);
        }

        // POST: Contract/DeletePolicy - Delete a policy by PolicyId
        [HttpPost]
        public JsonResult DeletePolicy(int policyId)
        {
            try
            {
                using (SqlConnection con = _db.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("sp_DeletePolicy", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PolicyID", policyId);

                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    con.Close();

                    if (rowsAffected > 0)
                    {
                        return Json(new { success = true, message = "Poliçe başarıyla silindi." });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Poliçe bulunamadı." });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Silme işlemi sırasında hata: {ex.Message}" });
            }
        }

        // GET: Contract/Details/{id}
        public IActionResult Details(int id)
        {
            ContractModel? contract = null;

            try
            {
                using (SqlConnection con = _db.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("sp_GetContractById", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ContractId", id);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        contract = new ContractModel
                        {
                            ContractId = (int)reader["ContractId"],
                            ContractNo = reader["ContractNo"].ToString() ?? "",
                            CustomerId = (int)reader["CustomerId"],
                            CustomerName = reader["CustomerName"].ToString(),
                            TariffId = (int)reader["TariffId"],
                            TariffName = reader["TariffName"].ToString(),
                            StartDate = (DateTime)reader["StartDate"],
                            EndDate = (DateTime)reader["EndDate"],
                            Premium = (decimal)reader["Premium"],
                            Status = reader["Status"].ToString() ?? "",
                            CreatedDate = (DateTime)reader["CreatedDate"]
                        };
                    }

                    reader.Close();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Hata: {ex.Message}";
            }

            return View(contract);
        }

        // Helper Methods
        private List<TariffModel> GetTariffList()
        {
            List<TariffModel> tariffs = new List<TariffModel>();

            using (SqlConnection con = _db.GetConnection())
            {
                // Gerçek tablo kolonlarını kullan: TariffId, TariffName, PlanType, TariffCode, MonthlyRate
                SqlCommand cmd = new SqlCommand("SELECT TariffId, TariffName, PlanType, TariffCode, MonthlyRate FROM Tariff", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    tariffs.Add(new TariffModel
                    {
                        TariffId = (int)reader["TariffId"],
                        TariffName = reader["TariffName"].ToString() ?? "",
                        Description = reader["PlanType"].ToString() ?? "", // PlanType'ı Description olarak kullan
                        BasePrice = reader["MonthlyRate"] != DBNull.Value ? Convert.ToDecimal(reader["MonthlyRate"]) : 0,
                        CoverageType = reader["TariffCode"].ToString() ?? "",
                        DurationMonths = 12, // Default değer
                        IsActive = true
                    });
                }

                reader.Close();
                con.Close();
            }

            return tariffs;
        }

        private List<TariffListItem> GetTariffListItems()
        {
            return GetTariffList().Select(t => new TariffListItem
            {
                TariffId = t.TariffId,
                TariffName = t.TariffName,
                BasePrice = t.BasePrice,
                DurationMonths = t.DurationMonths
            }).ToList();
        }

        private List<CustomerListItem> GetCustomerList()
        {
            List<CustomerListItem> customers = new List<CustomerListItem>();

            using (SqlConnection con = _db.GetConnection())
            {
                SqlCommand cmd = new SqlCommand(@"
                    SELECT c.CustomerId, c.CustomerType, 
                           COALESCE(p.FirstName + ' ' + p.LastName, comp.CompanyName) as DisplayName
                    FROM Customer c
                    LEFT JOIN Person p ON c.CustomerId = p.CustomerId
                    LEFT JOIN Company comp ON c.CustomerId = comp.CustomerId
                ", con);
                
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    customers.Add(new CustomerListItem
                    {
                        CustomerId = (int)reader["CustomerId"],
                        DisplayName = reader["DisplayName"].ToString() ?? "",
                        CustomerType = reader["CustomerType"].ToString() ?? ""
                    });
                }

                reader.Close();
                con.Close();
            }

            return customers;
        }
    }

    // ViewModel for Operations page
    public class ContractOperationsViewModel
    {
        public List<TariffModel> TariffList { get; set; } = new();
        public List<PolicyModel> PolicyList { get; set; } = new();
        public string? SelectedTariffName { get; set; }
        public string? SearchContractNo { get; set; }
    }
}
