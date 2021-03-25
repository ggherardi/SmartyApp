using System;
using System.Collections.Generic;
using System.Text;

namespace Smarty.Models
{
    public class UserCredentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class UserRegistration
    {
        public UserCredentials Credentials { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }

    public class CreditRecharge
    {
        public string TicketId { get; set; }
        public decimal Amount { get; set; }
    }
}
