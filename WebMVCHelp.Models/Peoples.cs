using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMVCHelp.Models
{
    public class Peoples
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SexoId { get; set; }
        public Sexo Sexo { get; set; }
    }
}
