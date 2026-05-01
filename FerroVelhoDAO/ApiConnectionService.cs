using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Script.Serialization;

namespace FerroVelhoDAO
{
    public static class ApiConnectionService
    {
        private static readonly HttpClient Http = new HttpClient();
        private static readonly JavaScriptSerializer Serializer = new JavaScriptSerializer();

        public static bool TestarConexao(string apiUrl)
        {
            GetObject(apiUrl, "/health", false, 0);
            return true;
        }

        public static tb_usuario ValidarLogin(string apiUrl, string nomeUsuario, string senhaUsuario)
        {
            var row = PostPublicRow(apiUrl, "/api/auth/login", new Dictionary<string, object>
            {
                { "nome_usuario", nomeUsuario ?? string.Empty },
                { "senha_usuario", senhaUsuario ?? string.Empty }
            });

            return row == null ? null : MapUsuario(row);
        }

        public static List<tb_produtos> ListarProdutos(
            string apiUrl,
            int empresaCod,
            bool incluirExcluidos = false,
            bool incluirExcluidosComSaldo = false)
        {
            var query = BuildQuery(new Dictionary<string, string>
            {
                { "incluirExcluidos", incluirExcluidos ? "true" : null },
                { "incluirExcluidosComSaldo", incluirExcluidosComSaldo ? "true" : null }
            });

            return GetRows(apiUrl, empresaCod, "/api/produtos" + query).Select(MapProduto).ToList();
        }

        public static bool ExisteProdutoPorCodigo(string apiUrl, int empresaCod, string codigo, int? excetoIdProd)
        {
            string codigoNormalizado = (codigo ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(codigoNormalizado))
            {
                return false;
            }

            return ListarProdutos(apiUrl, empresaCod, true).Any(produto =>
                produto.empresa_cod == empresaCod &&
                string.Equals((produto.cod_prod ?? string.Empty).Trim(), codigoNormalizado, StringComparison.Ordinal) &&
                (!excetoIdProd.HasValue || produto.id_prod != excetoIdProd.Value));
        }

        public static tb_produtos CriarProduto(string apiUrl, int empresaCod, string codigo, string descricao, decimal valor, int? usuario)
        {
            return MapProduto(PostRow(apiUrl, empresaCod, "/api/produtos", new Dictionary<string, object>
            {
                { "cod_prod", codigo },
                { "desc_prod", descricao },
                { "val_prod", valor },
                { "usuario", usuario }
            }));
        }

        public static void AtualizarProduto(string apiUrl, int empresaCod, int idProd, string codigo, string descricao, decimal valor)
        {
            PutRow(apiUrl, empresaCod, "/api/produtos/" + idProd, new Dictionary<string, object>
            {
                { "cod_prod", codigo },
                { "desc_prod", descricao },
                { "val_prod", valor }
            });
        }

        public static void ExcluirProduto(string apiUrl, int empresaCod, int idProd)
        {
            DeleteRow(apiUrl, empresaCod, "/api/produtos/" + idProd);
        }

        public static tb_impressora BuscarImpressoraPorId(string apiUrl, int empresaCod, int idImpressora)
        {
            var row = GetRow(apiUrl, empresaCod, "/api/impressoras/" + idImpressora);
            return row == null ? null : MapImpressora(row);
        }

        public static void SalvarImpressora(string apiUrl, int empresaCod, int idImpressora, string nomeImpressora)
        {
            PutRow(apiUrl, empresaCod, "/api/impressoras/" + idImpressora, new Dictionary<string, object>
            {
                { "nome_impressora", nomeImpressora }
            });
        }

        public static List<tb_tipoUsuario> ListarTiposUsuario(string apiUrl, int empresaCod)
        {
            return GetRows(apiUrl, empresaCod, "/api/tipos-usuario").Select(MapTipoUsuario).ToList();
        }

        public static List<tb_usuario> ListarUsuarios(string apiUrl, int empresaCod, bool incluirInativos)
        {
            var path = "/api/usuarios?incluirInativos=" + incluirInativos.ToString().ToLowerInvariant();
            return GetRows(apiUrl, empresaCod, path).Select(MapUsuario).ToList();
        }

        public static bool ExisteUsuarioPorNome(string apiUrl, int empresaCod, string nomeUsuario, int? excetoIdUsuario)
        {
            var path = "/api/usuarios/existe?nome_usuario=" + Escape(nomeUsuario);
            if (excetoIdUsuario.HasValue)
            {
                path += "&exceto_id=" + excetoIdUsuario.Value;
            }

            var row = GetRow(apiUrl, empresaCod, path);
            return Bool(row, "existe");
        }

