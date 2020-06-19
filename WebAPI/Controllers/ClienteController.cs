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
    [Route("v1/clientes")]
    public class ClienteController : ControllerBase
    {

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Cliente>>> Get([FromServices] DataContext context)
        {
            try
            {
                var clientes = await context.Clientes.Where(x => x.Ativo).ToListAsync();

                return clientes;
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
        public async Task<ActionResult<Cliente>> Post([FromServices] DataContext context, [FromBody] ClienteResult model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var cliente = context.Clientes.Where(x => x.CPF == model.CPF).FirstOrDefault();
                    
                    if (cliente == null)
                    {
                        var clienteNovo = new Cliente()
                        {
                            CPF = model.CPF,
                            Nome = model.Nome,
                            Endereco = model.Endereco,
                            Numero = model.Numero,
                            Cidade = model.Cidade,
                            Ativo = model.Ativo
                        };

                        context.Clientes.Add(clienteNovo);
                        await context.SaveChangesAsync();

                        return clienteNovo;

                    }
                    else
                    {

                        throw new Exception("Cliente já possui cadastro no sistema!");

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

        [HttpDelete]
        [Route("Delete/{idCliente}")]
        public async Task<ActionResult<Cliente>> Delete([FromServices] DataContext context, int idCliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var cliente = context.Clientes.Where(x => x.Id == idCliente).FirstOrDefault();

                    cliente.Ativo = false;

                    context.Clientes.Update(cliente);
                    await context.SaveChangesAsync();

                    return cliente;
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
