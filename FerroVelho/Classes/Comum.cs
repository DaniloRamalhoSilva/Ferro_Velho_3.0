using System;
using System.ComponentModel;
using System.Reflection;


namespace FerroVelho
{
    public class Comum
    {
        #region [ TRATAMENTO PARA GRAVAR DADOS NO SQL ]

        public static string TrataDataSQL(DateTime? data, bool incluirHorario = false)
        {
            return data == null ? null : (incluirHorario ? ((DateTime)data).ToString("yyyy-MM-dd HH:mm:ss") : ((DateTime)data).ToString("yyyy-MM-dd"));
        }

        public static string TrataMoedaSQL(decimal? valor, bool retornarNull = false)
        {
            if (valor == null) return retornarNull ? null : "0";

            return TrataMoedaSQL(valor.ToString(), retornarNull);
        }

        public static string TrataMoedaSQL(string valor, bool retornarNull = false)
        {
            valor = valor.Replace("R$", "");
            valor = valor.Replace(".", "");
            valor = valor.Replace(",", ".");

            if (string.IsNullOrEmpty(valor) && retornarNull) return null;
            return valor.Trim();
        }

        #endregion

        public static decimal ConverteStringDecima(string valorDecimal)
        {
            decimal saldo = decimal.TryParse(valorDecimal, out saldo) ? saldo : 0;
            return saldo;
        }

        public static string ObterDescricaoEnum(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                    typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }
}
