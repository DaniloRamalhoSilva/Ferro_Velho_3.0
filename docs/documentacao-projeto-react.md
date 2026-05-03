# Ferro Velho 3.0 - Documentacao do projeto e base para versao Web React

Documento criado para registrar o funcionamento do projeto desktop `Ferro_Velho_3.0` e orientar a criacao de uma versao web em React consumindo a API `Ferro_Velho_API`.

Data do levantamento: 2026-05-03.

## 1. Resumo executivo

O projeto `Ferro_Velho_3.0` e um aplicativo Windows Forms em C#/.NET Framework 4.8 para operacao de compra, venda e controle financeiro de um ferro-velho/cooperativa de reciclagem.

A aplicacao desktop nao acessa mais o banco diretamente. Ela usa a biblioteca `FerroVelhoDAO` como adaptador HTTP para uma API REST Node.js/Express localizada em `..\Ferro_Velho_API`.

Para uma versao web em React, o caminho mais adequado e reaproveitar a API existente, criando no frontend:

- fluxo de login;
- persistencia da sessao do usuario logado;
- envio automatico do header `x-empresa-cod`;
- telas equivalentes aos modulos WinForms;
- camada de servicos HTTP tipada;
- componentes para cadastros, operacoes, consultas e relatorios.

## 2. Projetos envolvidos

### 2.1 Desktop

Raiz: `Ferro_Velho_3.0`

Arquivos principais:

- `Sistema.sln`: solution Visual Studio.
- `FerroVelho/FerroVelho.csproj`: projeto Windows Forms.
- `FerroVelhoDAO/FerroVelhoDAO.csproj`: biblioteca de comunicacao com a API.
- `README.md`: instrucoes resumidas de execucao.
- `FerroVelho/configuração.xml`: arquivo local usado para guardar URL da API.

### 2.2 API

Raiz relativa: `..\Ferro_Velho_API`

Tecnologias:

- Node.js 16+
- Express
- Sequelize
- PostgreSQL

Scripts relevantes:

- `npm install`
- `npm run db:create`
- `npm run db:migrate`
- `npm run seed`
- `npm start`
- `npm run debug`

Seeds iniciais da API:

- empresa padrao;
- tipos de usuario `Administrador` e `Operacional`;
- usuario administrador `admin` / senha `admin`.

## 3. Tecnologias do desktop

| Area | Tecnologia |
| --- | --- |
| Interface | Windows Forms |
| Linguagem | C# |
| Framework | .NET Framework 4.8 |
| Relatorios | Microsoft ReportViewer / RDLC |
| Impressao | `System.Drawing.Printing.PrintDocument` |
| HTTP | `System.Net.Http.HttpClient` |
| JSON | `System.Web.Script.Serialization.JavaScriptSerializer` |
| API externa | `Ferro_Velho_API` |

Pacotes NuGet usados no WinForms:

- `Microsoft.ReportingServices.ReportViewerControl.Winforms` 150.1449.0
- `Microsoft.SqlServer.Types` 14.0.314.76

## 4. Estrutura de pastas do desktop

```text
Ferro_Velho_3.0/
  Sistema.sln
  README.md
  FerroVelho/
    Program.cs
    AppErrorHandler.cs
    Dao.cs
    imprimir.cs
    configuração.xml
    *.rdlc
    Cadastro/
      fm_cadProduto.*
      fm_cadUsuario.*
      fm_cadCliente.*
    Configuração/
      fm_configuracao.*
      fm_cofigImpressaora.*
      fm_cabecario.*
    Consulta/
      fm_estoque.*
      fm_movRecursos.*
      fm_notasCompra.*
      fm_notasVenda.*
    Iniciação/
      fm_login.*
      fm_menuPrincipal.*
    Operaçoes/
      fm_vender.*
      fm_outrasEntradas.*
      fm_outrasSaidas.*
      fm_recurcos.*
      fm_adiantamentoOp.*
      fm_recebmentoOp.*
      fm_outrasMovimentacoes.*
    Relatorios/
      fm_relProduto.*
      fm_repEstoque.*
      fm_relCompra.*
      fm_relVenda.*
      fm_relLucro.*
      fm_relFluxoCaixa.*
      fm_imp*.*
  FerroVelhoDAO/
    Models.cs
    DataContextFactory.cs
    ApiConnectionService.cs
```

Observacao: algumas pastas e classes usam nomes com acentos ou grafia antiga, por exemplo `Operaçoes`, `Configuração`, `fm_recurcos`, `fm_recebmentoOp`, `fm_menulPrincipal`. Na versao React vale padronizar nomes sem acentos e com ingles/portugues consistente.

## 5. Arquitetura atual

### 5.1 Fluxo em alto nivel

```text
Usuario
  -> Windows Forms
    -> DataContextFactory
      -> ApiConnectionService
        -> Ferro_Velho_API
          -> PostgreSQL
```

### 5.2 Responsabilidades

| Componente | Responsabilidade |
| --- | --- |
| `Program.cs` | Registra tratamento global de erro, abre login e depois menu principal. |
| `fm_login` | Le configuracao, testa API, autentica usuario. |
| `fm_menulPrincipal` | Menu, permissoes, area principal de compra. |
| Forms de cadastro | CRUD de produtos, usuarios e clientes. |
| Forms de operacao | Compras, vendas, ajustes de estoque, recursos e movimentos de cliente. |
| Forms de consulta | Estoque, notas, caixa e movimentacoes. |
| Forms de relatorio | Montam DataTables e RDLCs para visualizacao/impressao. |
| `DataContextFactory` | Guarda estado global da sessao, URL da API, empresa, cabecalho e metodos de negocio. |
| `ApiConnectionService` | Monta requisicoes HTTP, serializa JSON, envia header `x-empresa-cod` e mapeia respostas. |
| `Models.cs` | DTOs usados pelo desktop. |

## 6. Inicializacao e sessao

### 6.1 Entrada da aplicacao

`FerroVelho/Program.cs`:

1. registra `AppErrorHandler.Register()`;
2. ativa estilos visuais;
3. abre `fm_login` como modal;
4. se `fm_login.logar == true`, abre `fm_menulPrincipal`.

### 6.2 Configuracao da API

`DataContextFactory` usa `http://localhost:3000` como URL padrao.

O arquivo local `configuração.xml` pode guardar:

```xml
<configConexao>
  <apiUrl>http://localhost:3000</apiUrl>
</configConexao>
```

Regras atuais:

- se nao houver configuracao, usa `http://localhost:3000`;
- se a URL nao comecar com `http://` ou `https://`, volta para o padrao;
- se a URL terminar em `/api`, `ApiConnectionService` remove esse sufixo para evitar duplicidade.

No React, isso deve virar variavel de ambiente, por exemplo:

