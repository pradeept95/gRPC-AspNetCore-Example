using System.Collections.Generic;

namespace Application.Data.Model
{
    public class Employee
    {
        public Employee()
        {
            Addresses = new List<Address>();
            EmailAddresses = new List<EmailAddress>();
            Phones = new List<PhoneNumber>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<Address> Addresses { get; set; }
        public List<EmailAddress> EmailAddresses { get; set; }
        public List<PhoneNumber> Phones { get; set; }
    }
}
