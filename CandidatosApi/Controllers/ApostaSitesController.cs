using CandidatosBusiness;
using CandidatosModel;
using Microsoft.AspNetCore.Mvc;

namespace CandidatosApi.Controllers;

[ApiController]
[Route("api/aposta-sites")]
public class ApostaSitesController(IApostaSiteService service) : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var sites = service.ListarTodos();
        return sites.Count == 0 ? NoContent() : Ok(sites);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(string id)
    {
        var site = service.ObterPorId(id);
        return site == null ? NotFound() : Ok(site);
    }

    [HttpPost]
    public IActionResult Post([FromBody] ApostaSiteModel site)
    {
        if (site is null) return BadRequest("Payload inválido.");
        if (string.IsNullOrWhiteSpace(site.Nome)) return BadRequest("Nome é obrigatório.");
        if (string.IsNullOrWhiteSpace(site.Url)) return BadRequest("Url é obrigatória.");

        var criado = service.Criar(site);
        return CreatedAtAction(nameof(GetById), new { id = criado.Id }, criado);
    }

    [HttpPut]
    public IActionResult Put([FromBody] ApostaSiteModel site)
    {
        if (site is null || string.IsNullOrWhiteSpace(site.Id))
            return BadRequest("Dados inconsistentes.");
        return service.Atualizar(site) ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
        return service.Remover(id) ? NoContent() : NotFound();
    }

    [HttpGet("exportar-json")]
    public IActionResult ExportarJson([FromQuery] string? path = null)
    {
        var destino = string.IsNullOrWhiteSpace(path)
            ? Path.Combine(AppContext.BaseDirectory, "aposta-sites.json")
            : path;

        service.ExportarJson(destino);
        service.RegistrarLog($"API: exportar-json -> {destino}");
        return Ok(new { message = "Exportação realizada.", file = destino });
    }
}