```text
VITE_API_URL=http://localhost:3000
```

### 6.3 Login

O login chama:

1. `GET /health` para testar conexao;
2. `POST /api/auth/login` com `nome_usuario` e `senha_usuario`;
3. se retornar usuario, `DataContextFactory.DefinirUsuarioAutenticado(usuario)` guarda:
   - usuario logado;
   - `empresa_cod`;
   - cabecalho da empresa;
   - codigo da empresa usado como escopo das chamadas.

### 6.4 Header multiempresa

Todas as rotas de negocio, exceto login, exigem escopo da empresa.

Header principal:

```http
x-empresa-cod: 1
```

A API tambem aceita `empresaCod` ou `empresa_cod` via query/body em alguns pontos, mas o desktop sempre usa `x-empresa-cod`.

No React, criar um interceptor HTTP para enviar esse header em todas as chamadas autenticadas.

## 7. Permissoes

O usuario tem `permi_usuario` e relacionamento com `tb_tipoUsuario`.

O desktop considera administrador quando:

```text
tb_tipoUsuario.desc_tipoUsuario == "Administrador"
```

Comportamento no menu:

- Administrador ve todos os menus.
- Operacional fica com acesso reduzido:
  - nao ve Cadastro;
  - nao ve Consultar;
  - nao ve Relatorio;
  - nao ve Configuracoes;
  - nao ve Vender;
  - nao ve Outras Entradas;
  - nao ve Outras Saidas;
  - nao ve Recursos.

Na pratica, usuario operacional fica principalmente com a operacao de compra e cliente/pagamentos conforme disponibilidade do menu.

Recomendacao para React:

- criar `AuthContext`;
- armazenar `usuario`, `empresaCod`, `isAdministrador`;
- proteger rotas administrativas;
- deixar a API validar permissao no backend em uma evolucao futura, porque hoje a restricao forte esta principalmente na interface.

## 8. Modelo de dominio

### 8.1 Empresa

Representa a empresa/tenant.

Campos usados pelo desktop:

| Campo | Tipo | Uso |
| --- | --- | --- |
| `empresa_cod` | int | Escopo multiempresa e header `x-empresa-cod`. |
| `empresa_nome` | string | Nome juridico/identificacao. |
| `empresa_nome_fantasia` | string | Cabecalho em relatorios e comprovantes. |
| `empresa_telefone_comercial` | string | Cabecalho. |
| `empresa_endereco` | string | Cabecalho. |

### 8.2 Tipo de usuario

| Campo | Tipo | Uso |
| --- | --- | --- |
| `id_tipoUsuario` / `id_tipousuario` | int | Permissao. |
| `desc_tipoUsuario` / `desc_tipousuario` | string | Ex.: `Administrador`, `Operacional`. |

### 8.3 Usuario

| Campo | Tipo | Uso |
| --- | --- | --- |
| `id_usuario` | int | Usuario responsavel por operacoes. |
| `empresa_cod` | int | Empresa do usuario. |
| `nome_usuario` | string | Login e exibicao. |
| `senha_usuario` | string | Senha. Hoje e texto simples no fluxo atual. |
| `permi_usuario` | int | FK para tipo de usuario. |
| `ativo` | bool | Usuario ativo/inativo. |
| `tb_tipoUsuario` | objeto | Tipo mapeado para permissao. |

### 8.4 Produto

| Campo | Tipo | Uso |
| --- | --- | --- |
| `id_prod` | int | ID interno. |
| `empresa_cod` | int | Empresa. |
| `cod_prod` | string | Codigo operacional do produto. |
| `desc_prod` | string | Descricao. |
| `val_prod` | decimal | Valor padrao usado na compra. |
| `usuario` | int? | Usuario que cadastrou. |
| `excluido` | bool | Exclusao logica. |

Saldo do produto nao fica diretamente no registro. E calculado por entradas (`tb_itemc`) menos saidas (`tb_itemv`).

### 8.5 Cliente

| Campo | Tipo | Uso |
| --- | --- | --- |
| `id_cliente` | int | ID interno. |
| `nome_cliente` | string | Nome. |
| `cpf_cliente` | string | Documento, sem mascara ao salvar. |
| `tel_cliente` | string | Telefone, sem mascara ao salvar. |
| `Saldo` / `saldo` | decimal | Saldo financeiro consolidado. |

Saldo positivo significa credito do cliente. Saldo negativo significa valor devedor do cliente.

### 8.6 Compra

Compra representa entrada de material no estoque e possivel pagamento ao cliente.

| Campo | Tipo | Uso |
| --- | --- | --- |
| `id_compra` | int | Numero da nota de compra. |
| `data_compra` | DateTime | Data/hora. |
| `desconto_compra` | decimal | Valor abatido por divida do cliente. |
| `subtot_compra` | decimal | Soma dos itens antes de desconto. |
| `valor_nota` | decimal | Valor efetivamente pago ao cliente. |
| `id_cliente` | int? | Cliente associado, opcional. |
| `usuario` | int | Operador. |

### 8.7 Item de compra

| Campo | Tipo | Uso |
| --- | --- | --- |
| `id_item` | int | ID do item. |
| `id_prod` | int | Produto. |
| `cod_prod` | string | Codigo do produto. |
| `id_compra` | int | Nota de compra. |
| `quant_item` | decimal(3 casas) | Quantidade/peso. |
| `subtot_item` | decimal | Total do item. |
| `valor_item` | decimal | Valor unitario. |

Item de compra aumenta estoque.

### 8.8 Venda

Venda representa saida de material do estoque.

| Campo | Tipo | Uso |
| --- | --- | --- |
| `id_venda` | int | Numero da nota de venda. |
| `data_venda` | DateTime | Data/hora. |
| `valor_nota` | decimal | Total da venda. |
| `usuario` | int? | Operador. |

### 8.9 Item de venda

| Campo | Tipo | Uso |
| --- | --- | --- |
| `id_item` | int | ID do item. |
| `id_prod` | int? | Produto. |
| `cod_prod` | string | Codigo do produto. |
| `id_venda` | int? | Nota de venda. |
| `quant_item` | decimal(3 casas) | Quantidade/peso. |
| `subtot_item` | decimal | Total do item. |
| `valr_item` | decimal | Valor unitario. |

Item de venda reduz estoque.

### 8.10 Caixa

Registra movimentacoes financeiras.

| Campo | Tipo | Uso |
| --- | --- | --- |
| `id_caixa` | int | ID. |
| `data_caixa` | DateTime | Data/hora. |
| `valor_caixa` | decimal | Positivo entra no caixa, negativo sai do caixa. |
| `usuario` | int | Operador. |
| `desc_caixa` | string | Descricao. |
| `id_cliente` | int? | Cliente vinculado, opcional. |

