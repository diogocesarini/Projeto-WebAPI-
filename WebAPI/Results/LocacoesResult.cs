using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Results
{
    public class LocacoesResult
    {
        public int Id { get; set; }

        public decimal Preco { get; set; }

        public string DataSaida { get; set; }

        public string DataEntrega { get; set; }

        public string DataEntregada { get; set; }

        public int IdFilme { get; set; }

        public int IdCliente { get; set; }

    }
}
