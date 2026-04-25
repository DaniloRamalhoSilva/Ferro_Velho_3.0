# Migracao PostgreSQL - Sistema inteiro

Este roteiro organiza a retirada gradual de SQL Server/LINQ-to-SQL das telas e
consultas do sistema. A regra de implementacao e manter fallback SQL Server
enquanto existir codigo legado, mas todo fluxo novo deve chamar
`PostgresConnectionService` via `DataContextFactory`.

## Ordem dos blocos

1. Base compartilhada
   - Centralizar consultas PostgreSQL em `PostgresConnectionService`.
   - Expor wrappers em `DataContextFactory`.
   - Evitar `SqlCommand`, `SqlDataReader`, `DataContextFactory.Filtrar`,
     `CRUD`, `Selecionar` e `DataContext` em caminho PostgreSQL.

2. Fluxos ja migrados
   - Login.
   - Venda.
   - Compra.
   - Cadastro de produto.
   - Operacao recursos.

3. Cadastros restantes
   - Cliente.
   - Usuario.
   - Impressora/configuracao.

4. Consultas operacionais
   - Estoque.
   - Notas de compra.
   - Notas de venda.
   - Movimentacao de recursos.

5. Relatorios e impressoes
   - Compra.
   - Venda.
   - Produto.
   - Estoque.
   - Fluxo de caixa.
   - Lucro.
   - Financeiro.

6. Limpeza final
   - Remover dependencias SQL Server de caminhos ativos.
   - Revisar `DataContextFactoryNew`.
   - Compilar `Sistema.sln`.
   - Rodar smoke test manual dos menus principais.

## Status atual

- Base compartilhada concluida com wrappers PostgreSQL em `DataContextFactory`.
- Login, venda, compra e cadastro de produto concluidos.
- Operacao recursos concluida, incluindo caixa, adiantamento, recebimento e
  acerto de cliente.
- Cadastros de cliente, usuario e impressora concluidos.
- Consultas de estoque, notas de compra, notas de venda e movimentacao de
  recursos concluidas.
- Relatorios de compra, venda, produto, estoque, fluxo de caixa e lucro
  concluidos.
- Impressao da nota de compra no menu principal concluida com datasets
  PostgreSQL compativeis com `Report2.rdlc`.
- A solucao compila em Debug/Any CPU com 0 erros. Restam 2 avisos antigos no
  designer do menu principal sobre campos nao utilizados.

## Pendencias de limpeza

- As chamadas SQL Server restantes ficam nos blocos `else` de compatibilidade,
  nos helpers legados do `DataContextFactory` e nos arquivos gerados do DataSet.
- `DataContextFactoryNew` e as classes antigas em `FerroVelho/Classes` ainda
  existem no repositorio, mas nao entram no projeto compilado atual.
- O proximo passo recomendado e validar manualmente cada menu com uma base
  PostgreSQL real e, depois disso, remover de forma controlada o fallback SQL
  Server.

## Criterios por bloco

- Cada tela deve detectar PostgreSQL por `DataContextFactory.IsPostgresConnectionString`.
- SQL PostgreSQL deve usar parametros, nao concatenacao de texto.
- Consultas de relatorio devem retornar `DataTable` com os mesmos nomes de
  colunas esperados pelos grids/RDLCs.
- Depois de cada bloco, a solucao deve compilar sem erros.