        public static void CriarUsuario(string apiUrl, int empresaCod, string nomeUsuario, string senhaUsuario, int permissaoUsuario)
        {
            PostRow(apiUrl, empresaCod, "/api/usuarios", new Dictionary<string, object>
            {
                { "nome_usuario", nomeUsuario },
                { "senha_usuario", senhaUsuario },
                { "permi_usuario", permissaoUsuario }
            });
        }

        public static void AtualizarUsuario(string apiUrl, int empresaCod, int idUsuario, string nomeUsuario, string senhaUsuario, int permissaoUsuario)
        {
            PutRow(apiUrl, empresaCod, "/api/usuarios/" + idUsuario, new Dictionary<string, object>
            {
                { "nome_usuario", nomeUsuario },
                { "senha_usuario", senhaUsuario },
                { "permi_usuario", permissaoUsuario }
            });
        }

        public static void DefinirUsuarioAtivo(string apiUrl, int empresaCod, int idUsuario, bool ativo)
        {
            PatchRow(apiUrl, empresaCod, "/api/usuarios/" + idUsuario + "/ativo", new Dictionary<string, object>
            {
                { "ativo", ativo }
            });
        }

        public static List<tb_cliente> ListarClientes(string apiUrl, int empresaCod, string filtroCampo, string filtroValor)
        {
            var path = "/api/clientes";
            if (!string.IsNullOrWhiteSpace(filtroValor))
            {
                path += "?filtroCampo=" + Escape(filtroCampo) + "&filtroValor=" + Escape(filtroValor);
            }

            return GetRows(apiUrl, empresaCod, path).Select(MapCliente).ToList();
        }

        public static void InserirCliente(string apiUrl, int empresaCod, string nomeCliente, string cpfCliente, string telCliente)
        {
            PostRow(apiUrl, empresaCod, "/api/clientes", new Dictionary<string, object>
            {
                { "nome_cliente", nomeCliente },
                { "cpf_cliente", cpfCliente },
                { "tel_cliente", telCliente }
            });
        }

        public static void AtualizarCliente(string apiUrl, int empresaCod, int idCliente, string nomeCliente, string cpfCliente, string telCliente)
        {
            PutRow(apiUrl, empresaCod, "/api/clientes/" + idCliente, new Dictionary<string, object>
            {
                { "nome_cliente", nomeCliente },
                { "cpf_cliente", cpfCliente },
                { "tel_cliente", telCliente }
            });
        }

        public static void ExcluirCliente(string apiUrl, int empresaCod, int idCliente)
        {
            DeleteRow(apiUrl, empresaCod, "/api/clientes/" + idCliente);
        }

        public static DataTable CarregarMovimentacaoCliente(string apiUrl, int empresaCod, int idCliente, bool resumido)
        {
            return ToDataTable(GetRows(apiUrl, empresaCod, "/api/clientes/" + idCliente + "/movimentacao?resumido=" + resumido.ToString().ToLowerInvariant()));
        }

        public static string BuscarNomeUsuario(string apiUrl, int empresaCod, int idUsuario)
        {
            return String(GetRow(apiUrl, empresaCod, "/api/usuarios/" + idUsuario + "/nome"), "nome_usuario");
        }

        public static string BuscarNomeCliente(string apiUrl, int empresaCod, int idCliente)
        {
            return String(GetRow(apiUrl, empresaCod, "/api/clientes/" + idCliente + "/nome"), "nome_cliente");
        }

        public static decimal CalcularValorDevedor(string apiUrl, int empresaCod, int idCliente)
        {
            return Decimal(GetRow(apiUrl, empresaCod, "/api/clientes/" + idCliente + "/saldo?tipo=devedor"), "valor");
        }

        public static decimal CalcularValorCredito(string apiUrl, int empresaCod, int idCliente)
        {
            return Decimal(GetRow(apiUrl, empresaCod, "/api/clientes/" + idCliente + "/saldo?tipo=credito"), "valor");
        }

