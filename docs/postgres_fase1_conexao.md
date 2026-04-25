# Fase 1 - Conexao com PostgreSQL de producao

Este passo apenas configura a string de conexao no projeto para o banco de producao.
As telas e consultas ainda usam SQL Server/LINQ-to-SQL e serao migradas nas proximas etapas.

## 1) Definir os dados do banco de producao
- Host do banco
- Porta
- Nome do banco
- Usuario
- Senha

## 2) Atualizar `FerroVelho/configura*.xml`
No PowerShell, na raiz do repositorio:

```powershell
.\scripts\set-postgres-config.ps1 `
  -DbHost "SEU_HOST" `
  -Port 5432 `
  -Database "bd_ferrovelho" `
  -Username "SEU_USUARIO" `
  -Password "SUA_SENHA"
```

## 3) Verificar o arquivo gerado
```powershell
Get-ChildItem .\FerroVelho\configura*o.xml
Get-Content (Get-ChildItem .\FerroVelho\configura*o.xml | Select-Object -First 1).FullName
```

## 4) Observacao importante
Se o PostgreSQL estiver publicado apenas como `127.0.0.1:5432` no servidor, o desktop nao conecta direto.
Nesse caso, use um tunel SSH:

```powershell
ssh -L 15432:127.0.0.1:5432 root@SEU_IP_SERVIDOR -N
```

E rode o script com `-DbHost "127.0.0.1" -Port 15432`.
