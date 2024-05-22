using System;
using System.Linq;
using System.Threading.Tasks;

namespace PREFINAL_ASSIGNMENT_TWO_POKEMON.Models{
    public class Pokemon
    {
        public string Name { get; set; }
        public System.Collections.Generic.List<string> Moves { get; set; }
        public System.Collections.Generic.List<string> Abilities { get; set; }
    }
}
