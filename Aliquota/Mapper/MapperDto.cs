using Aliquota.Entities;
using Aliquota.Responses;

namespace Aliquota.Mapper;

public static class MapperDto
{
    public static FundoInvestimentoDetalhesDto MapearParaDto(this FundoInvestimento fundoInvestimento)
    {
        return new FundoInvestimentoDetalhesDto(
            fundoInvestimento.Id,
            fundoInvestimento.Nome,
            fundoInvestimento.Aplicacoes
                .Select(a => new AplicacaoDto(a.Id, a.Valor, a.DataAplicacao)).ToList(),
            fundoInvestimento.Resgates
                .Select(r => new ResgateDto(r.Id, r.ValorResgate, r.DataResgate, r.ImpostoDeRenda, r.ValorLiquido)).ToList()
        );
    }
}