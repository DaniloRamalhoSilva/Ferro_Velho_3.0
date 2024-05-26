CREATE PROC s_tb_caixa_saldo
AS

DECLARE @saldo DECIMAL, @totalEntrada DECIMAL, @totalSaida DECIMAL, @desconto DECIMAL, @credito DECIMAL;

select @totalEntrada = Sum(subTot_item) from tb_itemc
select @totalSaida = Sum(valor_caixa) from tb_caixa
select @desconto = Sum(desconto_compra),
       @credito = Sum(subtot_compra - desconto_compra - valor_nota)
from tb_compra


SELECT (@totalEntrada - @totalSaida + @desconto + @credito) AS saldo;
