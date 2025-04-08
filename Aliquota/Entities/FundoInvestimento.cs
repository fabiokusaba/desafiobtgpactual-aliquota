namespace Aliquota.Entities;

public class FundoInvestimento
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public List<Aplicacao> Aplicacoes { get; set; }
    public List<Resgate> Resgates { get; set; }
    
    public FundoInvestimento(string nome)
    {
        Nome = nome;
    }
    
    private FundoInvestimento()
    {
        
    }

    public Aplicacao ObterAplicacao(int aplicacaoId)
    {
        var aplicacao = Aplicacoes.Find(x => x.Id == aplicacaoId);
        
        if(aplicacao is null)
            throw new InvalidOperationException("Aplicação não encontrada");
        
        return aplicacao;
    }
}