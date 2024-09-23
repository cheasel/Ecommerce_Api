using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceApi.Model
{
    public class Vendor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        //public string BusinessLicense { get; set; }

        // Foreign Key
        public int UserId { get; set; }

        // Navigation property for one to one 
        public User User { get; set; }
    }
}