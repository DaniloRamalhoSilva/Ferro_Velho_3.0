 # ♻️ Ferro Velho 3.0

Bem-vindo ao **Ferro Velho 3.0**, um sistema de gerenciamento para ferros-velhos e cooperativas de reciclagem. Este projeto foi desenvolvido em **C# / Windows Forms** e utiliza banco de dados SQL Server via **LINQ to SQL**.

## 📚 Índice
- [Funcionalidades](#funcionalidades)
- [Requisitos](#requisitos)
- [Instalação](#instalação)
- [Uso](#uso)
- [Scripts de Banco](#scripts-de-banco)

## ✅ Funcionalidades
- Cadastro de usuários, clientes e produtos
- Operações de compra e venda
- Controle de estoque e movimentações financeiras
- Emissão de relatórios (vendas, compras, financeiro, estoque)
- Gerenciamento de usuários com bloqueio/reativação
- Suporte a impressão de comprovantes e relatórios

## 📋 Requisitos
- Windows com **.NET Framework 4.5** ou superior
- **SQL Server** para o banco de dados
- **Visual Studio 2017** ou mais recente
- **SQL Server Data Tools** instalado no Visual Studio (necessário para criação e execução de projetos de banco de dados)
- **LINQ to SQL Tools** instalado no Visual Studio (habilita o designer e o runtime de `.dbml`)

## 🛠️ Instalação
1. Clone este repositório
2. Restaure os pacotes NuGet (`Restaurar Pacotes` no Visual Studio)
3. Execute os scripts de banco disponíveis em [`BdScrips`](BdScrips/) no seu SQL Server
4. Ajuste as strings de conexão em `configuração.xml`
5. Abra `Sistema.sln` no Visual Studio e compile o projeto

## 💻 Uso
Ao iniciar o aplicativo, será apresentada a tela de login. No menu principal estão disponíveis módulos de:

- **Cadastros**: usuários, clientes e produtos
- **Consultas**: notas de compra/venda, estoque e movimentação de recursos
- **Operações**: vendas, recebimentos, adiantamentos e outras entradas/saídas
- **Relatórios**: financeiros e de estoque

## 📁 Scripts de Banco
A pasta [`BdScrips`](BdScrips/) contém os scripts de criação de tabelas, procedures e atualizações. Importante executar `u_tb_usuario_add_ativo.sql` para adicionar a coluna `ativo` na tabela `tb_usuario`.

