# PRD - Sistema de Gestão de Estoque de Papelaria Escolar

## 1. Visão Geral do Produto

**Nome do Produto:** Controle de Estoque - Papelaria Escolar

**Objetivo:** Desenvolver um sistema robusto, escalável e bem arquitetado para gerenciar o estoque de produtos em uma papelaria escolar, seguindo princípios SOLID e padrões orientados a objetos.

**Escopo:** Sistema web/desktop com camadas bem definidas (Presentation, Application, Domain, Infrastructure) que permite controle completo do ciclo de vida do estoque.

---

## 2. Objetivos Principais

- ✅ Manter registro atualizado e preciso de produtos e suas quantidades
- ✅ Prevenir inconsistências de dados através de validações e regras de negócio
- ✅ Proporcionar rastreabilidade completa das movimentações
- ✅ Facilitar a reposição de estoque através de alertas automáticos
- ✅ Garantir arquitetura extensível e mantível
- ✅ Implementar padrões orientados a objetos (SOLID, DRY, KISS)

---

## 3. Requisitos Funcionais

### 3.1 Gestão de Categorias
- **RF-001:** Criar categorias de produtos (ex: papéis, canetas, cadernos)
- **RF-002:** Listar categorias cadastradas
- **RF-003:** Atualizar nome e descrição de categorias
- **RF-004:** Deletar categorias (apenas se sem produtos vinculados)
- **RF-005:** Validar categoria duplicada

### 3.2 Gestão de Fornecedores
- **RF-006:** Cadastrar fornecedores com nome, CNPJ, telefone, email e endereço
- **RF-007:** Listar fornecedores
- **RF-008:** Atualizar dados de fornecedores
- **RF-009:** Deletar fornecedores (com validações)
- **RF-010:** Consultar produtos de um fornecedor específico

### 3.3 Gestão de Produtos
- **RF-011:** Cadastrar produtos com: código, nome, categoria, estoque mínimo, preço
- **RF-012:** Associar produto a fornecedor
- **RF-013:** Validar unicidade do código do produto
- **RF-014:** Listar produtos por categoria
- **RF-015:** Listar produtos abaixo do estoque mínimo
- **RF-016:** Atualizar preço e estoque mínimo de produtos
- **RF-017:** Consultar disponibilidade em tempo real

### 3.4 Movimentações de Estoque
- **RF-018:** Registrar entrada de produtos (nota de compra, recebimento)
- **RF-019:** Registrar saída de produtos (venda, consumo)
- **RF-020:** Bloquear saída se quantidade > saldo disponível
- **RF-021:** Permitir ajustes de estoque com justificativa
- **RF-022:** Validar que todas as movimentações têm usuário e data/hora

### 3.5 Consultas e Relatórios
- **RF-023:** Histórico completo de movimentações com filtros (data, tipo, produto)
- **RF-024:** Saldo atual de cada produto
- **RF-025:** Produtos em falta ou baixa quantidade
- **RF-026:** Valor total do estoque (custo/preço de venda)
- **RF-027:** Movimentações por período
- **RF-028:** Rastreabilidade: quem fez, quando, por qual motivo

### 3.6 Validações e Regras de Negócio
- **RF-029:** Impedir saída com quantidade > saldo
- **RF-030:** Permitir apenas quantidades positivas (maiores que zero)
- **RF-031:** Requerer justificativa para ajustes
- **RF-032:** Auditar todas as operações críticas
- **RF-033:** Validar integridade referencial (produto → categoria → fornecedor)

---

## 4. Requisitos Não-Funcionais

### 4.1 Arquitetura
- **RNF-001:** Implementar arquitetura em camadas (Presentation, Application, Domain, Infrastructure)
- **RNF-002:** Utilizar Repositório Pattern para abstrair dados
- **RNF-003:** Separação clara de responsabilidades (SRP)
- **RNF-004:** Injeção de dependência obrigatória
- **RNF-005:** Interfaces bem definidas para facilitar testes

### 4.2 Performance
- **RNF-006:** Consultas devem retornar em < 500ms
- **RNF-007:** Suportar até 50.000 produtos em memória
- **RNF-008:** Índices para buscas rápidas por código e categoria

### 4.3 Confiabilidade
- **RNF-009:** Validação de entrada em todas as camadas
- **RNF-010:** Tratamento robusto de exceções
- **RNF-011:** Transações atômicas para operações críticas
- **RNF-012:** Backup automático em memória (persistência futura)

### 4.4 Manutenibilidade
- **RNF-013:** Código limpo e legível (Clean Code)
- **RNF-014:** Cobertura de testes unitários ≥ 80%
- **RNF-015:** Documentação de classes e métodos
- **RNF-016:** Design Patterns apropriados (Factory, Strategy, etc.)

### 4.5 Segurança
- **RNF-017:** Validação de entrada em todas as operações
- **RNF-018:** Prevenção de operações inválidas por exceções de negócio
- **RNF-019:** Auditoria de mudanças críticas

---

## 5. Arquitetura em Camadas

