using Aliquota.Requests;
using Aliquota.Responses;

namespace Aliquota.Services;

public interface IAliquotaService
{
    Task<AplicacaoDto> Aplicar(Requisicao requisicao);
    Task<ResgateDto> Resgatar(Requisicao requisicao);
    Task<FundoInvestimentoDetalhesDto> Listar();
}