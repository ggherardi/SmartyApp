using System;
using System.Collections.Generic;
using System.Text;

namespace Smarty.Models
{
    public class SmartTicket
    {
        public string CardId { get; set; }
        public decimal Credit { get; set; }
        public string TicketType { get; set; }
        public DateTime? CurrentValidation { get; set; }
        public DateTime? SessionValidation { get; set; }
        public decimal? SessionExpense { get; set; }
        public int? UserId { get; set; }
    }
}