```
┌─────────────────────────────────────┐
│     PRESENTATION LAYER              │ ← UI/Controllers
├─────────────────────────────────────┤
│     APPLICATION LAYER               │ ← Use Cases/Services
├─────────────────────────────────────┤
│     DOMAIN LAYER                    │ ← Entities/Value Objects/Interfaces
├─────────────────────────────────────┤
│     INFRASTRUCTURE LAYER            │ ← Repositories/Persistence
└─────────────────────────────────────┘
```

### 5.1 Camada de Domínio (Domain Layer)
Contém as regras de negócio e entidades core:
- `Produto` (Entity)
- `Categoria` (Entity)
- `Fornecedor` (Entity)
- `Movimentacao` (Entity)
- `EstoqueMovimentacao` (Value Object)
- Interfaces de repositórios
- Exceções de negócio customizadas

### 5.2 Camada de Aplicação (Application Layer)
Orquestra as operações de negócio:
- `ProdutoService`
- `EstoqueService`
- `MovimentacaoService`
- `RelatorioService`
- DTOs (Data Transfer Objects)

### 5.3 Camada de Infraestrutura (Infrastructure Layer)
Implementa interfaces do domínio:
- `ProdutoRepository` (em memória)
- `CategoriaRepository` (em memória)
- `FornecedorRepository` (em memória)
- `MovimentacaoRepository` (em memória)

### 5.4 Camada de Apresentação (Presentation Layer)
Expõe funcionalidades:
- Controllers/Endpoints REST
- DTOs de entrada e saída
- Tratamento de HTTP

---

## 6. Modelo de Dados

### 6.1 Entidades

#### Categoria
```
- id: UUID
- nome: String (único, obrigatório)
- descricao: String
- dataCriacao: LocalDateTime
- dataAtualizacao: LocalDateTime
```

#### Fornecedor
```
- id: UUID
- nome: String (obrigatório)
- cnpj: String (único, validado)
- telefone: String
- email: String (validado)
- endereco: String
- dataCriacao: LocalDateTime
```

#### Produto
```
- id: UUID
- codigo: String (único, obrigatório)
- nome: String (obrigatório)
- categoria: Categoria (obrigatório)
- fornecedor: Fornecedor (obrigatório)
- precoUnitario: BigDecimal (> 0)
- estoqueMinimo: Integer (>= 0)
- estoqueAtual: Integer (>= 0)
- dataCriacao: LocalDateTime
- dataAtualizacao: LocalDateTime
```

#### Movimentacao
```
- id: UUID
- produto: Produto
- tipo: TipoMovimentacao (ENTRADA, SAIDA, AJUSTE)
- quantidade: Integer (> 0)
- saldoAnterior: Integer
- saldoAtual: Integer
- motivo: String (obrigatório para AJUSTE)
- usuarioOperador: String
- dataCriacao: LocalDateTime
```

#### TipoMovimentacao (Enum)
```
- ENTRADA (Recebimento de fornecedor)
- SAIDA (Venda/Consumo)
- AJUSTE (Acerto de inventário)
```

---

## 7. Interfaces Principais

### 7.1 Interface de Repositório (Genérica)
```java
public interface IRepository<T, ID> {
    T salvar(T entidade);
    T atualizar(T entidade);
    void deletar(ID id);
    T obterPorId(ID id);
    List<T> obterTodos();
}
```

### 7.2 Interface de Produto Repository
```java
public interface IProdutoRepository extends IRepository<Produto, UUID> {
    Produto obterPorCodigo(String codigo);
    List<Produto> obterPorCategoria(UUID categoriaId);
    List<Produto> obterAbaixoDoMinimo();
    List<Produto> obterPorFornecedor(UUID fornecedorId);
}
```

### 7.3 Interface de Estoque Service
```java
public interface IEstoqueService {
    void registrarEntrada(UUID produtoId, Integer quantidade, String motivo);
    void registrarSaida(UUID produtoId, Integer quantidade, String motivo);
    void ajustarEstoque(UUID produtoId, Integer novaQuantidade, String justificativa);
    Integer obterSaldoAtual(UUID produtoId);
    List<ProdutoEmFaltaDTO> obterProdutosEmFalta();
}
```

### 7.4 Interface de Movimentacao Repository
```java
public interface IMovimentacaoRepository extends IRepository<Movimentacao, UUID> {
    List<Movimentacao> obterPorProduto(UUID produtoId);
    List<Movimentacao> obterPorPeriodo(LocalDate dataInicio, LocalDate dataFim);
    List<Movimentacao> obterPorTipo(TipoMovimentacao tipo);
    List<Movimentacao> obterHistorico(UUID produtoId, LocalDate dataInicio, LocalDate dataFim);
}
```

---

## 8. Padrões de Design e Princípios SOLID

### 8.1 SOLID Principles

| Princípio | Aplicação |
|-----------|-----------|
| **S**RP   | Cada serviço tem uma única responsabilidade (ProdutoService, EstoqueService) |
| **O**CP   | Aberto para extensão (novos tipos de movimentação) via enums |
| **L**SP   | Interfaces de repositório com contrato consistente |
| **I**SP   | Interfaces segregadas (IProdutoRepository ≠ IFornecedorRepository) |
| **D**IP   | Injeção de dependências em todos os serviços |