### 8.11 Acerto de cliente

Registra movimentacao financeira direto no saldo do cliente sem passar pelo caixa.

| Campo | Tipo | Uso |
| --- | --- | --- |
| `id_acliente` | int | ID. |
| `data_acliente` | DateTime | Data/hora. |
| `valor_acliente` | decimal | Positivo aumenta credito/recebimento; negativo aumenta pagamento/baixa. |
| `usuario` | int | Operador. |
| `desc_acliente` | string | Descricao. |
| `id_cliente` | int | Cliente. |

### 8.12 Impressora

| Campo | Tipo | Uso |
| --- | --- | --- |
| `id_impressora` | int | `1` para cupom/relatorio no uso atual; `2` tambem aparece na tela de configuracao. |
| `nome_impressora` | string | Nome da impressora do Windows. |

No React, impressora local via navegador exige outra estrategia: PDF/download, impressao do navegador, ou um agente local.

## 9. Modulos e telas atuais

### 9.1 Login

Arquivo: `FerroVelho/Iniciação/fm_login.cs`

Funcoes:

- le configuracao local da URL da API;
- testa conexao com `/health`;
- autentica usuario;
- guarda sessao em `DataContextFactory`.

Campos:

- nome de usuario;
- senha.

Erros:

- usuario ou senha invalido;
- falha de conexao ou erro da API.

React equivalente:

- rota `/login`;
- formulario com usuario e senha;
- chamada `POST /api/auth/login`;
- salvar `usuario` e `empresa_cod`;
- redirecionar para dashboard.

### 9.2 Menu principal

Arquivo: `FerroVelho/Iniciação/fm_menuPrincipal.cs`

Menus:

- Operacoes:
  - Comprar;
  - Vender;
  - Outras Entradas;
  - Outras Saidas;
  - Recursos;
  - Cliente:
    - Pagamento (saida de caixa);
    - Recebimento (entrada de caixa);
    - Outras Movimentacoes.
- Cadastro:
  - Produto;
  - Usuarios;
  - Cliente.
- Consultar:
  - Estoque;
  - Movimentacao recurso;
  - Nota Fiscal:
    - Compra;
    - Venda;
  - Clientes.
- Relatorio:
  - Produtos;
  - Estoque;
  - Fluxo de Caixa;
  - Nota Fiscal:
    - Compra;
    - Venda;
  - Financeiro.
- Configuracoes:
  - Impressoras;
  - Cabecalho.

O proprio menu principal tambem contem a tela de compra embutida.

React equivalente:

- layout autenticado com sidebar/topbar;
- area principal por rotas;
- compra em rota propria `/operacoes/compras/nova`;
- guardar operacao em andamento no estado da pagina.

### 9.3 Cadastro de produtos

Arquivo: `FerroVelho/Cadastro/fm_cadProduto.cs`

Funcoes:

- listar produtos ativos;
- criar produto;
- alterar produto;
- excluir produto logicamente;
- validar codigo obrigatorio;
- validar descricao e valor;
- validar codigo unico por empresa.

Endpoints:

- `GET /api/produtos`
- `POST /api/produtos`
- `PUT /api/produtos/:id`
- `DELETE /api/produtos/:id`

Campos:

- codigo;
- descricao;
- valor.

Regras:

- codigo de produto e obrigatorio;
- descricao e valor sao obrigatorios;
- valor deve ser decimal valido;
- codigo nao pode repetir dentro da mesma empresa;
- exclusao e logica (`excluido = true`).

React equivalente:

- rota `/cadastros/produtos`;
- tabela com busca;
- formulario modal/lateral;
- validacao client-side e tratamento de `409` da API.

### 9.4 Cadastro de usuarios

Arquivo: `FerroVelho/Cadastro/fm_cadUsuario.cs`

Funcoes:

- listar tipos de usuario;
- listar usuarios ativos;
- alternar exibicao de inativos;
- criar usuario;
- alterar usuario;
- inativar usuario;
- reativar usuario.

Endpoints:

- `GET /api/tipos-usuario`
- `GET /api/usuarios?incluirInativos=false`
- `GET /api/usuarios/existe`
- `POST /api/usuarios`
- `PUT /api/usuarios/:id`
- `PATCH /api/usuarios/:id/ativo`

Campos:

- nome;
- senha;
- confirmacao de senha;
- tipo/permissao.

Regras:

- todos os campos sao obrigatorios;
- senha e confirmacao devem ser iguais;
- nome de usuario nao pode repetir na empresa;
- "excluir" usuario na verdade define `ativo = false`;
- reativar define `ativo = true`.

Ponto importante para web:

- a API atual trabalha com senha em texto simples. Antes de publicar uma versao web real, implementar hash de senha e autenticacao por token/sessao no backend.

### 9.5 Cadastro e consulta de clientes

Arquivo: `FerroVelho/Cadastro/fm_cadCliente.cs`

Funcoes:

- listar clientes ativos;
- criar, alterar e excluir cliente;
- buscar por nome, CPF ou telefone;
- selecionar cliente para outras operacoes;
- consultar movimentacao financeira completa ou resumida;
- exibir saldo colorido:
  - positivo em verde;
  - negativo em vermelho.

Endpoints:

- `GET /api/clientes`
- `GET /api/clientes?filtroCampo=nome&filtroValor=...`
- `GET /api/clientes?filtroCampo=cpf&filtroValor=...`
- `GET /api/clientes?filtroCampo=tel&filtroValor=...`
- `POST /api/clientes`
- `PUT /api/clientes/:id`
- `DELETE /api/clientes/:id`
- `GET /api/clientes/:id/movimentacao?resumido=true|false`

Campos:

- nome;
- CPF;
- telefone.

Regras:

- nome e obrigatorio;
- CPF e telefone sao salvos sem mascara;
- exclusao e logica;
- se a exclusao falhar, o desktop assume que o cliente tem movimentacao.

React equivalente:

- rota `/cadastros/clientes`;
- tabela com filtros;
- painel de detalhes com abas:
  - dados cadastrais;
  - movimentacao completa;
  - movimentacao resumida;
  - saldo.

### 9.6 Compra de material

Arquivo principal: `FerroVelho/Iniciação/fm_menuPrincipal.cs`

Compra e a operacao de entrada de material no estoque e pagamento/credito ao cliente.

Fluxo atual:

1. Usuario clica em `Operacoes > Comprar`.
2. Sistema carrega produtos ativos.
3. Usuario informa produto, valor unitario e quantidade/peso.
4. Ao adicionar o primeiro item:
   - cria uma compra em `POST /api/compras`;
   - insere item em `POST /api/compras/:id/itens`;
   - exibe numero da nota.
5. A cada item:
   - recalcula subtotal;
   - recalcula desconto;
   - recalcula total a pagar;
   - atualiza compra em `PUT /api/compras/:id`.
