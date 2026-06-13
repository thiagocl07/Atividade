using System;
using System.Collections.Generic;
using System.Linq;
using ControleEstoque.Application.Interfaces;
using ControleEstoque.Application.Models;
using ControleEstoque.Domain.Interfaces;
using ControleEstoque.Domain.Entities;
using ControleEstoque.Domain.Enums;

namespace ControleEstoque.Application.Services;

public sealed class MovimentacaoService : IMovimentacaoService
{
    private readonly IMovimentacaoRepository _movimentacaoRepository;
    private readonly IProdutoRepository _produtoRepository;

    public MovimentacaoService(IMovimentacaoRepository movimentacaoRepository, IProdutoRepository produtoRepository)
    {
        _movimentacaoRepository = movimentacaoRepository ?? throw new ArgumentNullException(nameof(movimentacaoRepository));
        _produtoRepository = produtoRepository ?? throw new ArgumentNullException(nameof(produtoRepository));
    }

    public void RegistrarMovimentacao(string codigoProduto, string tipo, int quantidade, int saldoAnterior, int saldoAtual, string motivo)
    {
        if (string.IsNullOrWhiteSpace(codigoProduto)) throw new ArgumentException("Código do produto é obrigatório.", nameof(codigoProduto));

        var produto = _produtoRepository.ObterPorCodigo(codigoProduto) ?? throw new InvalidOperationException("Produto não encontrado.");

        if (!Enum.TryParse<TipoMovimentacao>(tipo, true, out var tipoEnum))
        {
            throw new InvalidOperationException("Tipo de movimentação inválido.");
        }

        // saldo anterior calculado a partir do produto atual
        var saldoAnteriorReal = produto.EstoqueAtual;

        // atualiza o estoque na entidade de domínio (pode lançar se inválido)
        produto.RegistrarMovimentacao(tipoEnum, quantidade);
        _produtoRepository.Atualizar(produto);

        // cria e persiste a movimentação de domínio
        var movimentacao = new Movimentacao(produto, tipoEnum, quantidade, saldoAnteriorReal, motivo ?? "", "Sistema");
        _movimentacaoRepository.Salvar(movimentacao);
    }

    public IReadOnlyCollection<MovimentacaoModel> ObterTodasAsMovimentacoes()
    {
        return _movimentacaoRepository.ObterTodos().Select(MovimentacaoModel.FromEntity).ToArray();
    }

    public IReadOnlyCollection<MovimentacaoModel> ObterMovimentacoesPorProduto(string codigoProduto)
    {
        var produto = _produtoRepository.ObterPorCodigo(codigoProduto);
        if (produto is null) return Array.Empty<MovimentacaoModel>();
        return _movimentacaoRepository.ObterPorProduto(produto.Id).Select(MovimentacaoModel.FromEntity).ToArray();
    }

    public IReadOnlyCollection<MovimentacaoModel> ObterMovimentacoesRecentes(int quantidade = 50)
    {
        return _movimentacaoRepository.ObterTodos()
            .OrderByDescending(m => m.DataCriacao)
            .Take(quantidade)
            .Select(MovimentacaoModel.FromEntity)
            .ToArray();
    }
}
