# 🎲 API de Mitigação de Problemas com Casas de Apostas

[![.NET](https://img.shields.io/badge/.NET-9.0-purple?logo=dotnet)](https://dotnet.microsoft.com/)
[![Oracle](https://img.shields.io/badge/Oracle-DB-red?logo=oracle)](https://www.oracle.com/br/database/)
[![EFCore](https://img.shields.io/badge/Entity%20Framework%20Core-9.x-green?logo=nuget)](https://learn.microsoft.com/ef/core)
[![Swagger](https://img.shields.io/badge/OpenAPI-Swagger-blue?logo=swagger)](https://swagger.io/)
[![Status](https://img.shields.io/badge/status-em%20desenvolvimento-yellow)](#)

Projeto acadêmico desenvolvido em **C# e .NET 9**, que implementa uma **API REST** para gerenciar uma lista de sites de apostas.  
Permite **cadastro, consulta, atualização, exclusão e exportação** dos sites, além de registrar logs em TXT e exportar em JSON.  

---

## 👥 Integrantes
- Eric de Carvalho Rodrigues - RM550249
- Victoria Franceschini Pizza - RM 550609

---

## 🚀 Funcionalidades

- **CRUD completo** de sites de apostas.  
- **Exportação JSON** de todos os registros.  
- **Registro de logs** em TXT (ações e erros).  
- **Persistência em banco Oracle via EF Core**.  
- **Documentação** via OpenAPI (Swagger).  

---

## 📂 Estrutura do Projeto

```bash
D:\csharpSprint3
├── CandidatosApi         → Camada de apresentação (Controllers, Program.cs)
├── CandidatosBusiness    → Regras de negócio (Services, Interfaces)
├── CandidatosData        → Persistência (DbContext, Migrations)
├── CandidatosModel       → Entidades/Models (ApostaSiteModel)
```
---

🗄️ Banco de Dados
O banco utilizado é Oracle e a tabela criada pelo EF Core é:

```sql
CREATE TABLE APOSTA_SITES (
    Id            VARCHAR2(100) PRIMARY KEY,
    Nome          VARCHAR2(255) NOT NULL,
    Url           VARCHAR2(500) NOT NULL UNIQUE,
    Categoria     VARCHAR2(100),
    NivelRisco    VARCHAR2(50),
    DataCadastro  TIMESTAMP
);
```

---

⚙️ Como Rodar o Projeto
1. Restaurar dependências
```powershell
dotnet restore
```

2. Garantir pacotes instalados
```powershell
dotnet add CandidatosData package Oracle.EntityFrameworkCore -v 9.23.90
dotnet add CandidatosData package Microsoft.EntityFrameworkCore.Design -v 9.0.9
```

3. Criar Migration
```powershell
dotnet ef migrations add CriarTabelaApostaSites --project CandidatosData --startup-project CandidatosApi
```

4. Atualizar banco Oracle
```powershell
dotnet ef database update --project CandidatosData --startup-project CandidatosApi
```

5. Rodar a API
```powershell
dotnet run --project CandidatosApi
```

A API ficará disponível em:

```bash
http://localhost:5074/swagger
```

---

📡 Exemplos de Requisições
➕ Criar um site de apostas
```powershell
$body = @{
  nome = "Bet365"
  url = "https://www.bet365.com"
  categoria = "Esportes"
  nivelRisco = "Alto"
} | ConvertTo-Json

Invoke-RestMethod -Method Post `
  -Uri http://localhost:5074/api/aposta-sites `
  -ContentType "application/json" -Body $body
```

📋 Listar todos os sites
```powershell
Invoke-RestMethod -Method Get -Uri http://localhost:5074/api/aposta-sites
```

🔍 Obter por ID
```powershell
Invoke-RestMethod -Method Get -Uri http://localhost:5074/api/aposta-sites/{id}
```

♻️ Atualizar
```powershell
$body = @{
  id = "{id}"
  nome = "Bet365 Atualizado"
  url = "https://www.bet365.com"
  categoria = "Esportes"
  nivelRisco = "Médio"
} | ConvertTo-Json

Invoke-RestMethod -Method Put `
  -Uri http://localhost:5074/api/aposta-sites `
  -ContentType "application/json" -Body $body
```

❌ Remover
```powershell
Invoke-RestMethod -Method Delete -Uri http://localhost:5074/api/aposta-sites/{id}
```

📌 Próximos Passos
- Implementar autenticação JWT.
- Criar filtros por categoria e risco.
- Adicionar testes unitários com xUnit.
- Deploy em nuvem (Azure ou AWS).

📄 Licença
Projeto acadêmico — uso educacional e não comercial.
