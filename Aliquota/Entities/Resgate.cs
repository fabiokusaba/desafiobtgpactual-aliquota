namespace Aliquota.Entities;

public class Resgate
{
    public int Id { get; set; }
    public decimal ValorResgate { get; set; }
    public decimal ImpostoDeRenda { get; set; }
    public DateTime DataResgate { get; set; }
    public FundoInvestimento FundoInvestimento { get; set; }
    public int FundoId { get; set; }
    public Cliente Cliente { get; set; }
    public int ClienteId { get; set; }
    public decimal ValorLiquido { get; set; }

    public Resgate(decimal valorResgate, Aplicacao aplicacao)
    {
        ValorResgate = valorResgate;
        DataResgate = DateTime.Now;
        CalcularImpostoDeRenda(aplicacao);
    }
    
    private Resgate()
    {
        
    }

    private void CalcularImpostoDeRenda(Aplicacao aplicacao)
    {
        var lucro = ValorResgate - aplicacao.Valor;
        var tempoAplicacao = (DataResgate - aplicacao.DataAplicacao).TotalDays / 365;

        ImpostoDeRenda = tempoAplicacao switch
        {
            <= 1 => lucro * 0.225m,
            <= 2 => lucro * 0.185m,
            _ => lucro * 0.15m
        };

        ValorLiquido = ValorResgate - ImpostoDeRenda;
    }
}