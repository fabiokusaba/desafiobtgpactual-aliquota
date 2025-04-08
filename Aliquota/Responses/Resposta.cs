namespace Aliquota.Responses;

public record FundoInvestimentoDetalhesDto(
    int Id,
    string Nome,
    List<AplicacaoDto> Aplicacoes,
    List<ResgateDto> Resgates
);

public record AplicacaoDto(int Id, decimal Valor, DateTime DataAplicacao);

public record ResgateDto(int Id, decimal Valor, DateTime DataResgate, decimal ImpostoDeRenda, decimal ValorLiquido);
    