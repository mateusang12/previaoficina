using Microsoft.AspNetCore.Mvc;
using OficinaAPI.Data;
using OficinaAPI.DTOs;
using OficinaAPI.Models;

namespace OficinaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IOficinaRepo _repository;

        public ClientesController(IOficinaRepo repository)
        {
            _repository = repository;
        }

        // Rota: GET api/clientes
        [HttpGet]
        public ActionResult<IEnumerable<ClienteReadDto>> ObterTodos()
        {
            var clientes = _repository.ObterTodosClientes();

            // Converte a Model para DTO antes de enviar ao Angular
            var clientesDto = clientes.Select(c => new ClienteReadDto
            {
                Id = c.Id,
                Nome = c.Nome,
                Cpf = c.Cpf,
                Telefone = c.Telefone
            });

            return Ok(clientesDto);
        }

        // Rota: POST api/clientes
        [HttpPost]
        public ActionResult<ClienteReadDto> Criar([FromBody] ClienteCreateDto clienteCreateDto)
        {
            var clienteModel = new Cliente
            {
                Nome = clienteCreateDto.Nome,
                Cpf = clienteCreateDto.Cpf,
                Telefone = clienteCreateDto.Telefone
            };

            _repository.CriarCliente(clienteModel);
            _repository.SaveChanges();

            var clienteReadDto = new ClienteReadDto
            {
                Id = clienteModel.Id,
                Nome = clienteModel.Nome,
                Cpf = clienteModel.Cpf,
                Telefone = clienteModel.Telefone
            };

            return CreatedAtAction(nameof(ObterTodos), new { id = clienteReadDto.Id }, clienteReadDto);
        }
    }
}