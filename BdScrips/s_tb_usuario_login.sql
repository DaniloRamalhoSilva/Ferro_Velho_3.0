Create proc s_tb_usuario_login
(
	@nome_usuario VARCHAR(250) = NULL,
	@senha_usuario VARCHAR(100) = NULL
)
AS

SELECT id_usuario, nome_usuario, permi_usuario
  FROM tb_usuario
WHERE @nome_usuario = nome_usuariO
  AND @senha_usuario = senha_usuario
