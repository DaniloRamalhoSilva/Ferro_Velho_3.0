CREATE PROC s_tb_estoque 
(
	@dataInicial DATETIME,
	@dataFinal DATETIME
)
AS

SELECT UPPER(TP.desc_prod), SUM(inicio), SUM(Entrada), SUM(Saida), (SUM(inicio) + SUM(Entrada) - SUM(Saida)) as Saldo
FROM(
	-- inicial
	select  id_prod, SUM(inicio) as inicio, null as Entrada, null as Saida
	from (
		SELECT id_prod, SUM(quant_item) as inicio
		FROM tb_itemc TIC
		INNER JOIN tb_compra TC on TC.id_compra = TIC.id_compra
		WHERE  TC.data_compra < @dataInicial
		GROUP BY id_prod

		UNION ALL

		SELECT id_prod, -sum(quant_item) AS Inicio
		FROM tb_itemV TIV
		INNER JOIN tb_venda TV on TV.id_venda = TIV.id_venda 
		WHERE TV.data_venda < @dataInicial
		GROUP BY id_prod

		) as inicio
	GROUP BY id_prod
	UNION ALL
	-- Entrada
	SELECT id_prod, null as Inicio, SUM(quant_item) as Entrada , null as Saida
	FROM tb_itemc TIC
	INNER JOIN tb_compra TC on TC.id_compra = TIC.id_compra
	WHERE TC.data_compra between @dataInicial and @dataFinal 
	GROUP BY id_prod
	UNION ALL
	-- Saida
	SELECT id_prod, null as Inicio, null as Entrada, sum(quant_item) AS Saida
	FROM tb_itemV TIV
	INNER JOIN tb_venda TV on TV.id_venda = TIV.id_venda 
	WHERE TV.data_venda between @dataInicial and @dataFinal 
	GROUP BY id_prod
) AS Estoque
INNER JOIN tb_produtos TP ON TP.id_prod = Estoque.id_prod
GROUP BY TP.desc_prod,  Estoque.id_prod
ORDER BY Estoque.id_prod