### 8.2 Design Patterns

| Padrão | Uso |
|--------|-----|
| **Repository** | Abstrair persistência de dados |
| **Dependency Injection** | Facilitar testes e flexibilidade |
| **Factory** | Criar movimentações de forma consistente |
| **Value Object** | EstoqueMovimentacao com imutabilidade |
| **Strategy** | Diferentes tipos de movimentação |
| **Observer** | Notificações de estoque mínimo (futuro) |

### 8.3 Boas Práticas

- ✅ Imutabilidade de Value Objects
- ✅ Validações no domínio (Domain-Driven Design)
- ✅ Exceções customizadas para negócio
- ✅ Uso de DTOs entre camadas
- ✅ Enums para estados e tipos
- ✅ Timestamps em todas as entidades

---

## 9. Exceções Customizadas

```
EstoqueException (base)
├── ProdutoNaoEncontradoException
├── EstoqueInsuficienteException
├── CategoriaNaoEncontradaException
├── FornecedorNaoEncontradoException
├── MovimentacaoInvalidaException
├── CategoriaComProdutosVinculadosException
├── CodigoProdutoDuplicadoException
└── OperacaoNaoPermitidaException
```

---

## 10. Fluxos Principais

### 10.1 Fluxo de Entrada de Estoque
```
1. Validar produto existe
2. Validar quantidade > 0
3. Calcular novo saldo
4. Criar objeto Movimentacao
5. Salvar movimentacao no repositório
6. Atualizar saldo do produto
7. Retornar confirmação
```

### 10.2 Fluxo de Saída de Estoque
```
1. Validar produto existe
2. Validar quantidade > 0
3. Validar saldo >= quantidade (CRÍTICO)
4. Calcular novo saldo
5. Criar objeto Movimentacao
6. Salvar movimentacao no repositório
7. Atualizar saldo do produto
8. Verificar se abaixo do mínimo
9. Retornar confirmação
```

### 10.3 Fluxo de Ajuste de Estoque
```
1. Validar produto existe
2. Validar nova quantidade >= 0
3. Obter saldo anterior
4. Calcular diferença (entrada ou saída)
5. Validar justificativa fornecida
6. Criar Movimentacao com tipo AJUSTE
7. Salvar operação
8. Auditar alteração
```

---

## 11. Critérios de Aceitação

### 11.1 Funcionalidade
- ✅ Todas as operações CRUD funcionam sem erros
- ✅ Saídas são bloqueadas quando há insuficiência de estoque
- ✅ Histórico registra todas as movimentações com usuário e timestamp
- ✅ Produtos em falta aparecem nos relatórios

### 11.2 Arquitetura
- ✅ Camadas bem definidas e testáveis
- ✅ Repositórios em memória funcionam perfeitamente
- ✅ Injeção de dependência implementada
- ✅ Sem dependências circulares

### 11.3 Qualidade
- ✅ Cobertura de testes ≥ 80%
- ✅ Sem vazamento de memória em operações repetidas
- ✅ Exceções tratadas apropriadamente
- ✅ Código segue convenções de nomenclatura

### 11.4 Documentação
- ✅ README com instruções de uso
- ✅ Classes e métodos públicos documentados
- ✅ Exemplos de uso inclusos

---

## 12. Roadmap de Desenvolvimento

### Fase 1: Core Domain (Sprint 1-2)
- [x] Estrutura de camadas
- [x] Entidades do domínio
- [x] Repositórios em memória
- [x] Serviços de aplicação

### Fase 2: API REST (Sprint 2-3)
- [x] Controllers de produto
- [x] Controllers de estoque
- [x] Endpoints de movimentação
- [x] DTOs padronizados

### Fase 3: Validações e Negócio (Sprint 3)
- [x] Regras de negócio
- [x] Exceções customizadas
- [x] Tratamento de erros HTTP

### Fase 4: Testes (Sprint 4)
- [x] Testes unitários
- [x] Testes de integração
- [x] Cobertura ≥ 80%

### Fase 5: Relatórios (Sprint 5)
- [ ] Endpoint de movimentações por período
- [ ] Relatório de produtos em falta
- [ ] Relatório financeiro do estoque

---

## 13. Tecnologias Recomendadas

| Camada | Tecnologia |
|--------|-----------|
| **Linguagem** | Java 11+ ou C# .NET |
| **API** | Spring Boot / ASP.NET Core |
| **Banco (futuro)** | PostgreSQL / SQL Server |
| **Testes** | JUnit5, Mockito / xUnit, Moq |
| **Documentação** | Swagger/OpenAPI |
| **Controle Versão** | Git |

---

## 14. Sucesso Esperado

O sistema será considerado bem-sucedido quando:

✅ Todas as movimentações são registradas corretamente
✅ Não há possibilidade de estoque negativo
✅ Relatórios precisos disponíveis em tempo real
✅ Arquitetura permite fácil manutenção e evolução
✅ Código é testável e bem documentado
✅ Todas as transações são auditadas

---

**Versão:** 1.0
**Data:** Maio 2026
**Status:** Aprovado para desenvolvimento
