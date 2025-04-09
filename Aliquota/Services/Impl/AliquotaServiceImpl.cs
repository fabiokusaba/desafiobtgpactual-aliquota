using Aliquota.Data;
using Aliquota.Entities;
using Aliquota.Mapper;
using Aliquota.Requests;
using Aliquota.Responses;
using Microsoft.EntityFrameworkCore;

namespace Aliquota.Services.Impl;

public class AliquotaServiceImpl : IAliquotaService
{
    private readonly AppDbContext _context;

    public AliquotaServiceImpl(AppDbContext context)
    {
        _context = context;
    }

    public async Task<AplicacaoDto> Aplicar(Requisicao requisicao)
    {
        if (requisicao.Valor <= 0)
            throw new ArgumentException("O valor da aplicação precisa ser maior que 0");
        
        var cliente = await _context.Clientes
            .Include(c => c.Aplicacoes)
            .FirstOrDefaultAsync(c => c.Id == requisicao.ClienteId);
        
        if (cliente is null)
            throw new InvalidOperationException("Não foi encontrado nenhum cliente");

        var fundoInvestimento = await _context.Fundos.FindAsync(requisicao.NumeroFundoInvestimento);
        
        if (fundoInvestimento is null)
            throw new InvalidOperationException("Não foi encontrado nenhum fundo de investimento");
        
        await _context.Entry(fundoInvestimento).Collection(fi => fi.Aplicacoes).LoadAsync();

        var aplicacao = new Aplicacao(requisicao.Valor, requisicao.NumeroFundoInvestimento, requisicao.ClienteId);
        
        fundoInvestimento.Aplicacoes.Add(aplicacao);
        cliente.Aplicacoes.Add(aplicacao);
        
        _context.Aplicacoes.Add(aplicacao);
        await _context.SaveChangesAsync();

        return new AplicacaoDto(aplicacao.Id, aplicacao.Valor, aplicacao.DataAplicacao);
    }

    public async Task<ResgateDto> Resgatar(Requisicao requisicao)
    {
        if (requisicao.Valor <= 0)
            throw new ArgumentException("O valor do resgate deve ser maior que 0");
        
        var cliente = await _context.Clientes
            .Include(c => c.Resgates)
            .Include(c => c.Aplicacoes)
            .FirstOrDefaultAsync(c => c.Id == requisicao.ClienteId);
        
        if (cliente is null)
            throw new InvalidOperationException("Não foi encontrado nenhum cliente");
        
        var fundoInvestimento = await _context.Fundos.FindAsync(requisicao.NumeroFundoInvestimento);

        if (fundoInvestimento is null)
            throw new InvalidOperationException("Fundo de investimento não encontrado");
        
        await _context.Entry(fundoInvestimento).Collection(fi => fi.Aplicacoes).LoadAsync();

        var aplicacao = fundoInvestimento.ObterAplicacao(requisicao.AplicacaoId);

        if (aplicacao.Valor < requisicao.Valor)
            throw new InvalidOperationException("O valor do resgate não pode ser maior que o valor aplicado");
        
        aplicacao.RetirarSaldo(requisicao.Valor);

        var resgate = new Resgate(requisicao.Valor, aplicacao);
        resgate.ClienteId = requisicao.ClienteId;
        resgate.FundoId = requisicao.NumeroFundoInvestimento;

        _context.Aplicacoes.Update(aplicacao);
        _context.Fundos.Update(fundoInvestimento);
        
        _context.Resgates.Add(resgate);
        await _context.SaveChangesAsync();

        return new ResgateDto(resgate.Id, requisicao.Valor, resgate.DataResgate, resgate.ImpostoDeRenda, resgate.ValorLiquido);
    }

    public async Task<List<FundoInvestimentoDetalhesDto>> Listar()
    {
        var fundos = await _context.Fundos
            .Include(fi => fi.Aplicacoes)
            .Include(fi => fi.Resgates)
            .ToListAsync();
        
        var fundoInvestimentoDetalhes = fundos.Select(f => f.MapearParaDto()).ToList();

        return fundoInvestimentoDetalhes;
    }
}