        public static tb_compra CriarCompra(string apiUrl, int empresaCod, DateTime dataCompra, int usuario, decimal descontoCompra, decimal subtotCompra, decimal valorNota)
        {
            return MapCompra(PostRow(apiUrl, empresaCod, "/api/compras", new Dictionary<string, object>
            {
                { "data_compra", ApiDate(dataCompra) },
                { "usuario", usuario },
                { "desconto_compra", descontoCompra },
                { "subtot_compra", subtotCompra },
                { "valor_nota", valorNota }
            }));
        }

        public static void AtualizarCompraValores(string apiUrl, int empresaCod, int idCompra, decimal descontoCompra, decimal subtotCompra, decimal valorNota, int? idCliente)
        {
            PutRow(apiUrl, empresaCod, "/api/compras/" + idCompra, new Dictionary<string, object>
            {
                { "desconto_compra", descontoCompra },
                { "subtot_compra", subtotCompra },
                { "valor_nota", valorNota },
                { "id_cliente", idCliente }
            });
        }

        public static void InserirItemCompra(string apiUrl, int empresaCod, string codigoProduto, int idCompra, decimal quantItem, decimal subTotItem, decimal valorItem)
        {
            PostRow(apiUrl, empresaCod, "/api/compras/" + idCompra + "/itens", new Dictionary<string, object>
            {
                { "cod_prod", codigoProduto },
                { "quant_item", quantItem },
                { "subtot_item", subTotItem },
                { "valor_item", valorItem }
            });
        }

        public static List<tb_itemc> ListarItensCompra(string apiUrl, int empresaCod, int idCompra)
        {
            return GetRows(apiUrl, empresaCod, "/api/compras/" + idCompra + "/itens").Select(MapItemCompra).ToList();
        }

        public static void ExcluirItemCompra(string apiUrl, int empresaCod, int idItem)
        {
            DeleteRow(apiUrl, empresaCod, "/api/compras/itens/" + idItem);
        }

        public static void ExcluirCompra(string apiUrl, int empresaCod, int idCompra)
        {
            DeleteRow(apiUrl, empresaCod, "/api/compras/" + idCompra);
        }

        public static decimal CalcularSaldoProduto(string apiUrl, int empresaCod, string codigoProduto)
        {
            return Decimal(GetRow(apiUrl, empresaCod, "/api/produtos/" + Escape(codigoProduto) + "/saldo"), "saldo");
        }

        public static decimal CalcularSaldoCaixa(string apiUrl, int empresaCod)
        {
            return Decimal(GetRow(apiUrl, empresaCod, "/api/caixa/saldo"), "saldo");
        }

        public static void InserirCaixa(string apiUrl, int empresaCod, DateTime dataCaixa, string descricao, int usuario, decimal valorCaixa, int? idCliente)
        {
            PostRow(apiUrl, empresaCod, "/api/caixa", new Dictionary<string, object>
            {
                { "data_caixa", ApiDate(dataCaixa) },
                { "desc_caixa", descricao },
                { "usuario", usuario },
                { "valor_caixa", valorCaixa },
                { "id_cliente", idCliente }
            });
        }

        public static void InserirAcertoCliente(string apiUrl, int empresaCod, DateTime dataAcerto, string descricao, int usuario, decimal valorAcerto, int idCliente)
        {
            PostRow(apiUrl, empresaCod, "/api/clientes/" + idCliente + "/acertos", new Dictionary<string, object>
            {
                { "data_acliente", ApiDate(dataAcerto) },
                { "desc_acliente", descricao },
                { "usuario", usuario },
                { "valor_acliente", valorAcerto }
            });
        }

        public static DataTable CarregarMovimentacaoRecursos(string apiUrl, int empresaCod, DateTime inicio, DateTime fim)
        {
            return ToDataTable(GetRows(apiUrl, empresaCod, "/api/recursos/movimentacao?inicio=" + ApiDate(inicio.Date) + "&fim=" + ApiDate(fim.Date)));
        }

        public static DataTable ListarCompras(string apiUrl, int empresaCod, DateTime? inicio, DateTime? fim, int? idCompra)
        {
            var query = BuildQuery(new Dictionary<string, string>
            {
                { "inicio", inicio.HasValue ? ApiDate(inicio.Value) : null },
                { "fim", fim.HasValue ? ApiDate(fim.Value) : null },
                { "id_compra", idCompra.HasValue ? idCompra.Value.ToString(CultureInfo.InvariantCulture) : null }
            });

            return ToDataTable(GetRows(apiUrl, empresaCod, "/api/compras" + query), "id_compra", "data_compra", "desconto_compra", "subtot_compra", "valor_nota", "usuario", "id_cliente");
        }

