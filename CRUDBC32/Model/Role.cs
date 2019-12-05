using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDBC32.Model
{
    [Table ("tb_m_role")]
    public class Role
    {
        [Key]
        public int ID { get; set; }
        public String RoleName { get; set; }

        public Role() { }

        public Role(string name)
        {
            this.RoleName = name;
        }
    }
}
