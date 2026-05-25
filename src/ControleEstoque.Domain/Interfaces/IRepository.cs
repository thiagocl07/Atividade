namespace ControleEstoque.Domain.Interfaces;

public interface IRepository<T, TId>
{
    T Salvar(T entidade);
    T Atualizar(T entidade);
    void Deletar(TId id);
    T? ObterPorId(TId id);
    IReadOnlyCollection<T> ObterTodos();
}