        public static DataTable ListarVendas(string apiUrl, int empresaCod, DateTime? inicio, DateTime? fim, int? idVenda)
        {
            var query = BuildQuery(new Dictionary<string, string>
            {
                { "inicio", inicio.HasValue ? ApiDate(inicio.Value) : null },
                { "fim", fim.HasValue ? ApiDate(fim.Value) : null },
                { "id_venda", idVenda.HasValue ? idVenda.Value.ToString(CultureInfo.InvariantCulture) : null }
            });

            return ToDataTable(GetRows(apiUrl, empresaCod, "/api/vendas" + query), "id_venda", "data_venda", "valor_nota", "usuario");
        }

        public static DataTable CarregarEstoqueAtual(string apiUrl, int empresaCod)
        {
            return ToDataTable(GetRows(apiUrl, empresaCod, "/api/estoque/atual"), "id_prod", "cod_prod", "desc_prod", "qunt_est");
        }

        public static DataTable CarregarResumoCompraProdutos(string apiUrl, int empresaCod, DateTime inicio, DateTime fim)
        {
            return ToDataTable(GetRows(apiUrl, empresaCod, "/api/relatorios/compras/produtos?inicio=" + ApiDate(inicio) + "&fim=" + ApiDate(fim)));
        }

        public static DataTable CarregarRelatorioCompraItens(string apiUrl, int empresaCod, int idCompra)
        {
            return ToDataTable(GetRows(apiUrl, empresaCod, "/api/relatorios/compras/" + idCompra + "/itens"));
        }

        public static DataTable CarregarRelatorioCompraCabecalho(string apiUrl, int empresaCod, int idCompra)
        {
            return ToDataTable(GetRows(apiUrl, empresaCod, "/api/relatorios/compras/" + idCompra + "/cabecalho"));
        }

        public static DataTable CarregarRelatorioCompraCliente(string apiUrl, int empresaCod, int idCompra)
        {
            return ToDataTable(GetRows(apiUrl, empresaCod, "/api/relatorios/compras/" + idCompra + "/cliente"));
        }

        public static DataTable CalcularResumoCompraCaixa(string apiUrl, int empresaCod, DateTime inicio, DateTime fim)
        {
            return ToDataTable(GetRows(apiUrl, empresaCod, "/api/relatorios/compras/caixa?inicio=" + ApiDate(inicio) + "&fim=" + ApiDate(fim)));
        }

        public static DataTable CarregarResumoVendaProdutos(string apiUrl, int empresaCod, DateTime inicio, DateTime fim)
        {
            return ToDataTable(GetRows(apiUrl, empresaCod, "/api/relatorios/vendas/produtos?inicio=" + ApiDate(inicio) + "&fim=" + ApiDate(fim)));
        }

        public static decimal CalcularTotalVendaProdutos(string apiUrl, int empresaCod, DateTime inicio, DateTime fim)
        {
            return Decimal(GetRow(apiUrl, empresaCod, "/api/relatorios/vendas/total?inicio=" + ApiDate(inicio) + "&fim=" + ApiDate(fim)), "total");
        }

        public static DataTable CarregarLucroDetalhado(string apiUrl, int empresaCod, DateTime inicio, DateTime fim)
        {
            return ToDataTable(GetRows(apiUrl, empresaCod, "/api/relatorios/lucro/detalhado?inicio=" + ApiDate(inicio) + "&fim=" + ApiDate(fim)));
        }

        public static DataTable CarregarLucroTotal(string apiUrl, int empresaCod, DateTime inicio, DateTime fim)
        {
            return ToDataTable(GetRows(apiUrl, empresaCod, "/api/relatorios/lucro/total?inicio=" + ApiDate(inicio) + "&fim=" + ApiDate(fim)));
        }

        public static DataTable CarregarFluxoCaixa(string apiUrl, int empresaCod, DateTime inicio, DateTime fim)
        {
            return ToFluxoCaixaTable(GetRows(apiUrl, empresaCod, "/api/relatorios/fluxo-caixa?inicio=" + ApiDate(inicio) + "&fim=" + ApiDate(fim)));
        }

        public static decimal CalcularSaldoInicialFluxoCaixa(string apiUrl, int empresaCod, DateTime inicio)
        {
            return Decimal(GetRow(apiUrl, empresaCod, "/api/relatorios/fluxo-caixa/saldo-inicial?inicio=" + ApiDate(inicio)), "saldo");
        }

