using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication_aspnet.Models
{
    public class City
    {
        [Key]
        public int Nid_city{ get; set; }
        public string Sname { get; set; } = null!;
        public DateTime Dcreated_at { get; set; }
    }
}