6. Ao finalizar:
   - valida que total nao e negativo;
   - imprime se marcado;
   - limpa tela.

Endpoints:

- `GET /api/produtos`
- `GET /api/caixa/saldo`
- `POST /api/compras`
- `POST /api/compras/:id/itens`
- `GET /api/compras/:id/itens`
- `PUT /api/compras/:id`
- `DELETE /api/compras/itens/:idItem`
- `DELETE /api/compras/:id`
- `GET /api/clientes/:id/saldo?tipo=devedor`
- `GET /api/clientes/:id/saldo?tipo=credito`

Campos da tela:

- produto por codigo ou descricao;
- quantidade/peso;
- valor unitario;
- subtotal do item;
- desconto;
- subtotal da nota;
- total a pagar;
- cliente opcional;
- imprimir;
- adicionar como credito.

Regras importantes:

- todos os campos do item sao obrigatorios;
- valor do desconto nao pode deixar total negativo, exceto quando o fluxo de credito esta marcado e o total e zerado;
- para adicionar item, o desktop consulta saldo de caixa e exige saldo suficiente para o subtotal do item;
- quando cliente e informado, o sistema busca o saldo devedor e preenche como desconto;
- se cliente ainda possui saldo devedor, nao permite adicionar novo credito;
- se marcar "Adicionar como credito", o total da nota fica zero e a diferenca vira credito do cliente pela formula da API:

```text
credito = subtot_compra - desconto_compra - valor_nota
```

Comportamento em caso de cancelamento:

- se a compra foi criada e nenhum item ficou salvo, o desktop exclui a compra;
- se fechar a janela com compra em andamento, bloqueia fechamento ate finalizar ou remover itens.

React equivalente:

- rota `/operacoes/compras/nova`;
- estado local para rascunho de compra;
- criar a nota no primeiro item ou criar explicitamente ao iniciar;
- atualizar totais a cada mudanca;
- bloquear navegacao com compra em andamento;
- confirmar exclusao de itens;
- tratar erro de caixa insuficiente.

### 9.7 Venda de material

Arquivo: `FerroVelho/Operaçoes/fm_vender.cs`

Venda e a operacao de saida de material do estoque.

Fluxo atual:

1. Usuario abre `Operacoes > Vender`.
2. Sistema carrega produtos ativos ou excluidos com saldo.
3. Usuario informa produto, valor unitario e quantidade/peso.
4. Sistema consulta saldo do produto.
5. Se saldo for suficiente:
   - cria venda no primeiro item;
   - insere item;
   - lista itens da venda;
   - recalcula total.
6. Ao finalizar:
   - atualiza `valor_nota`;
   - imprime nota;
   - limpa tela.

Endpoints:

- `GET /api/produtos?incluirExcluidosComSaldo=true`
- `GET /api/produtos/:codProd/saldo`
- `POST /api/vendas`
- `POST /api/vendas/:id/itens`
- `GET /api/vendas/:id/itens`
- `PUT /api/vendas/:id`
- `DELETE /api/vendas/itens/:idItem`
- `DELETE /api/vendas/:id`
- `GET /api/relatorios/vendas/:id`

Regras:

- todos os campos do item sao obrigatorios;
- quantidade vendida nao pode superar saldo atual do produto;
- se a venda ficar sem itens, a nota e excluida;
- fechamento com venda em andamento e bloqueado.

Observacao de comportamento atual:

- `bt_finalCompra_Click` chama `imprimirNF` dentro do `if (checkBox1.Checked)` e depois chama `imprimirNF` novamente fora do `if`. Isso faz a venda imprimir sempre e, se a opcao estiver marcada, pode imprimir duas vezes. Na versao React, decidir se esse comportamento deve ser corrigido.

React equivalente:

- rota `/operacoes/vendas/nova`;
- consulta de saldo ao selecionar produto;
- validacao de saldo antes de chamar API;
- impressao via PDF/browser.

### 9.8 Outras entradas

Arquivo: `FerroVelho/Operaçoes/fm_outrasEntradas.cs`

Uso:

- ajuste manual para aumentar estoque;
- tambem usada pela tela de correcao de estoque.

Fluxo:

- seleciona produto;
- informa quantidade;
- cria uma compra com valores zerados;
- insere item de compra com `subtot_item = 0` e `valor_item = 0`;
- fecha a tela.

Endpoints:

- `GET /api/produtos?incluirExcluidos=true`
- `GET /api/produtos/:codProd/saldo`
- `POST /api/compras`
- `POST /api/compras/:id/itens`

### 9.9 Outras saidas

Arquivo: `FerroVelho/Operaçoes/fm_outrasSaidas.cs`

Uso:

- ajuste manual para reduzir estoque;
- tambem usada pela tela de correcao de estoque.

Fluxo:

- seleciona produto;
- informa quantidade;
- cria uma venda com valor zerado;
- insere item de venda com `subtot_item = 0` e `valr_item = 0`;
- fecha a tela.

Endpoints:

- `GET /api/produtos?incluirExcluidos=true`
- `GET /api/produtos/:codProd/saldo`
- `POST /api/vendas`
- `POST /api/vendas/:id/itens`

### 9.10 Recursos / caixa

Arquivo: `FerroVelho/Operaçoes/fm_recurcos.cs`

Uso:

- adicionar recurso ao caixa;
- retirar recurso do caixa, chamado de sangria.

Fluxo:

- carrega saldo atual de caixa;
- se usuario informa valor em "adicionar", cria lancamento positivo;
- se usuario informa valor em "retirar", cria lancamento negativo;
- descricao padrao:
  - `Adicao de recurso`;
  - `Sangria`.

Endpoints:

- `GET /api/caixa/saldo`
- `POST /api/caixa`

Regra:

- `valor_caixa > 0` aumenta saldo;
- `valor_caixa < 0` reduz saldo.

### 9.11 Pagamento ao cliente

Arquivo: `FerroVelho/Operaçoes/fm_adiantamentoOp.cs`

Menu: `Operacoes > Cliente > Pagamento (Saida Cx)`

Uso:

- pagar valor ao cliente, reduzindo caixa;
- pode ser usado para adiantamento ou pagamento de credito existente.

Fluxo:

- seleciona cliente;
- busca credito do cliente;
- se houver credito, preenche valor e descricao `Pagamento`;
- se nao houver credito, descricao `Adiantamento`;
- exige valor positivo;
- exige saldo de caixa suficiente;
- grava no caixa com valor negativo e `id_cliente`;
- opcionalmente imprime comprovante.

Endpoints:

- `GET /api/clientes/:id/saldo?tipo=credito`
- `GET /api/caixa/saldo`
- `POST /api/caixa`

