﻿using CRUDBC32.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDBC32.Context
{
    public class MyContext : DbContext
    {
        public MyContext() : base("MyContext") { }

        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionItem> TransactionItem{ get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
