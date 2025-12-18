using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication_aspnet.Models
{
    public class User
    {
        [Key]
        public int Nid_user { get; set; }
        public string Sname { get; set; } = null!;
        public string Slast_name { get; set; } = null!;
        public string Semail { get; set; } = null!;
        public DateTime Dcreated_at { get; set; }
        public string Saddress { get; set; } = null!;
        public int Nphone { get; set; }
    }
}
