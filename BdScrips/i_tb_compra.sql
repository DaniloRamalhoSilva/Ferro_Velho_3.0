CREATE PROC i_tb_compra
(
	@valor_nota decimal(18, 2),
	@usuario int,
	@desconto_compra decimal(18, 2) = NULL,
	@subtot_compra decimal(18, 2) = NULL,
	@id_cliente int = NULL
)
AS 

INSERT INTO tb_compra VALUES(GETDATE(), @valor_nota, @usuario, @desconto_compra, @subtot_compra, @id_cliente) 





