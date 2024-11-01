using System.Windows.Input;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;


namespace ClientApp.Entities
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public decimal Balance { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}

