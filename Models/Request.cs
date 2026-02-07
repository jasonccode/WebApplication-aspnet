using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication_aspnet.Models
{
    public class Request
    {
        [Key]
        public int Id_request { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime Dcreated_at { get; set; }
        public string Status { get; set; } = null!;
    }
}
