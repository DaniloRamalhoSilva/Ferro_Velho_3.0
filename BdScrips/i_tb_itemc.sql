CREATE PROC i_tb_itemc
(
	@id_prod int,
	@id_compra int, 
	@quant_item decimal(18, 3),
	@subTot_item decimal(18, 3),
	@valor_item decimal(18, 3)
)
AS

INSERT INTO tb_itemc VALUES(@id_prod, @id_compra, @quant_item, @subTot_item, @valor_item)