### 9.12 Recebimento de cliente

Arquivo: `FerroVelho/Operaçoes/fm_recebmentoOp.cs`

Menu: `Operacoes > Cliente > Recebimento (Entrada Cx)`

Uso:

- receber valor do cliente, aumentando caixa;
- normalmente abate valor devedor.

Fluxo:

- seleciona cliente;
- busca valor devedor;
- se houver, preenche o valor;
- grava no caixa com valor positivo e `id_cliente`;
- opcionalmente imprime comprovante.

Endpoints:

- `GET /api/clientes/:id/saldo?tipo=devedor`
- `POST /api/caixa`

### 9.13 Outras movimentacoes de cliente

Arquivo: `FerroVelho/Operaçoes/fm_outrasMovimentacoes.cs`

Uso:

- registrar acerto direto na conta do cliente sem movimentar caixa.

Fluxo:

- seleciona cliente;
- informa valor e observacao;
- grava em `tb_acliente`;
- opcionalmente imprime comprovante.

Endpoint:

- `POST /api/clientes/:id/acertos`

### 9.14 Consulta de estoque

Arquivo: `FerroVelho/Consulta/fm_estoque.cs`

Funcoes:

- listar produtos e saldo atual;
- filtrar por descricao;
- filtrar por codigo;
- imprimir estoque;
- corrigir saldo.

Endpoints:

- `GET /api/produtos?incluirExcluidosComSaldo=true`
- `GET /api/produtos/:codProd/saldo`
- `GET /api/estoque/atual`

Correcao de saldo:

- usuario informa novo saldo;
- se saldo atual for maior que novo saldo, sistema abre `Outras Saidas` com a diferenca;
- se saldo atual for menor que novo saldo, sistema abre `Outras Entradas` com a diferenca;
- apos a operacao, recarrega estoque.

React equivalente:

- rota `/consultas/estoque`;
- tabela com saldo calculado;
- acao "Corrigir saldo" criando entrada/saida de ajuste.

### 9.15 Consulta de notas de compra

Arquivo: `FerroVelho/Consulta/fm_notasCompra.cs`

Funcoes:

- pesquisar compras por periodo;
- pesquisar compra por numero da nota;
- listar itens da nota;
- excluir nota;
- imprimir nota.

Endpoints:

- `GET /api/compras?inicio=...&fim=...`
- `GET /api/compras?id_compra=...`
- `GET /api/compras/:id/itens`
- `DELETE /api/compras/:id`
- `GET /api/usuarios/:id/nome`
- `GET /api/clientes/:id/nome`

### 9.16 Consulta de notas de venda

Arquivo: `FerroVelho/Consulta/fm_notasVenda.cs`

Funcoes:

- pesquisar vendas por periodo;
- pesquisar venda por numero da nota;
- listar itens da nota;
- excluir nota;
- imprimir nota.

Endpoints:

- `GET /api/vendas?inicio=...&fim=...`
- `GET /api/vendas?id_venda=...`
- `GET /api/vendas/:id/itens`
- `DELETE /api/vendas/:id`
- `GET /api/usuarios/:id/nome`

### 9.17 Consulta de movimentacao de recursos

Arquivo: `FerroVelho/Consulta/fm_movRecursos.cs`

Funcoes:

- listar movimentacoes de caixa por periodo;
- formatar entradas e saidas.

Endpoint:

- `GET /api/recursos/movimentacao?inicio=...&fim=...`

Observacao:

- impressao esta marcada como `Em Manutencao` no desktop.

### 9.18 Relatorios

Relatorios usam RDLC e ReportViewer no desktop. Na versao React, a recomendacao e substituir por:

- tabelas HTML imprimiveis;
- geracao de PDF no frontend;
- geracao de PDF no backend;
- exportacao CSV/XLSX quando fizer sentido.

Relatorios atuais:

| Tela | Arquivo | Endpoint(s) |
| --- | --- | --- |
| Produtos | `fm_relProduto.cs` | `GET /api/produtos` |
| Estoque atual | `fm_impEstoque2.cs` | `GET /api/estoque/atual` |
| Estoque por periodo | `fm_repEstoque.cs` | `GET /api/estoque/periodo` |
| Compra por periodo | `fm_relCompra.cs` | `GET /api/relatorios/compras/produtos`, `GET /api/relatorios/compras/caixa` |
| Venda por periodo | `fm_relVenda.cs` | `GET /api/relatorios/vendas/produtos`, `GET /api/relatorios/vendas/total` |
| Lucro financeiro | `fm_relLucro.cs` | `GET /api/relatorios/lucro/detalhado`, `GET /api/relatorios/lucro/total` |
| Fluxo de caixa | `fm_relFluxoCaixa.cs` | `GET /api/relatorios/fluxo-caixa`, `GET /api/relatorios/fluxo-caixa/saldo-inicial` |
| Nota de venda | `fm_vender.cs`, `fm_notasVenda.cs` | `GET /api/relatorios/vendas/:id` |
| Nota de compra | `fm_menuPrincipal.cs`, `fm_notasCompra.cs` | `GET /api/relatorios/compras/:id/itens`, `/cabecalho`, `/cliente` |

## 10. Contrato da API usado pelo desktop

Base URL padrao: `http://localhost:3000`

Rotas publicas:

| Metodo | Rota | Uso |
| --- | --- | --- |
| `GET` | `/` | Identificacao da API. |
| `GET` | `/health` | Teste de conectividade. |
| `POST` | `/api/auth/login` | Login. |

Rotas autenticadas/escopadas por empresa:

Todas devem enviar:

```http
x-empresa-cod: <empresa_cod>
Content-Type: application/json
```

### 10.1 Auth

```http
POST /api/auth/login
```

Body:

```json
{
  "nome_usuario": "admin",
  "senha_usuario": "admin"
}
```

Resposta esperada:

```json
{
  "id_usuario": 1,
  "empresa_cod": 1,
  "nome_usuario": "admin",
  "senha_usuario": "admin",
  "permi_usuario": 1,
  "ativo": true,
  "desc_tipousuario": "Administrador"
}
```

### 10.2 Empresas

| Metodo | Rota | Uso |
| --- | --- | --- |
| `GET` | `/api/empresas/planos` | Lista planos. |
| `GET` | `/api/empresas` | Lista empresas conforme escopo. |
| `POST` | `/api/empresas` | Cria empresa. |
| `GET` | `/api/empresas/:empresaCod` | Busca empresa. |
| `PUT` | `/api/empresas/:empresaCod/cabecalho` | Atualiza cabecalho usado em impressao/relatorios. |
| `GET` | `/api/empresas/:empresaCod/assinaturas` | Lista assinaturas. |

Body de cabecalho:

