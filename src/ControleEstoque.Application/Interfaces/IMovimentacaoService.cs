using System;
using System.Collections.Generic;
using ControleEstoque.Application.Models;

namespace ControleEstoque.Application.Interfaces;

public interface IMovimentacaoService
{
    void RegistrarMovimentacao(string codigoProduto, string tipo, int quantidade, int saldoAnterior, int saldoAtual, string motivo);
    IReadOnlyCollection<MovimentacaoModel> ObterTodasAsMovimentacoes();
    IReadOnlyCollection<MovimentacaoModel> ObterMovimentacoesPorProduto(string codigoProduto);
    IReadOnlyCollection<MovimentacaoModel> ObterMovimentacoesRecentes(int quantidade = 50);
}
