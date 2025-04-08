namespace Aliquota.Entities;

public class Cliente
{
    public Cliente(string nome)
    {
        Nome = nome;
    }
    
    public int Id { get; set; }
    public string Nome { get; set; }
    public List<Aplicacao> Aplicacoes { get; set; } = [];
    public List<Resgate> Resgates { get; set; } = [];
    
    private Cliente()
    {
        
    }
}