using Microsoft.AspNetCore.Mvc;
using OficinaAPI.Data;
using OficinaAPI.DTOs;
using OficinaAPI.Models;

namespace OficinaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VeiculosController : ControllerBase
    {
        private readonly IOficinaRepo _repository;

        public VeiculosController(IOficinaRepo repository)
        {
            _repository = repository;
        }

        // Rota: POST api/veiculos
        [HttpPost]
        public ActionResult CriarVeiculo([FromBody] VeiculoCreateDto veiculoDto)
        {
            var cliente = _repository.ObterClientePorId(veiculoDto.ClienteId);
            if (cliente == null)
            {
                return NotFound(new { message = "Cliente não encontrado para vincular este veículo." });
            }

            var veiculoModel = new Veiculo
            {
                Marca = veiculoDto.Marca,
                Modelo = veiculoDto.Modelo,
                Ano = veiculoDto.Ano,
                Placa = veiculoDto.Placa,
                ClienteId = veiculoDto.ClienteId
            };

            _repository.CriarVeiculo(veiculoModel);
            _repository.SaveChanges();

            return Ok(new { message = "Veículo cadastrado e vinculado com sucesso!" });
        }

        // Rota: GET api/veiculos/cliente/5
        [HttpGet("cliente/{clienteId}")]
        public ActionResult<IEnumerable<Veiculo>> ObterPorCliente(int clienteId)
        {
            var veiculos = _repository.ObterVeiculosPorCliente(clienteId);
            return Ok(veiculos);
        }
    }
}