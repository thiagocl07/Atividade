using System;
using System.Collections.Generic;
using System.Linq;
using ControleEstoque.Application.Interfaces;
using ControleEstoque.Application.Models;
using ControleEstoque.Application.Repositories;

namespace ControleEstoque.Application.Services;

public sealed class MovimentacaoService : IMovimentacaoService
{
    private readonly InMemoryMovimentacaoRepository _repository;

    public MovimentacaoService(InMemoryMovimentacaoRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public void RegistrarMovimentacao(string codigoProduto, string tipo, int quantidade, int saldoAnterior, int saldoAtual, string motivo)
    {
        var movimentacao = new MovimentacaoModel
        {
            Id = Guid.NewGuid(),
            CodigoProduto = codigoProduto,
            NomeProduto = codigoProduto,
            Tipo = tipo,
            Quantidade = quantidade,
            SaldoAnterior = saldoAnterior,
            SaldoAtual = saldoAtual,
            Motivo = motivo,
            UsuarioOperador = "Sistema",
            DataCriacao = DateTime.UtcNow
        };

        _repository.Registrar(movimentacao);
    }

    public IReadOnlyCollection<MovimentacaoModel> ObterTodasAsMovimentacoes()
    {
        return _repository.ObterTodas();
    }

    public IReadOnlyCollection<MovimentacaoModel> ObterMovimentacoesPorProduto(string codigoProduto)
    {
        return _repository.ObterPorProduto(codigoProduto);
    }

    public IReadOnlyCollection<MovimentacaoModel> ObterMovimentacoesRecentes(int quantidade = 50)
    {
        return _repository.ObterRecentes(quantidade);
    }
}