```json
{
  "empresa_nome_fantasia": "Ferro Velho Exemplo",
  "empresa_telefone_comercial": "11999999999",
  "empresa_endereco": "Rua Exemplo, 123"
}
```

### 10.3 Usuarios

| Metodo | Rota | Query/Body | Uso |
| --- | --- | --- | --- |
| `GET` | `/api/tipos-usuario` | - | Lista tipos. |
| `GET` | `/api/usuarios` | `incluirInativos=true|false` | Lista usuarios. |
| `GET` | `/api/usuarios/existe` | `nome_usuario`, `exceto_id` | Verifica duplicidade. |
| `POST` | `/api/usuarios` | JSON | Cria usuario. |
| `PUT` | `/api/usuarios/:id` | JSON | Atualiza usuario. |
| `PATCH` | `/api/usuarios/:id/ativo` | `{ "ativo": true|false }` | Ativa/inativa. |
| `GET` | `/api/usuarios/:id/nome` | - | Busca nome. |

Body de criar/atualizar:

```json
{
  "nome_usuario": "operador",
  "senha_usuario": "123",
  "permi_usuario": 2
}
```

### 10.4 Produtos

| Metodo | Rota | Query/Body | Uso |
| --- | --- | --- | --- |
| `GET` | `/api/produtos` | `incluirExcluidos`, `incluirExcluidosComSaldo` | Lista produtos. |
| `POST` | `/api/produtos` | JSON | Cria produto. |
| `PUT` | `/api/produtos/:id` | JSON | Atualiza produto. |
| `DELETE` | `/api/produtos/:id` | - | Exclusao logica. |
| `GET` | `/api/produtos/:codProd/saldo` | - | Saldo atual. |

Body:

```json
{
  "cod_prod": "FERRO",
  "desc_prod": "Ferro",
  "val_prod": 1.25,
  "usuario": 1
}
```

Resposta de saldo:

```json
{
  "cod_prod": "FERRO",
  "saldo": 10.5
}
```

### 10.5 Clientes

| Metodo | Rota | Query/Body | Uso |
| --- | --- | --- | --- |
| `GET` | `/api/clientes` | `filtroCampo`, `filtroValor` | Lista/filtra clientes. |
| `POST` | `/api/clientes` | JSON | Cria cliente. |
| `PUT` | `/api/clientes/:id` | JSON | Atualiza cliente. |
| `DELETE` | `/api/clientes/:id` | - | Exclusao logica. |
| `GET` | `/api/clientes/:id/nome` | - | Busca nome. |
| `GET` | `/api/clientes/:id/saldo` | `tipo=devedor|credito` | Busca saldo especifico. |
| `GET` | `/api/clientes/:id/movimentacao` | `resumido=true|false` | Historico financeiro. |
| `POST` | `/api/clientes/:id/acertos` | JSON | Acerto direto de cliente. |

Body de cliente:

```json
{
  "nome_cliente": "Cliente Exemplo",
  "cpf_cliente": "12345678900",
  "tel_cliente": "11999999999"
}
```

Body de acerto:

```json
{
  "data_acliente": "2026-05-03T10:00:00",
  "desc_acliente": "Ajuste manual",
  "usuario": 1,
  "valor_acliente": 50
}
```

### 10.6 Compras

| Metodo | Rota | Query/Body | Uso |
| --- | --- | --- | --- |
| `GET` | `/api/compras` | `inicio`, `fim`, `id_compra` | Lista compras. |
| `POST` | `/api/compras` | JSON | Cria compra. |
| `PUT` | `/api/compras/:id` | JSON | Atualiza valores da compra. |
| `DELETE` | `/api/compras/:id` | - | Exclusao logica da compra. |
| `GET` | `/api/compras/:id/itens` | - | Lista itens. |
| `POST` | `/api/compras/:id/itens` | JSON | Insere item. |
| `DELETE` | `/api/compras/itens/:idItem` | - | Exclui item. |

Body de compra:

```json
{
  "data_compra": "2026-05-03T10:00:00",
  "usuario": 1,
  "desconto_compra": 0,
  "subtot_compra": 0,
  "valor_nota": 0
}
```

Body de item de compra:

```json
{
  "cod_prod": "FERRO",
  "quant_item": 10.5,
  "subtot_item": 21,
  "valor_item": 2
}
```

Body de atualizacao:

```json
{
  "desconto_compra": 5,
  "subtot_compra": 21,
  "valor_nota": 16,
  "id_cliente": 3
}
```

### 10.7 Vendas

| Metodo | Rota | Query/Body | Uso |
| --- | --- | --- | --- |
| `GET` | `/api/vendas` | `inicio`, `fim`, `id_venda` | Lista vendas. |
| `POST` | `/api/vendas` | JSON | Cria venda. |
| `PUT` | `/api/vendas/:id` | JSON | Atualiza valor final. |
| `DELETE` | `/api/vendas/:id` | - | Exclusao logica da venda. |
| `GET` | `/api/vendas/:id/itens` | - | Lista itens. |
| `POST` | `/api/vendas/:id/itens` | JSON | Insere item. |
| `DELETE` | `/api/vendas/itens/:idItem` | - | Exclui item. |

Body de venda:

```json
{
  "data_venda": "2026-05-03T10:00:00",
  "usuario": 1,
  "valor_nota": 0
}
```

Body de item de venda:

```json
{
  "cod_prod": "FERRO",
  "quant_item": 10.5,
  "subtot_item": 31.5,
  "valr_item": 3
}
```

Body de atualizacao:

```json
{
  "valor_nota": 31.5,
  "usuario": 1
}
```

### 10.8 Caixa e recursos

| Metodo | Rota | Query/Body | Uso |
| --- | --- | --- | --- |
| `GET` | `/api/caixa/saldo` | - | Saldo do caixa. |
| `POST` | `/api/caixa` | JSON | Insere lancamento. |
| `GET` | `/api/recursos/movimentacao` | `inicio`, `fim` | Lista movimentacao. |

Body:

```json
{
  "data_caixa": "2026-05-03T10:00:00",
  "desc_caixa": "Recebimento",
  "usuario": 1,
  "valor_caixa": 100,
  "id_cliente": 3
}
```

### 10.9 Estoque

| Metodo | Rota | Query/Body | Uso |
| --- | --- | --- | --- |
| `GET` | `/api/estoque/atual` | - | Lista estoque atual. |
| `GET` | `/api/estoque/periodo` | `inicio`, `fim`, `cod_prod` | Movimento de estoque por periodo/produto. |

### 10.10 Impressoras

| Metodo | Rota | Body | Uso |
| --- | --- | --- | --- |
| `GET` | `/api/impressoras/:id` | - | Busca impressora. |
| `PUT` | `/api/impressoras/:id` | `{ "nome_impressora": "..." }` | Salva impressora. |

