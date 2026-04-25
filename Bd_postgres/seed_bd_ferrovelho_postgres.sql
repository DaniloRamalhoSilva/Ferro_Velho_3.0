-- Seed inicial para PostgreSQL.
-- Execucao:
--   psql -U postgres -f Bd_postgres/seed_bd_ferrovelho_postgres.sql
--
-- Usuario criado em base nova:
--   login: admin
--   senha: admin

\connect bd_ferrovelho

CREATE SCHEMA IF NOT EXISTS dbo;
SET search_path TO dbo, public;

INSERT INTO dbo.tb_tipousuario (id_tipousuario, desc_tipousuario)
VALUES
  (1, 'Administrador'),
  (2, 'Operacional')
ON CONFLICT (id_tipousuario)
DO UPDATE SET desc_tipousuario = EXCLUDED.desc_tipousuario;

SELECT setval(
  pg_get_serial_sequence('dbo.tb_tipousuario', 'id_tipousuario'),
  GREATEST((SELECT COALESCE(MAX(id_tipousuario), 1) FROM dbo.tb_tipousuario), 1),
  TRUE
);

INSERT INTO dbo.tb_usuario (nome_usuario, senha_usuario, permi_usuario, ativo)
SELECT 'admin', 'admin', 1, TRUE
WHERE NOT EXISTS (
  SELECT 1
  FROM dbo.tb_usuario
  WHERE UPPER(nome_usuario) = UPPER('admin')
);

UPDATE dbo.tb_usuario
SET permi_usuario = 1,
    ativo = TRUE,
    senha_usuario = CASE
      WHEN senha_usuario IS NULL OR senha_usuario = '' THEN 'admin'
      ELSE senha_usuario
    END
WHERE UPPER(nome_usuario) = UPPER('admin');

SELECT setval(
  pg_get_serial_sequence('dbo.tb_usuario', 'id_usuario'),
  GREATEST((SELECT COALESCE(MAX(id_usuario), 1) FROM dbo.tb_usuario), 1),
  TRUE
);
