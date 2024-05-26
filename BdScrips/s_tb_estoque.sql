ALTER PROC [dbo].[s_tb_estoque] 
(
	@dataInicial DATETIME,
	@dataFinal DATETIME,
	@id_prod INT = null
)
AS

SELECT Estoque.id_prod as Codigo,
       UPPER(TP.desc_prod) as Descrição, 
	   SUM(inicio) as Inicio, 
	   SUM(entrada) as Entrada, 
	   SUM(saida) as Saida, 
	   (SUM(inicio) + SUM(entrada) - SUM(saida)) as Saldo
FROM(
	-- inicial
	select  id_prod, SUM(inicio) as inicio, 0 as entrada, 0 as saida
	from (
		SELECT id_prod, SUM(quant_item) as inicio
		FROM tb_itemc TIC
		INNER JOIN tb_compra TC on TC.id_compra = TIC.id_compra
		WHERE  TC.data_compra < @dataInicial
		GROUP BY id_prod

		UNION ALL

		SELECT id_prod, -sum(quant_item) AS inicio
		FROM tb_itemV TIV
		INNER JOIN tb_venda TV on TV.id_venda = TIV.id_venda 
		WHERE TV.data_venda < @dataInicial
		GROUP BY id_prod

		) as Inicio
	GROUP BY id_prod
	UNION ALL
	-- Entrada
	SELECT id_prod, 0 as Inicio, SUM(quant_item) as entrada , 0 as saida
	FROM tb_itemc TIC
	INNER JOIN tb_compra TC on TC.id_compra = TIC.id_compra
	WHERE TC.data_compra between @dataInicial and @dataFinal 
	GROUP BY id_prod
	UNION ALL
	-- Saida
	SELECT id_prod, 0 as Inicio, 0 as entrada, sum(quant_item) AS saida
	FROM tb_itemV TIV
	INNER JOIN tb_venda TV on TV.id_venda = TIV.id_venda 
	WHERE TV.data_venda between @dataInicial and @dataFinal 
	GROUP BY id_prod
) AS Estoque
INNER JOIN tb_produtos TP ON TP.id_prod = estoque.id_prod
WHERE @id_prod IS NULL OR Estoque.id_prod = @id_prod
GROUP BY TP.desc_prod,  Estoque.id_prod
ORDER BY Estoque.id_prod