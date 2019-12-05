﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDBC32.Model
{
    [Table("tb_m_transaction")]
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        public int Total { get; set; }

        public DateTimeOffset CreateDate { get; set; }
 
        public Transaction()
        {
            this.CreateDate = DateTimeOffset.Now.LocalDateTime;
        }

        public Transaction(int total)
        {
            this.Total = total;
        }
    }
}