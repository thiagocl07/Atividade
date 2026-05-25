using ControleEstoque.Domain.Entities;

namespace ControleEstoque.Domain.Interfaces;

public interface IMovimentacaoRepository : IRepository<Movimentacao, Guid>
{
    IReadOnlyCollection<Movimentacao> ObterPorProduto(Guid produtoId);
    IReadOnlyCollection<Movimentacao> ObterPorPeriodo(DateTime inicio, DateTime fim);
}
