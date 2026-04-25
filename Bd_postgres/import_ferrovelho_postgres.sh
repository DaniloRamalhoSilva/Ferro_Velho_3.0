#!/usr/bin/env bash
set -euo pipefail

# Usage:
#   ./scripts/import_ferrovelho_postgres.sh [sql_file]
# Example:
#   ./scripts/import_ferrovelho_postgres.sh bd_ferroVelho.sql

ROOT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
SQL_FILE="${1:-$ROOT_DIR/bd_ferroVelho.sql}"
COMPOSE_ENV_FILE="${COMPOSE_ENV_FILE:-$ROOT_DIR/.env}"

if [[ ! -f "$SQL_FILE" ]]; then
  echo "Arquivo SQL nao encontrado: $SQL_FILE"
  echo "Salve o arquivo bd_ferroVelho.sql e rode novamente."
  exit 1
fi

if [[ ! -f "$COMPOSE_ENV_FILE" ]]; then
  echo "Arquivo .env nao encontrado em: $COMPOSE_ENV_FILE"
  exit 1
fi

echo "Criando banco/usuario (ferrovelho_db / ferrovelho_user)..."
docker compose --env-file "$COMPOSE_ENV_FILE" exec -T db \
  psql -v ON_ERROR_STOP=1 -U postgres <<'SQL'
SELECT
  'CREATE ROLE ferrovelho_user LOGIN PASSWORD ''TroqueEssaSenha@123'''
WHERE NOT EXISTS (
  SELECT 1 FROM pg_roles WHERE rolname = 'ferrovelho_user'
)\gexec

SELECT
  'CREATE DATABASE ferrovelho_db OWNER ferrovelho_user'
WHERE NOT EXISTS (
  SELECT 1 FROM pg_database WHERE datname = 'ferrovelho_db'
)\gexec

GRANT ALL PRIVILEGES ON DATABASE ferrovelho_db TO ferrovelho_user;
SQL

echo "Importando arquivo SQL em ferrovelho_db: $SQL_FILE"
docker compose --env-file "$COMPOSE_ENV_FILE" exec -T db \
  psql -v ON_ERROR_STOP=1 -U postgres -d ferrovelho_db < "$SQL_FILE"

echo "Importacao concluida."
echo "Banco: ferrovelho_db"
echo "Usuario: ferrovelho_user"
echo "Troque a senha padrao do usuario apos a importacao."