### 10.11 Relatorios

| Metodo | Rota | Query | Uso |
| --- | --- | --- | --- |
| `GET` | `/api/relatorios/compras/produtos` | `inicio`, `fim` | Resumo de compras por produto. |
| `GET` | `/api/relatorios/compras/caixa` | `inicio`, `fim` | Resumo financeiro de compras. |
| `GET` | `/api/relatorios/compras/:id/itens` | - | Itens para nota de compra. |
| `GET` | `/api/relatorios/compras/:id/cabecalho` | - | Cabecalho da nota de compra. |
| `GET` | `/api/relatorios/compras/:id/cliente` | - | Cliente da nota de compra. |
| `GET` | `/api/relatorios/vendas/produtos` | `inicio`, `fim` | Resumo de vendas por produto. |
| `GET` | `/api/relatorios/vendas/total` | `inicio`, `fim` | Total vendido. |
| `GET` | `/api/relatorios/vendas/:id` | - | Dados da nota de venda. |
| `GET` | `/api/relatorios/lucro/detalhado` | `inicio`, `fim` | Lucro detalhado. |
| `GET` | `/api/relatorios/lucro/total` | `inicio`, `fim` | Lucro total. |
| `GET` | `/api/relatorios/fluxo-caixa` | `inicio`, `fim` | Fluxo de caixa. |
| `GET` | `/api/relatorios/fluxo-caixa/saldo-inicial` | `inicio` | Saldo inicial. |

## 11. Tratamento de erros

`ApiConnectionService` tenta extrair `message` do JSON de erro da API. Se nao existir, usa mensagens por status:

| Status | Mensagem desktop |
| --- | --- |
| `401` / `403` | Usuario sem permissao. |
| `404` | Registro nao encontrado. |
| `409` | Conflito com dados atuais. |
| `500` | Erro interno da API. |
| Outros | Erro generico com codigo HTTP. |

Erros de conexao:

- timeout;
- `HttpRequestException`;
- `WebException`;
- `SocketException`.

Mensagem geral: nao foi possivel conectar a API.

No React:

- centralizar isso em `apiClient`;
- exibir toast/dialog com `error.response.data.message` quando existir;
- tratar `401/403` redirecionando ou bloqueando tela;
- tratar falha de rede com mensagem clara.

## 12. Formatos e convencoes de dados

Datas enviadas pelo desktop:

```text
yyyy-MM-ddTHH:mm:ss
```

Valores monetarios:

- decimal;
- normalmente 2 casas.

Quantidades/peso:

- decimal;
- normalmente 3 casas em venda, estoque e ajustes;
- compra principal mostra `N1` em alguns campos, mas itens aceitam decimal.

Mascaras:

- CPF e telefone sao exibidos formatados no WinForms;
- ao salvar cliente, sao enviados sem mascara.

Booleanos em query:

```text
true
false
```

## 13. Regras financeiras e de estoque

### 13.1 Estoque

```text
saldo_produto = soma(quant_item de compras nao excluidas)
              - soma(quant_item de vendas nao excluidas)
```

Entradas de estoque:

- compras normais;
- outras entradas;
- correcao de estoque para cima.

Saidas de estoque:

- vendas normais;
- outras saidas;
- correcao de estoque para baixo.

### 13.2 Caixa

```text
saldo_caixa = soma(valor_caixa)
```

Entradas de caixa:

- adicionar recursos;
- recebimento de cliente.

Saidas de caixa:

- sangria;
- pagamento ao cliente;
- adiantamento ao cliente.

Compra normal nao cria automaticamente lancamento em `tb_caixa` pelo desktop. A regra atual consulta saldo do caixa para permitir a compra, mas os valores da compra ficam na propria tabela de compra e nos relatatorios. Antes da versao web, validar com o dono do negocio se isso e intencional.

### 13.3 Saldo de cliente

A API calcula saldo a partir de:

- caixa vinculado ao cliente;
- acertos de cliente;
- desconto em compras;
- credito gerado em compras.

Interpretacao usada:

- saldo devedor: quando total consolidado e negativo;
- saldo credito: quando total consolidado e positivo.

## 14. Mapeamento sugerido de rotas React

```text
/login
/app
/app/dashboard
/app/operacoes/compras/nova
/app/operacoes/vendas/nova
/app/operacoes/estoque/entrada
/app/operacoes/estoque/saida
/app/operacoes/caixa/recursos
/app/operacoes/clientes/pagamento
/app/operacoes/clientes/recebimento
/app/operacoes/clientes/acerto
/app/cadastros/produtos
/app/cadastros/usuarios
/app/cadastros/clientes
/app/consultas/estoque
/app/consultas/caixa
/app/consultas/notas/compras
/app/consultas/notas/vendas
/app/relatorios/produtos
/app/relatorios/estoque
/app/relatorios/compras
/app/relatorios/vendas
/app/relatorios/financeiro
/app/relatorios/fluxo-caixa
/app/configuracoes/cabecalho
/app/configuracoes/impressao
```

## 15. Estrutura sugerida para o frontend React

```text
src/
  app/
    App.tsx
    router.tsx
  shared/
    api/
      apiClient.ts
      authApi.ts
      produtosApi.ts
      clientesApi.ts
      usuariosApi.ts
      comprasApi.ts
      vendasApi.ts
      caixaApi.ts
      relatoriosApi.ts
    auth/
      AuthContext.tsx
      ProtectedRoute.tsx
      roleGuards.ts
    components/
      Layout.tsx
      DataTable.tsx
      CurrencyInput.tsx
      DecimalInput.tsx
      DateRangeFilter.tsx
      ConfirmDialog.tsx
      PrintView.tsx
    utils/
      formatters.ts
      dates.ts
      money.ts
  pages/
    LoginPage.tsx
    DashboardPage.tsx
    produtos/
    usuarios/
    clientes/
    compras/
    vendas/
    estoque/
    caixa/
    relatorios/
    configuracoes/
  types/
    models.ts
```

## 16. API client recomendado para React

Exemplo conceitual:

```ts
const API_URL = import.meta.env.VITE_API_URL ?? 'http://localhost:3000';

export async function request<T>(path: string, options: RequestInit = {}): Promise<T> {
  const session = getSession();

  const headers = new Headers(options.headers);
  headers.set('Content-Type', 'application/json');

  if (session?.empresa_cod) {
    headers.set('x-empresa-cod', String(session.empresa_cod));
  }

  const response = await fetch(`${API_URL}${path}`, {
    ...options,
    headers,
  });

  const text = await response.text();
  const data = text ? JSON.parse(text) : null;

  if (!response.ok) {
    throw new Error(data?.message ?? `Erro HTTP ${response.status}`);
  }

  return data as T;
}
```

## 17. Tipos TypeScript sugeridos

