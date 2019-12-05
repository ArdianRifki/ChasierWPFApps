using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDBC32.Model
{
    [Table("tb_m_item")]
    public class Item
    {
        private string text1;
        private string text2;
        private string text3;

        //[Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public int Price { get; set; }

        //[ForeignKey("Supplier")]

        public Supplier Supplier { get; set; }
        public DateTimeOffset CreateDate { get; set; }


        public Item() { }

        public Item(string name, int stock, int price, Supplier supplier)
        {
            this.Name = name;
            this.Stock = stock;
            this.Price = price;
            this.Supplier = supplier;  
        }

        public Item(string text1, string text2, string text3)
        {
            this.text1 = text1;
            this.text2 = text2;
            this.text3 = text3;
        }
    }
}
