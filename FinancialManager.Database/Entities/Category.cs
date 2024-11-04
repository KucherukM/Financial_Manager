using System.Collections.Generic;
using System.Windows.Input;
using System.ComponentModel.DataAnnotations;


namespace ClientApp.Entities
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public bool IsExpense { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}

