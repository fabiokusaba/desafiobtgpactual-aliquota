using Aliquota.Requests;
using Aliquota.Services;
using Microsoft.AspNetCore.Mvc;

namespace Aliquota.Controllers;

[ApiController]
[Route("/v1/[controller]")]
public class AliquotaController : ControllerBase
{
    private readonly IAliquotaService _aliquotaService;

    public AliquotaController(IAliquotaService aliquotaService)
    {
        _aliquotaService = aliquotaService;
    }

    [HttpPost("{fundoId}/aplicacao")]
    public async Task<IActionResult> Aplicar(int fundoId, [FromBody] Requisicao requisicao)
    {
        try
        {
            requisicao.NumeroFundoInvestimento = fundoId;
            var aplicacao = await _aliquotaService.Aplicar(requisicao);

            return CreatedAtAction(nameof(Aplicar), new
            {
                data = new
                {
                    id = aplicacao.Id,
                    valor = aplicacao.Valor,
                    dataAplicacao = aplicacao.DataAplicacao,
                }
            });
        }
        catch (ArgumentException ex)
        {
            return NotFound(new
            {
                data = new
                {
                    error = ex.Message
                }
            });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new
            {
                data = new
                {
                    error = ex.Message
                }
            });
        }
    }

    [HttpPost("{fundoId}/resgatar")]
    public async Task<IActionResult> Resgatar(int fundoId, [FromBody] Requisicao requisicao)
    {
        try
        {
            requisicao.NumeroFundoInvestimento = fundoId;
            var resgate = await _aliquotaService.Resgatar(requisicao);

            return CreatedAtAction(nameof(Resgatar), new
            {
                data = new
                {
                    id = resgate.Id,
                    valor = resgate.Valor,
                    dataResgate = resgate.DataResgate
                }
            });
        }
        catch (ArgumentException ex)
        {
            return NotFound(new
            {
                data = new
                {
                    error = ex.Message
                }
            });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new
            {
                data = new
                {
                    error = ex.Message
                }
            });
        }
    }

    [HttpGet("listar")]
    public async Task<IActionResult> Listar()
    {
        var fundoAplicacoes = await _aliquotaService.Listar();

        return Ok(new
        {
            data = fundoAplicacoes
        });
    }
}