using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynsPunkt_ApS.Models
{
    public class Supplier
    {
        public int CVRnummer { get; set; }
        public string supplierName { get; set; }
        public string adress { get; set; }
        public int zipCode { get; set; }
        public int phoneNumber { get; set; }
        public string mail { get; set; }
        public string bankName { get; set; }
        public int registrationNumber { get; set; }
        public int accountNumber { get; set; }
        public string billingInformation { get; set; }

        public Supplier(int cVRnummer, string supplierName, string adress, int zipCode, string mail, string billingInformation, int phoneNumber)
        {
            this.CVRnummer = cVRnummer;
            this.supplierName = supplierName;
            this.adress = adress;
            this.zipCode = zipCode;
            this.mail = mail;
            this.billingInformation = billingInformation;
            this.phoneNumber = phoneNumber;
        }

    }
}
