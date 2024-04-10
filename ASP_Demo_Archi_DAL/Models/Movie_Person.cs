using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP_Demo_Archi_DAL.Models
{
    internal class Movie_Person
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int MovieId { get; set; }
        public string Role { get; set; }
    }
}
