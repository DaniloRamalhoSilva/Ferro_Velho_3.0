param(
    [Parameter(Mandatory = $true)]
    [string]$DbHost,

    [Parameter(Mandatory = $false)]
    [int]$Port = 5432,

    [Parameter(Mandatory = $true)]
    [string]$Database,

    [Parameter(Mandatory = $true)]
    [string]$Username,

    [Parameter(Mandatory = $true)]
    [string]$Password,

    [Parameter(Mandatory = $false)]
    [string]$ConfigPath = ""
)

if ([string]::IsNullOrWhiteSpace($ConfigPath)) {
    $candidate = Get-ChildItem -Path "FerroVelho" -File | Where-Object {
        $_.Name -like "configura*o.xml"
    } | Select-Object -First 1

    if ($null -eq $candidate) {
        throw "Nao foi encontrado arquivo de configuracao em FerroVelho (ex: configuracao.xml)."
    }

    $ConfigPath = $candidate.FullName
}

$connectionString = "Host=$DbHost;Port=$Port;Database=$Database;Username=$Username;Password=$Password;Search Path=dbo,public;Pooling=true;Timeout=15;Command Timeout=60;SSL Mode=Disable"

$xml = @"
<configConexao>
  <dataUser>$connectionString</dataUser>
  <dataImp>$connectionString</dataImp>
</configConexao>
"@

Set-Content -Path $ConfigPath -Value $xml -Encoding UTF8

Write-Host "Arquivo atualizado:" $ConfigPath
Write-Host "Conexao configurada para PostgreSQL:"
Write-Host $connectionString
