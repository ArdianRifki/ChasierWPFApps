using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDBC32.Model
{
    [Table("tb_m_transactionitem")]
    public class TransactionItem
    {
        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int SubTotal { get; set; }

        public Item Items { get; set; }
        public Transaction Transactions { get; set; }

        public TransactionItem() { }

        public TransactionItem(Item items, Transaction transactions, int quantity, int subtotal)
        {
            this.Items = items;
            this.Transactions = transactions;
            this.Quantity = quantity;
            this.SubTotal = subtotal;
        }
    }
}
