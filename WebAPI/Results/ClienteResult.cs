using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Results
{
    public class ClienteResult
    {
        public string CPF { get; set; }

        public string Nome { get; set; }

        public string Endereco { get; set; }

        public string Numero { get; set; }

        public string Cidade { get; set; }

        public string Telefone { get; set; }

        public bool Ativo { get; set; }
    }
}
