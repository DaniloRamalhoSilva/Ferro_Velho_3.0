# Ferro Velho 3.0

Aplicativo Windows Forms para operacao de ferro-velho/cooperativa de reciclagem.

Este projeto nao acessa mais banco de dados diretamente. Todas as consultas e gravacoes sao feitas pela API em `..\Ferro_Velho_API`.

## Requisitos

- Windows com .NET Framework 4.8
- Visual Studio 2017 ou mais recente, ou MSBuild compativel
- Projeto `Ferro_Velho_API` configurado e em execucao
- Pacotes NuGet restaurados

## Configuracao

Na tela de configuracao do aplicativo:

- `URL da API`: endereco base da API, por exemplo `http://localhost:3000`

O codigo da empresa nao e configurado manualmente. Ele vem do `empresa_cod`
retornado pelo login e passa a ser enviado no header `x-empresa-cod` em todas
as requisicoes de negocio. Se nao houver usuario autenticado com `empresa_cod`
valido, as chamadas de negocio para a API falham antes de enviar a requisicao.

Se nao houver configuracao local, o aplicativo usa `http://localhost:3000`.

## Uso

Ao iniciar o aplicativo, sera apresentada a tela de login. O menu principal possui modulos de cadastros, consultas, operacoes, relatorios e configuracoes, todos alimentados pela API.
