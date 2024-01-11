## Iniciar projeto:

Inserir credenciais do banco de dados no arquivo appsettings.json

posteriormente, na raiz do projeto, executar o comando:

```bash
dotnet ef database update -p .\LeitorNfe\LeitorNfe.csproj
```

### Iniciar banco
Caso necessário, executar container do banco de dados:

```bash
docker-compose up -d
```

## Itens não concluídos:
- Edição de uma nota fiscal, inserindo outro arquivo, não foi finalizada;
- Projeto de testes unitários não iniciado;
- Finalizar conifguração configuração das tabelas via Entity Framework;