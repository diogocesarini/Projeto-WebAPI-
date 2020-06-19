using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Results
{
    public class FilmeResult
    {
        public string Titulo { get; set; }

        public string Sinopse { get; set; }

        public string Genero { get; set; }

        public string Ano { get; set; }

        public bool IsDisponivel { get; set; }

        public bool Ativo { get; set; }
    }
}
