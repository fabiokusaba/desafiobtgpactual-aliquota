namespace Aliquota.Entities;

public class Aplicacao
{
    public int Id { get; set; }
    public decimal Valor { get; set; }
    public DateTime DataAplicacao { get; set; }
    public int FundoId { get; set; }
    public int ClienteId { get; set; }
    
    
    private Aplicacao()
    {
        
    }

    public Aplicacao(decimal valor, DateTime dataAplicacao, int fundoId, int clienteId)
    {
        if(valor <= 0)
            throw new ArgumentException("O valor da aplicação não pode ser menor ou igual a zero.", nameof(valor));
        
        Valor = valor;
        DataAplicacao = dataAplicacao;
        FundoId = fundoId;
        ClienteId = clienteId;
    }

    public void RetirarSaldo(decimal valor)
    {
        Valor -= valor;
    }
}