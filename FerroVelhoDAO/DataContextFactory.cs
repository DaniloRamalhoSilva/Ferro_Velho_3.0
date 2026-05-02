using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace FerroVelhoDAO
{
    public class DataContextFactory
    {
        private const string DefaultApiUrl = "http://localhost:3000";
        private const string CabecalhoFileName = "cabe\u00e7alho.xml";
        private const string ConfiguracaoFileName = "configura\u00e7\u00e3o.xml";

        public static tb_usuario usu;
        public static string nome;
        public static string tel;
        public static string endereco;
        public static string conexaoUser;
        public static string conexaoImp;
        private static int? empresaCodAutenticada;

        public static string ApiBaseUrl
        {
            get
            {
                if (string.IsNullOrWhiteSpace(conexaoUser))
                {
                    return DefaultApiUrl;
                }

                var value = conexaoUser.Trim();
                return value.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                       value.StartsWith("https://", StringComparison.OrdinalIgnoreCase)
                    ? value
                    : DefaultApiUrl;
            }
        }

        public static int EmpresaCod
        {
            get
            {
                if (!empresaCodAutenticada.HasValue || empresaCodAutenticada.Value <= 0)
                {
                    throw new InvalidOperationException("Empresa nao definida. Realize o login antes de acessar dados da API.");
                }

                return empresaCodAutenticada.Value;
            }
        }

        public static tb_usuario ValidarLoginApi(string nomeUsuario, string senhaUsuario)
        {
            return ApiConnectionService.ValidarLogin(ApiBaseUrl, nomeUsuario, senhaUsuario);
        }

        public static void DefinirUsuarioAutenticado(tb_usuario usuario)
        {
            if (usuario == null)
            {
                throw new InvalidOperationException("Usuario autenticado nao informado.");
            }

            if (usuario.empresa_cod <= 0)
            {
                throw new InvalidOperationException("Login retornou usuario sem empresa_cod valido.");
            }

            usu = usuario;
            empresaCodAutenticada = usuario.empresa_cod;
            conexaoImp = usuario.empresa_cod.ToString();
        }

        public static void LimparSessao()
        {
            usu = null;
            empresaCodAutenticada = null;
            conexaoImp = string.Empty;
        }

        public static bool TestarConexaoApi()
        {
            return ApiConnectionService.TestarConexao(ApiBaseUrl);
        }

        public static List<tb_produtos> ListarProdutosApi(bool incluirExcluidos = false, bool incluirExcluidosComSaldo = false)
        {
            return ApiConnectionService.ListarProdutos(ApiBaseUrl, EmpresaCod, incluirExcluidos, incluirExcluidosComSaldo);
        }

        public static bool ExisteProdutoApi(string codigo, int? excetoIdProd)
        {
            return ApiConnectionService.ExisteProdutoPorCodigo(ApiBaseUrl, EmpresaCod, codigo, excetoIdProd);
        }

        public static tb_produtos CriarProdutoApi(string codigo, string descricao, decimal valor, int? usuario)
        {
            return ApiConnectionService.CriarProduto(ApiBaseUrl, EmpresaCod, codigo, descricao, valor, usuario);
        }

        public static void AtualizarProdutoApi(int idProd, string codigo, string descricao, decimal valor)
        {
            ApiConnectionService.AtualizarProduto(ApiBaseUrl, EmpresaCod, idProd, codigo, descricao, valor);
        }

        public static void ExcluirProdutoApi(int idProd)
        {
            ApiConnectionService.ExcluirProduto(ApiBaseUrl, EmpresaCod, idProd);
        }

        public static tb_impressora BuscarImpressoraApi(int idImpressora)
        {
            return ApiConnectionService.BuscarImpressoraPorId(ApiBaseUrl, EmpresaCod, idImpressora);
        }

        public static void SalvarImpressoraApi(int idImpressora, string nomeImpressora)
        {
            ApiConnectionService.SalvarImpressora(ApiBaseUrl, EmpresaCod, idImpressora, nomeImpressora);
        }

        public static List<tb_tipoUsuario> ListarTiposUsuarioApi()
        {
            return ApiConnectionService.ListarTiposUsuario(ApiBaseUrl, EmpresaCod);
        }

        public static List<tb_usuario> ListarUsuariosApi(bool incluirInativos)
        {
            return ApiConnectionService.ListarUsuarios(ApiBaseUrl, EmpresaCod, incluirInativos);
        }

        public static bool ExisteUsuarioApi(string nomeUsuario, int? excetoIdUsuario)
        {
            return ApiConnectionService.ExisteUsuarioPorNome(ApiBaseUrl, EmpresaCod, nomeUsuario, excetoIdUsuario);
        }

        public static void CriarUsuarioApi(string nomeUsuario, string senhaUsuario, int permissaoUsuario)
        {
            ApiConnectionService.CriarUsuario(ApiBaseUrl, EmpresaCod, nomeUsuario, senhaUsuario, permissaoUsuario);
        }

        public static void AtualizarUsuarioApi(int idUsuario, string nomeUsuario, string senhaUsuario, int permissaoUsuario)
        {
            ApiConnectionService.AtualizarUsuario(ApiBaseUrl, EmpresaCod, idUsuario, nomeUsuario, senhaUsuario, permissaoUsuario);
        }

        public static void DefinirUsuarioAtivoApi(int idUsuario, bool ativo)
        {
            ApiConnectionService.DefinirUsuarioAtivo(ApiBaseUrl, EmpresaCod, idUsuario, ativo);
        }

        public static List<tb_cliente> ListarClientesApi(string filtroCampo = null, string filtroValor = null)
        {
            return ApiConnectionService.ListarClientes(ApiBaseUrl, EmpresaCod, filtroCampo, filtroValor);
        }

        public static void InserirClienteApi(string nomeCliente, string cpfCliente, string telCliente)
        {
            ApiConnectionService.InserirCliente(ApiBaseUrl, EmpresaCod, nomeCliente, cpfCliente, telCliente);
        }

        public static void AtualizarClienteApi(int idCliente, string nomeCliente, string cpfCliente, string telCliente)
        {
            ApiConnectionService.AtualizarCliente(ApiBaseUrl, EmpresaCod, idCliente, nomeCliente, cpfCliente, telCliente);
        }

        public static void ExcluirClienteApi(int idCliente)
        {
            ApiConnectionService.ExcluirCliente(ApiBaseUrl, EmpresaCod, idCliente);
        }

        public static DataTable CarregarMovimentacaoClienteApi(int idCliente, bool resumido)
        {
            return ApiConnectionService.CarregarMovimentacaoCliente(ApiBaseUrl, EmpresaCod, idCliente, resumido);
        }

        public static string BuscarNomeUsuarioApi(int idUsuario)
        {
            return ApiConnectionService.BuscarNomeUsuario(ApiBaseUrl, EmpresaCod, idUsuario);
        }

        public static string BuscarNomeClienteApi(int idCliente)
        {
            return ApiConnectionService.BuscarNomeCliente(ApiBaseUrl, EmpresaCod, idCliente);
        }

        public static decimal CalcularValorDevedorApi(int idCliente)
        {
            return ApiConnectionService.CalcularValorDevedor(ApiBaseUrl, EmpresaCod, idCliente);
        }

        public static decimal CalcularValorCreditoApi(int idCliente)
        {
            return ApiConnectionService.CalcularValorCredito(ApiBaseUrl, EmpresaCod, idCliente);
        }

        public static tb_compra CriarCompraApi(DateTime dataCompra, int usuario, decimal descontoCompra, decimal subtotCompra, decimal valorNota)
        {
            return ApiConnectionService.CriarCompra(ApiBaseUrl, EmpresaCod, dataCompra, usuario, descontoCompra, subtotCompra, valorNota);
        }

        public static void AtualizarCompraApi(int idCompra, decimal descontoCompra, decimal subtotCompra, decimal valorNota, int? idCliente)
        {
            ApiConnectionService.AtualizarCompraValores(ApiBaseUrl, EmpresaCod, idCompra, descontoCompra, subtotCompra, valorNota, idCliente);
        }

        public static void InserirItemCompraApi(string codigoProduto, int idCompra, decimal quantItem, decimal subTotItem, decimal valorItem)
        {
            ApiConnectionService.InserirItemCompra(ApiBaseUrl, EmpresaCod, codigoProduto, idCompra, quantItem, subTotItem, valorItem);
        }

        public static List<tb_itemc> ListarItensCompraApi(int idCompra)
        {
            return ApiConnectionService.ListarItensCompra(ApiBaseUrl, EmpresaCod, idCompra);
        }

        public static void ExcluirItemCompraApi(int idItem)
        {
            ApiConnectionService.ExcluirItemCompra(ApiBaseUrl, EmpresaCod, idItem);
        }

        public static void ExcluirCompraApi(int idCompra)
        {
            ApiConnectionService.ExcluirCompra(ApiBaseUrl, EmpresaCod, idCompra);
        }

        public static decimal CalcularSaldoProdutoApi(string codigoProduto)
        {
            return ApiConnectionService.CalcularSaldoProduto(ApiBaseUrl, EmpresaCod, codigoProduto);
        }

        public static decimal CalcularSaldoCaixaApi()
        {
            return ApiConnectionService.CalcularSaldoCaixa(ApiBaseUrl, EmpresaCod);
        }

        public static void InserirCaixaApi(DateTime dataCaixa, string descricao, int usuario, decimal valorCaixa, int? idCliente)
        {
            ApiConnectionService.InserirCaixa(ApiBaseUrl, EmpresaCod, dataCaixa, descricao, usuario, valorCaixa, idCliente);
        }

        public static void InserirAcertoClienteApi(DateTime dataAcerto, string descricao, int usuario, decimal valorAcerto, int idCliente)
        {
            ApiConnectionService.InserirAcertoCliente(ApiBaseUrl, EmpresaCod, dataAcerto, descricao, usuario, valorAcerto, idCliente);
        }

        public static DataTable CarregarMovimentacaoRecursosApi(DateTime inicio, DateTime fim)
        {
            return ApiConnectionService.CarregarMovimentacaoRecursos(ApiBaseUrl, EmpresaCod, inicio, fim);
        }

        public static DataTable ListarComprasApi(DateTime? inicio, DateTime? fim, int? idCompra)
        {
            return ApiConnectionService.ListarCompras(ApiBaseUrl, EmpresaCod, inicio, fim, idCompra);
        }

        public static DataTable ListarVendasApi(DateTime? inicio, DateTime? fim, int? idVenda)
        {
            return ApiConnectionService.ListarVendas(ApiBaseUrl, EmpresaCod, inicio, fim, idVenda);
        }

        public static DataTable CarregarEstoqueAtualApi()
        {
            return ApiConnectionService.CarregarEstoqueAtual(ApiBaseUrl, EmpresaCod);
        }

        public static DataTable CarregarResumoCompraProdutosApi(DateTime inicio, DateTime fim)
        {
            return ApiConnectionService.CarregarResumoCompraProdutos(ApiBaseUrl, EmpresaCod, inicio, fim);
        }

        public static DataTable CarregarRelatorioCompraItensApi(int idCompra)
        {
            return ApiConnectionService.CarregarRelatorioCompraItens(ApiBaseUrl, EmpresaCod, idCompra);
        }

        public static DataTable CarregarRelatorioCompraCabecalhoApi(int idCompra)
        {
            return ApiConnectionService.CarregarRelatorioCompraCabecalho(ApiBaseUrl, EmpresaCod, idCompra);
        }

        public static DataTable CarregarRelatorioCompraClienteApi(int idCompra)
        {
            return ApiConnectionService.CarregarRelatorioCompraCliente(ApiBaseUrl, EmpresaCod, idCompra);
        }

        public static DataTable CalcularResumoCompraCaixaApi(DateTime inicio, DateTime fim)
        {
            return ApiConnectionService.CalcularResumoCompraCaixa(ApiBaseUrl, EmpresaCod, inicio, fim);
        }

        public static DataTable CarregarResumoVendaProdutosApi(DateTime inicio, DateTime fim)
        {
            return ApiConnectionService.CarregarResumoVendaProdutos(ApiBaseUrl, EmpresaCod, inicio, fim);
        }

        public static decimal CalcularTotalVendaProdutosApi(DateTime inicio, DateTime fim)
        {
            return ApiConnectionService.CalcularTotalVendaProdutos(ApiBaseUrl, EmpresaCod, inicio, fim);
        }

        public static DataTable CarregarLucroDetalhadoApi(DateTime inicio, DateTime fim)
        {
            return ApiConnectionService.CarregarLucroDetalhado(ApiBaseUrl, EmpresaCod, inicio, fim);
        }

        public static DataTable CarregarLucroTotalApi(DateTime inicio, DateTime fim)
        {
            return ApiConnectionService.CarregarLucroTotal(ApiBaseUrl, EmpresaCod, inicio, fim);
        }

        public static DataTable CarregarFluxoCaixaApi(DateTime inicio, DateTime fim)
        {
            return ApiConnectionService.CarregarFluxoCaixa(ApiBaseUrl, EmpresaCod, inicio, fim);
        }

        public static decimal CalcularSaldoInicialFluxoCaixaApi(DateTime inicio)
        {
            return ApiConnectionService.CalcularSaldoInicialFluxoCaixa(ApiBaseUrl, EmpresaCod, inicio);
        }

        public static DataTable CarregarEstoquePeriodoApi(DateTime inicio, DateTime fim, string codigoProduto)
        {
            return ApiConnectionService.CarregarEstoquePeriodo(ApiBaseUrl, EmpresaCod, inicio, fim, codigoProduto);
        }

        public static DataTable CarregarProdutosDataTableApi(bool incluirExcluidos = true)
        {
            return ApiConnectionService.CarregarProdutosDataTable(ApiBaseUrl, EmpresaCod, incluirExcluidos);
        }

        public static tb_venda CriarVendaApi(DateTime dataVenda, int usuario, decimal valorNota)
        {
            return ApiConnectionService.CriarVenda(ApiBaseUrl, EmpresaCod, dataVenda, usuario, valorNota);
        }

        public static void AtualizarVendaApi(int idVenda, decimal valorNota, int usuario)
        {
            ApiConnectionService.AtualizarVenda(ApiBaseUrl, EmpresaCod, idVenda, valorNota, usuario);
        }

        public static void InserirItemVendaApi(string codigoProduto, int idVenda, decimal quantItem, decimal subTotItem, decimal valrItem)
        {
            ApiConnectionService.InserirItemVenda(ApiBaseUrl, EmpresaCod, codigoProduto, idVenda, quantItem, subTotItem, valrItem);
        }

        public static List<tb_itemv> ListarItensVendaApi(int idVenda)
        {
            return ApiConnectionService.ListarItensVenda(ApiBaseUrl, EmpresaCod, idVenda);
        }

        public static void ExcluirItemVendaApi(int idItem)
        {
            ApiConnectionService.ExcluirItemVenda(ApiBaseUrl, EmpresaCod, idItem);
        }

        public static void ExcluirVendaApi(int idVenda)
        {
            ApiConnectionService.ExcluirVenda(ApiBaseUrl, EmpresaCod, idVenda);
        }

        public static DataTable CarregarRelatorioVendaApi(int idVenda)
        {
            return ApiConnectionService.CarregarDadosRelatorioVenda(ApiBaseUrl, EmpresaCod, idVenda);
        }

        public static void GravarCabecario(string nome, string tel, string endereco)
        {
            XmlTextWriter STW_Arquivo;
            STW_Arquivo = new XmlTextWriter(GetAppFilePath(CabecalhoFileName), Encoding.UTF8);
            STW_Arquivo.WriteStartElement("cabecario");
            STW_Arquivo.WriteElementString("Nome", nome.Trim());
            STW_Arquivo.WriteElementString("Telefone", tel.Trim());
            STW_Arquivo.WriteElementString("Endereco", endereco.Trim());
            STW_Arquivo.WriteEndElement();
            STW_Arquivo.Close();
        }

        public static void GravarConecxao(string apiUrl)
        {
            XmlTextWriter STW_Arquivo;
            STW_Arquivo = new XmlTextWriter(GetAppFilePath(ConfiguracaoFileName), Encoding.UTF8);
            STW_Arquivo.WriteStartElement("configConexao");
            STW_Arquivo.WriteElementString("apiUrl", string.IsNullOrWhiteSpace(apiUrl) ? DefaultApiUrl : apiUrl.Trim());
            STW_Arquivo.WriteEndElement();
            STW_Arquivo.Close();
        }

        public static void FU_lerCabecario()
        {
            try
            {
                XElement XML = XElement.Load(ResolveConfigPath(CabecalhoFileName));

                nome = XML.Element("Nome").Value;
                tel = XML.Element("Telefone").Value;
                endereco = XML.Element("Endereco").Value;

                XML = null;
            }
            catch
            {
                nome = "";
                tel = "";
                endereco = "";
            }
        }

        public static void FU_lerConfiguracao()
        {
            try
            {
                XElement XML = XElement.Load(ResolveConfigPath(ConfiguracaoFileName));

                conexaoUser = GetXmlValue(XML, "apiUrl", "dataUser");
                if (string.IsNullOrWhiteSpace(conexaoUser) ||
                    (!conexaoUser.StartsWith("http://", StringComparison.OrdinalIgnoreCase) &&
                     !conexaoUser.StartsWith("https://", StringComparison.OrdinalIgnoreCase)))
                {
                    conexaoUser = DefaultApiUrl;
                }

                conexaoImp = empresaCodAutenticada.HasValue ? empresaCodAutenticada.Value.ToString() : string.Empty;
                XML = null;
            }
            catch
            {
                conexaoUser = DefaultApiUrl;
                conexaoImp = empresaCodAutenticada.HasValue ? empresaCodAutenticada.Value.ToString() : string.Empty;
            }
        }

        private static string ResolveConfigPath(string fileName)
        {
            var appPath = GetAppFilePath(fileName);
            if (File.Exists(appPath))
            {
                return appPath;
            }

            var legacyPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\", fileName));
            return File.Exists(legacyPath) ? legacyPath : appPath;
        }

        private static string GetAppFilePath(string fileName)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
        }

        private static string GetXmlValue(XElement xml, string currentName, string legacyName)
        {
            var current = xml.Element(currentName);
            if (current != null)
            {
                return current.Value;
            }

            var legacy = xml.Element(legacyName);
            return legacy == null ? string.Empty : legacy.Value;
        }
    }
}