        public static DataTable CarregarEstoquePeriodo(string apiUrl, int empresaCod, DateTime inicio, DateTime fim, string codigoProduto)
        {
            var query = BuildQuery(new Dictionary<string, string>
            {
                { "inicio", ApiDate(inicio) },
                { "fim", ApiDate(fim) },
                { "cod_prod", codigoProduto }
            });

            return ToDataTable(GetRows(apiUrl, empresaCod, "/api/estoque/periodo" + query));
        }

        public static DataTable CarregarProdutosDataTable(string apiUrl, int empresaCod, bool incluirExcluidos = true)
        {
            var path = "/api/produtos";
            if (incluirExcluidos)
            {
                path += "?incluirExcluidos=true";
            }

            return ToDataTable(GetRows(apiUrl, empresaCod, path), "id_prod", "cod_prod", "desc_prod", "val_prod", "usuario");
        }

        public static tb_venda CriarVenda(string apiUrl, int empresaCod, DateTime dataVenda, int usuario, decimal valorNota)
        {
            return MapVenda(PostRow(apiUrl, empresaCod, "/api/vendas", new Dictionary<string, object>
            {
                { "data_venda", ApiDate(dataVenda) },
                { "usuario", usuario },
                { "valor_nota", valorNota }
            }));
        }

        public static void AtualizarVenda(string apiUrl, int empresaCod, int idVenda, decimal valorNota, int usuario)
        {
            PutRow(apiUrl, empresaCod, "/api/vendas/" + idVenda, new Dictionary<string, object>
            {
                { "valor_nota", valorNota },
                { "usuario", usuario }
            });
        }

        public static void InserirItemVenda(string apiUrl, int empresaCod, string codigoProduto, int idVenda, decimal quantItem, decimal subTotItem, decimal valrItem)
        {
            PostRow(apiUrl, empresaCod, "/api/vendas/" + idVenda + "/itens", new Dictionary<string, object>
            {
                { "cod_prod", codigoProduto },
                { "quant_item", quantItem },
                { "subtot_item", subTotItem },
                { "valr_item", valrItem }
            });
        }

        public static List<tb_itemv> ListarItensVenda(string apiUrl, int empresaCod, int idVenda)
        {
            return GetRows(apiUrl, empresaCod, "/api/vendas/" + idVenda + "/itens").Select(MapItemVenda).ToList();
        }

        public static void ExcluirItemVenda(string apiUrl, int empresaCod, int idItem)
        {
            DeleteRow(apiUrl, empresaCod, "/api/vendas/itens/" + idItem);
        }

        public static void ExcluirVenda(string apiUrl, int empresaCod, int idVenda)
        {
            DeleteRow(apiUrl, empresaCod, "/api/vendas/" + idVenda);
        }

        public static DataTable CarregarDadosRelatorioVenda(string apiUrl, int empresaCod, int idVenda)
        {
            return ToDataTable(GetRows(apiUrl, empresaCod, "/api/relatorios/vendas/" + idVenda));
        }

        private static Dictionary<string, object> GetRow(string apiUrl, int empresaCod, string path)
        {
            return GetObject(apiUrl, path, true, empresaCod) as Dictionary<string, object>;
        }

        private static List<Dictionary<string, object>> GetRows(string apiUrl, int empresaCod, string path)
        {
            var value = GetObject(apiUrl, path, true, empresaCod);
            var rows = value as object[];
            if (rows == null)
            {
                return new List<Dictionary<string, object>>();
            }

            return rows.OfType<Dictionary<string, object>>().ToList();
        }

        private static Dictionary<string, object> PostRow(string apiUrl, int empresaCod, string path, Dictionary<string, object> body)
        {
            return SendWithBody(HttpMethod.Post, apiUrl, empresaCod, path, body);
        }

        private static Dictionary<string, object> PostPublicRow(string apiUrl, string path, Dictionary<string, object> body)
        {
            return SendWithBody(HttpMethod.Post, apiUrl, 0, path, body, false);
        }

        private static Dictionary<string, object> PutRow(string apiUrl, int empresaCod, string path, Dictionary<string, object> body)
        {
            return SendWithBody(HttpMethod.Put, apiUrl, empresaCod, path, body);
        }

        private static Dictionary<string, object> PatchRow(string apiUrl, int empresaCod, string path, Dictionary<string, object> body)
        {
            return SendWithBody(new HttpMethod("PATCH"), apiUrl, empresaCod, path, body);
        }

