using System.ComponentModel;


namespace FerroVelho
{
    public enum ValidacaoEnum
    {
        // Positivas
        [Description("Sucesso!")]
        SUCESSO = 1,

        // Negativa
        [Description("Erro Inesperado!")]
        ERRO_DEFUL = 2,

        [Description("Erro!")]
        ERRO = 3,

        [Description("Usuario ou senha invalido")]
        USUARIO_SENHA_INVALIDO = 4,

        [Description("String de conexão: User inexistente")]
        SEM_CONEXA_BANCO_DADOS = 5,

        [Description("Saldo insuficiente para essa compra! Necessário almentar valor em caixa")]
        SALDO_INSIFICIENTE = 6,
    }
}
