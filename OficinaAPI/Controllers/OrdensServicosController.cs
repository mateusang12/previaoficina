using Microsoft.AspNetCore.Mvc;
using OficinaAPI.Data;
using OficinaAPI.Models;

namespace OficinaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdensServicosController : ControllerBase
    {
        private readonly IOficinaRepo _repository;

        public OrdensServicosController(IOficinaRepo repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public ActionResult Criar([FromBody] OrdemServico os)
        {
            _repository.CriarOS(os);
            _repository.SaveChanges();

            // Gera a OS usando o ano atual 2026 + o ID formatado com no mínimo 2 dígitos (Ex: 202601)
            os.NumeroOS = $"2026{os.Id:D2}";
            _repository.AtualizarOS(os);
            _repository.SaveChanges();

            return Ok(new { message = "Agendamento realizado!", numeroOS = os.NumeroOS });
        }

        [HttpGet]
        public ActionResult<IEnumerable<OrdemServico>> ListarTodas() => Ok(_repository.ObterTodasOS());

        [HttpPut("atribuir/{id}")]
        public ActionResult AtribuirMecanico(int id, [FromBody] Dictionary<string, string> dados)
        {
            var os = _repository.ObterOSPorId(id);
            if (os == null) return NotFound();

            os.MecanicoResponsavel = dados["mecanico"];
            os.Status = "Em Andamento";

            _repository.AtualizarOS(os);
            _repository.SaveChanges();
            return Ok();
        }

        [HttpPut("finalizar/{id}")]
        public ActionResult Finalizar(int id, [FromBody] Dictionary<string, string> dados)
        {
            var os = _repository.ObterOSPorId(id);
            if (os == null) return NotFound();

            os.ComentarioFinal = dados["comentario"];
            os.Status = "Finalizado";

            _repository.AtualizarOS(os);
            _repository.SaveChanges();
            return Ok();
        }
    }
}