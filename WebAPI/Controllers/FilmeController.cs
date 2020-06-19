using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.Results;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("v1/filmes")]
    public class FilmeController : ControllerBase
    {

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Filme>>> Get([FromServices] DataContext context)
        {
            try
            {
                var filmes = await context.Filmes.Where(x => x.Ativo).ToListAsync();

                return filmes;
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
        public async Task<ActionResult<Filme>> Post([FromServices] DataContext context, [FromBody] FilmeResult model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var filme = new Filme()
                    {
                         Titulo = model.Titulo,
                         Sinopse = model.Sinopse,
                         Genero = model.Genero,
                         Ano = model.Ano,
                         Ativo = model.Ativo,
                         IsDisponivel = model.IsDisponivel
                    };
                    context.Filmes.Add(filme);
                    await context.SaveChangesAsync();

                    return filme;
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

        [HttpDelete]
        [Route("{idFilme}")]
        public async Task<ActionResult<Filme>> Delete([FromServices] DataContext context, int idFilme)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var filme = context.Filmes.Where(x => x.Id == idFilme).FirstOrDefault();

                    if (filme != null)
                    {
                        filme.Ativo = false;

                        context.Filmes.Update(filme);
                        await context.SaveChangesAsync();
                    }
                    else
                    {
                        throw new Exception("Filme não encontrado!");
                    }

                    return filme;
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
