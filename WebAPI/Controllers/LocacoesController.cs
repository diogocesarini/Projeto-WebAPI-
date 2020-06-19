using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.Results;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("v1/locacoes")]
    public class LocacoesController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Locacoes>>> Get([FromServices] DataContext context)
        {
            try
            {
                var locacoes = await context.Locacoes
                .Include(x => x.Cliente)
                .Include(x => x.Filme)
                .ToListAsync();

                return locacoes;
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    errors = ex.Message
                });
            }

        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Locacoes>> Post([FromServices] DataContext context, [FromBody] LocacoesResult model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var filme = context.Filmes.Where(x => x.Id == model.IdFilme).FirstOrDefault();
                    var cliente = context.Clientes.Where(x => x.Id == model.IdCliente).FirstOrDefault();

                    if (cliente == null)
                    {
                        throw new Exception("Esse cliente não existe!");
                    }

                    if (filme != null)
                    {
                        var locacoes = new Locacoes()
                        {
                            IdCliente = model.IdCliente,
                            IdFilme = model.IdFilme,
                            Preco = model.Preco,
                            DataSaida = DateTimeOffset.Parse(model.DataSaida),
                            DataEntrega = DateTimeOffset.Parse(model.DataEntrega),
                            DataEntregada = null
                        };

                        if (filme.IsDisponivel)
                        {
                            filme.IsDisponivel = false;

                            context.Filmes.Update(filme);
                            context.Locacoes.Add(locacoes);
                            await context.SaveChangesAsync();
                        }
                        else
                        {
                            throw new Exception("Filme não está disponivel para locação!");
                        }

                        return locacoes;
                    }
                    else
                    {
                        throw new Exception("Nenhum filme encontrado!");
                    }

                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    errors = ex.Message
                });
            }

        }

        [HttpPost]
        [Route("devolucao/{idLocacao}")]
        public async Task<ActionResult<Locacoes>> Post([FromServices] DataContext context, int idLocacao)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var locacao = context.Locacoes.Where(x => x.Id == idLocacao).FirstOrDefault();

                    if (locacao != null)
                    {
                        if (locacao.DataEntregada == null)
                        {
                            var filme = context.Filmes.Where(x => x.Id == locacao.IdFilme).FirstOrDefault();

                            filme.IsDisponivel = false;

                            TimeSpan diferencaDias = DateTime.Now.Subtract(locacao.DataEntrega.DateTime);

                            if (diferencaDias.Days > 0)
                            {
                                throw new Exception("Filme está a " + diferencaDias.Days + " dia(s) e " + diferencaDias.Days + ":"+ diferencaDias.Minutes + " hrs atrasados!.");
                            }

                            locacao.DataEntregada = DateTime.Now;

                            context.Filmes.Update(filme);
                            context.Locacoes.Update(locacao);
                            await context.SaveChangesAsync();

                        }
                        else
                        {
                            throw new Exception("Filme já foi entregue!.");
                        }
                    }
                    else
                    {
                        throw new Exception("Nenhuma locação encontrada!");
                    }

                    return locacao;
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    errors = ex.Message
                });
            }

        }
    }
}
