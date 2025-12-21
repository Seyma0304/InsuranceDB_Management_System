using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.Models.Data;

namespace WebApplication.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(CustomerCreateModel model)
        {
            using (SqlConnection con = new Db().GetConnection())
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