        private static Dictionary<string, object> DeleteRow(string apiUrl, int empresaCod, string path)
        {
            using (var request = BuildRequest(HttpMethod.Delete, apiUrl, path, true, empresaCod))
            using (var response = Http.SendAsync(request).GetAwaiter().GetResult())
            {
                var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                if (!response.IsSuccessStatusCode)
                {
                    throw BuildApiException(response, json);
                }

                return string.IsNullOrWhiteSpace(json)
                    ? null
                    : Serializer.DeserializeObject(json) as Dictionary<string, object>;
            }
        }

        private static Dictionary<string, object> SendWithBody(HttpMethod method, string apiUrl, int empresaCod, string path, Dictionary<string, object> body)
        {
            return SendWithBody(method, apiUrl, empresaCod, path, body, true);
        }

        private static Dictionary<string, object> SendWithBody(HttpMethod method, string apiUrl, int empresaCod, string path, Dictionary<string, object> body, bool includeEmpresa)
        {
            using (var request = BuildRequest(method, apiUrl, path, includeEmpresa, empresaCod))
            {
                request.Content = new StringContent(Serializer.Serialize(body), Encoding.UTF8, "application/json");

                using (var response = Http.SendAsync(request).GetAwaiter().GetResult())
                {
                    var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    if (!response.IsSuccessStatusCode)
                    {
                        throw BuildApiException(response, json);
                    }

                    return string.IsNullOrWhiteSpace(json)
                        ? null
                        : Serializer.DeserializeObject(json) as Dictionary<string, object>;
                }
            }
        }

        private static object GetObject(string apiUrl, string path, bool includeEmpresa, int empresaCod)
        {
            using (var request = BuildRequest(HttpMethod.Get, apiUrl, path, includeEmpresa, empresaCod))
            using (var response = Http.SendAsync(request).GetAwaiter().GetResult())
            {
                var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                if (!response.IsSuccessStatusCode)
                {
                    throw BuildApiException(response, json);
                }

                return string.IsNullOrWhiteSpace(json) ? null : Serializer.DeserializeObject(json);
            }
        }

        private static HttpRequestMessage BuildRequest(HttpMethod method, string apiUrl, string path, bool includeEmpresa, int empresaCod)
        {
            var request = new HttpRequestMessage(method, NormalizeBaseUrl(apiUrl) + path);
            if (includeEmpresa)
            {
                if (empresaCod <= 0)
                {
                    throw new InvalidOperationException("Empresa nao definida. Realize o login antes de acessar dados da API.");
                }

                request.Headers.Add("x-empresa-cod", empresaCod.ToString(CultureInfo.InvariantCulture));
            }

            return request;
        }

        private static string NormalizeBaseUrl(string apiUrl)
        {
            var url = string.IsNullOrWhiteSpace(apiUrl) ? "http://localhost:3000" : apiUrl.Trim();
            if (!url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) &&
                !url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                url = "http://localhost:3000";
            }

            if (url.EndsWith("/api", StringComparison.OrdinalIgnoreCase))
            {
                url = url.Substring(0, url.Length - 4);
            }

            return url.TrimEnd('/');
        }

        private static string BuildQuery(Dictionary<string, string> values)
        {
            var parts = values
                .Where(x => !string.IsNullOrWhiteSpace(x.Value))
                .Select(x => Escape(x.Key) + "=" + Escape(x.Value))
                .ToList();

            return parts.Count == 0 ? string.Empty : "?" + string.Join("&", parts);
        }

        private static string Escape(string value)
        {
            return Uri.EscapeDataString(value ?? string.Empty);
        }

        private static string ApiDate(DateTime value)
        {
            return value.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
        }

        private static InvalidOperationException BuildApiException(HttpResponseMessage response, string json)
        {
            string apiMessage = TryExtractApiMessage(json);
            if (!string.IsNullOrWhiteSpace(apiMessage))
            {
                return new InvalidOperationException(apiMessage);
            }

            return new InvalidOperationException("API retornou erro " + (int)response.StatusCode + ": " + json);
        }

        private static string TryExtractApiMessage(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                return string.Empty;
            }

