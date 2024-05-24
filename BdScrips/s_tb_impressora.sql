Create proc s_tb_impressora
(
	@id_impressora INT = null
)
AS

select id_impressora, nome_impressora
from tb_impressora
where @id_impressora is null or id_impressora = @id_impressora 