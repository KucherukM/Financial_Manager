using System;
using System.Windows.Input;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ClientApp.Entities
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [ForeignKey("Account")]
        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}

