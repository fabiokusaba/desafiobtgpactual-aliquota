namespace Aliquota.Requests;

public class Requisicao
{
    public Requisicao(int aplicacaoId, decimal valor, DateTime data)
    {
        AplicacaoId = aplicacaoId;
        Valor = valor;
        Data = data;
    }

    public int NumeroFundoInvestimento { get; set; }
    public int AplicacaoId { get; set; }
    public decimal Valor { get; set; }
    public int ClienteId { get; set; }
    public DateTime Data { get; set; }
    
    public Requisicao()
    {
        
    }
}