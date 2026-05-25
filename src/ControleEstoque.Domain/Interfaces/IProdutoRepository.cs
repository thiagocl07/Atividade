using ControleEstoque.Domain.Entities;

namespace ControleEstoque.Domain.Interfaces;

public interface IProdutoRepository : IRepository<Produto, Guid>
{
    Produto? ObterPorCodigo(string codigo);
    IReadOnlyCollection<Produto> ObterPorCategoria(Guid categoriaId);
    IReadOnlyCollection<Produto> ObterAbaixoDoMinimo();
    IReadOnlyCollection<Produto> ObterPorFornecedor(Guid fornecedorId);
}
