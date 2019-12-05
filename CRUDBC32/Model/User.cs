using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDBC32.Model
{
    [Table("tb_m_user")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }

        public Role Roles { get; set; }

        public User() { }
        public User(string name, string email, string password, Role role)
        {
            this.Name = name;
            this.Email = email;
            this.Password = password;
            this.Roles = role;
        }
    }
}
