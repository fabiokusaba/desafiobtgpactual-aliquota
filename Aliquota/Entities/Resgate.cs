namespace Aliquota.Entities;

public class Resgate
{
    public int Id { get; set; }
    public decimal ValorResgate { get; set; }
    public decimal ImpostoDeRenda { get; set; }
    public DateTime DataResgate { get; set; }
    public int FundoId { get; set; }
    public int ClienteId { get; set; }

    public Resgate(decimal valorResgate, DateTime dataResgate, Aplicacao aplicacao)
    {
        if (dataResgate < aplicacao.DataAplicacao)
            throw new ArgumentException("A data do resgate não pode ser menor que a data da aplicação.");
        
        ValorResgate = valorResgate;
        DataResgate = dataResgate;
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

        ValorResgate -= ImpostoDeRenda;
    }
}