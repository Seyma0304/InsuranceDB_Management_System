using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class CustomerCreateModel
    {
        // User info
        public string Email { get; set; }
        public string UserPassword { get; set; }

        // Customer info
        public string Phone { get; set; }

        // Address
        public string AddressLine { get; set; }

        // Customer type
        public string CustomerType { get; set; }  // "Person" or "Company"

        // Person fields
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalIdNo { get; set; }

        // Company fields
        public string CompanyName { get; set; }
        public string CompanyTaxNo { get; set; }
    }

}