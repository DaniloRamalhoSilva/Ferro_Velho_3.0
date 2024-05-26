CREATE PROC s_tb_produtos
(
	@id_prod INT = null
)
AS

select *
from tb_produtos
where @id_prod is null or id_prod = @id_prod 