```ts
export type TipoUsuario = {
  id_tipoUsuario?: number;
  id_tipousuario?: number;
  desc_tipoUsuario?: string;
  desc_tipousuario?: string;
};

export type Usuario = {
  id_usuario: number;
  empresa_cod: number;
  nome_usuario: string;
  senha_usuario?: string;
  permi_usuario: number;
  ativo: boolean;
  desc_tipousuario?: string;
  tb_tipoUsuario?: TipoUsuario;
};

export type Produto = {
  id_prod: number;
  empresa_cod: number;
  cod_prod: string;
  desc_prod: string;
  val_prod: number | null;
  usuario?: number | null;
  excluido: boolean;
};

export type Cliente = {
  id_cliente: number;
  cpf_cliente?: string | null;
  nome_cliente: string;
  tel_cliente?: string | null;
  saldo?: number;
  Saldo?: number;
};

export type Compra = {
  id_compra: number;
  data_compra: string;
  desconto_compra: number;
  subtot_compra: number;
  valor_nota: number;
  id_cliente?: number | null;
  usuario: number;
};

export type ItemCompra = {
  id_item: number;
  id_prod: number;
  cod_prod: string;
  id_compra: number;
  quant_item: number;
  subtot_item?: number;
  subTot_item?: number;
  valor_item: number;
  desc_prod?: string;
};

export type Venda = {
  id_venda: number;
  data_venda: string;
  valor_nota: number;
  usuario?: number | null;
};

export type ItemVenda = {
  id_item: number;
  id_prod?: number | null;
  cod_prod?: string | null;
  id_venda?: number | null;
  quant_item?: number | null;
  subtot_item?: number | null;
  subTot_item?: number | null;
  valr_item?: number | null;
  desc_prod?: string;
};
```

## 18. Prioridade recomendada para migracao

### Fase 1 - Base

1. Criar projeto React com Vite + TypeScript.
2. Criar `apiClient`.
3. Criar login.
4. Criar layout autenticado.
5. Criar controle de permissao Administrador/Operacional.

### Fase 2 - Cadastros

1. Produtos.
2. Clientes.
3. Usuarios.

Essas telas validam boa parte do contrato da API sem mexer nos fluxos financeiros mais delicados.

### Fase 3 - Operacoes principais

1. Compra.
2. Venda.
3. Outras entradas/saidas.
4. Recursos de caixa.
5. Pagamento/recebimento/acerto de cliente.

### Fase 4 - Consultas

1. Estoque.
2. Notas de compra.
3. Notas de venda.
4. Movimentacao de recursos.
5. Movimentacao de clientes.

### Fase 5 - Relatorios e impressao

1. Relatorios em tela.
2. Exportacao/print browser.
3. PDF se necessario.
4. Comprovantes.

## 19. Pontos de atencao antes da web

1. Autenticacao: hoje nao ha token JWT/sessao robusta. A web precisa de autenticacao real no backend.
2. Senha: o fluxo atual usa senha em texto simples. Migrar para hash no banco.
3. Autorizacao: regras de permissao estao principalmente na UI. Ideal validar permissoes tambem na API.
4. Impressao: RDLC/ReportViewer nao existe no navegador. Definir PDF/HTML imprimivel.
5. Operacoes em andamento: compra e venda criam nota antes da finalizacao. No React, tratar abandono de pagina e cancelamento.
6. Caixa e compra: o desktop valida saldo de caixa para comprar, mas nao grava automaticamente uma saida de caixa na compra. Confirmar regra de negocio.
7. Venda imprime sempre no codigo atual. Decidir se corrige na web.
8. Nomes e campos legados: ha variacoes como `subTot_item` e `subtot_item`, `id_tipoUsuario` e `id_tipousuario`, `valr_item`. O frontend deve normalizar.
9. Decimais: usar componentes controlados para evitar erro de separador decimal `,` vs `.`.
10. Multiempresa: nunca permitir chamada de negocio sem `empresa_cod`.

## 20. Checklist de telas para validar equivalencia

| Modulo | Tela React | API validada | Prioridade |
| --- | --- | --- | --- |
| Login | `/login` | `/health`, `/api/auth/login` | Alta |
| Produtos | `/cadastros/produtos` | `/api/produtos` | Alta |
| Clientes | `/cadastros/clientes` | `/api/clientes` | Alta |
| Usuarios | `/cadastros/usuarios` | `/api/usuarios` | Media |
| Compra | `/operacoes/compras/nova` | `/api/compras` | Alta |
| Venda | `/operacoes/vendas/nova` | `/api/vendas` | Alta |
| Estoque | `/consultas/estoque` | `/api/estoque`, `/api/produtos/:cod/saldo` | Alta |
| Caixa | `/operacoes/caixa/recursos` | `/api/caixa` | Alta |
| Cliente financeiro | pagamento/recebimento/acerto | `/api/caixa`, `/api/clientes/:id/acertos` | Alta |
| Notas | compras/vendas | `/api/compras`, `/api/vendas` | Media |
| Relatorios | varios | `/api/relatorios/*` | Media |
| Configuracoes | cabecalho/impressao | `/api/empresas/:id/cabecalho`, `/api/impressoras/:id` | Baixa |

## 21. Referencias de codigo importantes

Desktop:

- `FerroVelho/Program.cs`
- `FerroVelho/Iniciação/fm_login.cs`
- `FerroVelho/Iniciação/fm_menuPrincipal.cs`
- `FerroVelho/Operaçoes/fm_vender.cs`
- `FerroVelho/Cadastro/fm_cadProduto.cs`
- `FerroVelho/Cadastro/fm_cadCliente.cs`
- `FerroVelho/Cadastro/fm_cadUsuario.cs`
- `FerroVelho/Consulta/fm_estoque.cs`
- `FerroVelhoDAO/DataContextFactory.cs`
- `FerroVelhoDAO/ApiConnectionService.cs`
- `FerroVelhoDAO/Models.cs`

API:

- `../Ferro_Velho_API/src/routers/ferroVelho.router.js`
- `../Ferro_Velho_API/src/services/ferroVelho.service.js`
- `../Ferro_Velho_API/src/migrations/*`
- `../Ferro_Velho_API/Ferro_Velho.postman_collection.json`

## 22. Conclusao pratica

A versao React deve ser tratada como uma nova interface para a API existente, nao como uma conversao literal de WinForms.

O desktop serve como referencia de:

- fluxos de negocio;
- validacoes;
- nomes de campos;
- ordem das operacoes;
- relatorios existentes;
- permissoes de menu.

A API existente ja cobre praticamente todos os casos necessarios para a primeira versao web. Os maiores ajustes antes de producao web sao seguranca de autenticacao, padronizacao de contratos e substituicao da estrategia de impressao.