            try
            {
                var value = Serializer.DeserializeObject(json) as Dictionary<string, object>;
                return value != null && value.ContainsKey("message")
                    ? Convert.ToString(value["message"], CultureInfo.CurrentCulture)
                    : string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        private static DataTable ToDataTable(IEnumerable<Dictionary<string, object>> rows, params string[] columnOrder)
        {
            var list = rows.ToList();
            var table = new DataTable();
            var keys = new List<string>();

            if (columnOrder != null)
            {
                keys.AddRange(columnOrder.Where(x => !string.IsNullOrWhiteSpace(x)));
            }

            foreach (var row in list)
            {
                foreach (var key in row.Keys)
                {
                    if (!keys.Contains(key))
                    {
                        keys.Add(key);
                    }
                }
            }

            foreach (var key in keys)
            {
                table.Columns.Add(key, typeof(object));
            }

            foreach (var row in list)
            {
                var dataRow = table.NewRow();
                foreach (var key in keys)
                {
                    dataRow[key] = row.ContainsKey(key) && row[key] != null ? row[key] : DBNull.Value;
                }

                table.Rows.Add(dataRow);
            }

            return table;
        }

        private static DataTable ToFluxoCaixaTable(IEnumerable<Dictionary<string, object>> rows)
        {
            var table = new DataTable();
            table.Columns.Add("Data", typeof(string));
            table.Columns.Add("Inicio", typeof(decimal));
            table.Columns.Add("Entrada", typeof(decimal));
            table.Columns.Add("Saida", typeof(decimal));
            table.Columns.Add("Saldo", typeof(decimal));

            foreach (var row in rows)
            {
                var dataRow = table.NewRow();
                dataRow["Data"] = DateOnlyText(row, "Data");
                dataRow["Inicio"] = 0m;
                dataRow["Entrada"] = Decimal(row, "Entrada");
                dataRow["Saida"] = Decimal(row, "Saida");
                dataRow["Saldo"] = 0m;
                table.Rows.Add(dataRow);
            }

            return table;
        }

        private static tb_tipoUsuario MapTipoUsuario(Dictionary<string, object> row)
        {
            return new tb_tipoUsuario
            {
                id_tipoUsuario = Int(row, "id_tipoUsuario", "id_tipousuario", "permi_usuario"),
                desc_tipoUsuario = String(row, "desc_tipoUsuario", "desc_tipousuario")
            };
        }

        private static tb_usuario MapUsuario(Dictionary<string, object> row)
        {
            var tipo = MapTipoUsuario(row);
            return new tb_usuario
            {
                id_usuario = Int(row, "id_usuario"),
                empresa_cod = Int(row, "empresa_cod", "empresaCod"),
                nome_usuario = String(row, "nome_usuario"),
                senha_usuario = String(row, "senha_usuario"),
                permi_usuario = Int(row, "permi_usuario"),
                ativo = Bool(row, "ativo"),
                tb_tipoUsuario = tipo
            };
        }

        private static tb_produtos MapProduto(Dictionary<string, object> row)
        {
            return new tb_produtos
            {
                id_prod = Int(row, "id_prod"),
                empresa_cod = Int(row, "empresa_cod", "empresaCod"),
                cod_prod = String(row, "cod_prod"),
                desc_prod = String(row, "desc_prod"),
                val_prod = NullableDecimal(row, "val_prod"),
                usuario = NullableInt(row, "usuario"),
                excluido = Bool(row, "excluido")
            };
        }

        private static tb_cliente MapCliente(Dictionary<string, object> row)
        {
            return new tb_cliente
            {
                id_cliente = Int(row, "id_cliente"),
                cpf_cliente = String(row, "cpf_cliente"),
                nome_cliente = String(row, "nome_cliente"),
                tel_cliente = String(row, "tel_cliente"),
                Saldo = Decimal(row, "Saldo", "saldo")
            };
        }

        private static tb_compra MapCompra(Dictionary<string, object> row)
        {
            return new tb_compra
            {
                id_compra = Int(row, "id_compra"),
                data_compra = Date(row, "data_compra"),
                desconto_compra = Decimal(row, "desconto_compra"),
                subtot_compra = Decimal(row, "subtot_compra"),
                valor_nota = Decimal(row, "valor_nota"),
                id_cliente = NullableInt(row, "id_cliente"),
                usuario = Int(row, "usuario")
            };
        }

        private static tb_venda MapVenda(Dictionary<string, object> row)
        {
            return new tb_venda
            {
                id_venda = Int(row, "id_venda"),
                data_venda = Date(row, "data_venda"),
                valor_nota = Decimal(row, "valor_nota"),
                usuario = NullableInt(row, "usuario")
            };
        }

        private static tb_itemc MapItemCompra(Dictionary<string, object> row)
        {
            var produto = MapProduto(row);
            var item = new tb_itemc
            {
                id_item = Int(row, "id_item"),
                id_prod = Int(row, "id_prod"),
                cod_prod = String(row, "cod_prod"),
                id_compra = Int(row, "id_compra"),
                quant_item = Decimal(row, "quant_item"),
                subTot_item = Decimal(row, "subTot_item", "subtot_item"),
                valor_item = Decimal(row, "valor_item"),
                tb_produtos = produto
            };
            return item;
        }

        private static tb_itemv MapItemVenda(Dictionary<string, object> row)
        {
            var produto = MapProduto(row);
            var item = new tb_itemv
            {
                id_item = Int(row, "id_item"),
                id_prod = NullableInt(row, "id_prod"),
                cod_prod = String(row, "cod_prod"),
                id_venda = NullableInt(row, "id_venda"),
                quant_item = NullableDecimal(row, "quant_item"),
                subTot_item = NullableDecimal(row, "subTot_item", "subtot_item"),
                valr_item = NullableDecimal(row, "valr_item"),
                tb_produtos = produto
            };
            return item;
        }

        private static tb_impressora MapImpressora(Dictionary<string, object> row)
        {
            return new tb_impressora
            {
                id_impressora = Int(row, "id_impressora"),
                nome_impressora = String(row, "nome_impressora")
            };
        }

        private static object Value(Dictionary<string, object> row, params string[] names)
        {
            if (row == null)
            {
                return null;
            }

            foreach (var name in names)
            {
                if (row.ContainsKey(name))
                {
                    return row[name];
                }
            }

            return null;
        }

        private static string String(Dictionary<string, object> row, params string[] names)
        {
            var value = Value(row, names);
            return value == null ? string.Empty : Convert.ToString(value, CultureInfo.CurrentCulture);
        }

        private static string DateOnlyText(Dictionary<string, object> row, params string[] names)
        {
            var value = Value(row, names);
            if (value == null)
            {
                return string.Empty;
            }

            if (value is DateTime)
            {
                return ((DateTime)value).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            }

            var text = Convert.ToString(value, CultureInfo.InvariantCulture);
            DateTime parsed;
            if (text.Length >= 10
                && DateTime.TryParseExact(text.Substring(0, 10), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsed))
            {
                return parsed.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            }

            return DateTime.TryParse(text, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out parsed)
                || DateTime.TryParse(text, CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal, out parsed)
                ? parsed.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                : text;
        }

        private static int Int(Dictionary<string, object> row, params string[] names)
        {
            var value = Value(row, names);
            return value == null ? 0 : Convert.ToInt32(value, CultureInfo.InvariantCulture);
        }

        private static int? NullableInt(Dictionary<string, object> row, params string[] names)
        {
            var value = Value(row, names);
            return value == null ? (int?)null : Convert.ToInt32(value, CultureInfo.InvariantCulture);
        }

        private static decimal Decimal(Dictionary<string, object> row, params string[] names)
        {
            var value = Value(row, names);
            return value == null ? 0m : Convert.ToDecimal(value, CultureInfo.InvariantCulture);
        }

        private static decimal? NullableDecimal(Dictionary<string, object> row, params string[] names)
        {
            var value = Value(row, names);
            return value == null ? (decimal?)null : Convert.ToDecimal(value, CultureInfo.InvariantCulture);
        }

        private static bool Bool(Dictionary<string, object> row, params string[] names)
        {
            var value = Value(row, names);
            if (value == null)
            {
                return false;
            }

            if (value is bool)
            {
                return (bool)value;
            }

            return string.Equals(Convert.ToString(value, CultureInfo.InvariantCulture), "true", StringComparison.OrdinalIgnoreCase)
                || string.Equals(Convert.ToString(value, CultureInfo.InvariantCulture), "1", StringComparison.OrdinalIgnoreCase);
        }

        private static DateTime Date(Dictionary<string, object> row, params string[] names)
        {
            var value = Value(row, names);
            if (value == null)
            {
                return DateTime.MinValue;
            }

            if (value is DateTime)
            {
                return (DateTime)value;
            }

            DateTime parsed;
            return DateTime.TryParse(Convert.ToString(value, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out parsed)
                ? parsed
                : DateTime.MinValue;
        }
    }
}
