using System.Collections.Generic;
using System.Windows.Input;
using System.ComponentModel.DataAnnotations;

namespace ClientApp.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<Account> Accounts { get; set; }
        public ICollection<Budget> Budgets { get; set; }
    }
}

