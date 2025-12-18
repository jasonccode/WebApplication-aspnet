using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication_aspnet.Models
{
    public class Office
    {
        [Key]
        public int Nid_office{ get; set; }
        public string Sname { get; set; } = null!;
        public string Saddress { get; set; } = null!;
        public DateTime Dcreated_at { get; set; }
    }
}