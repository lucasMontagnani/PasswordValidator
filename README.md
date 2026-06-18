# PasswordValidator
API REST que valida se uma senha atende a um conjunto de regras predefinidas.

## Regras de validação

Uma senha é considerada válida quando:

- Possui 9 ou mais caracteres;
- Contém ao menos 1 dígito;
- Contém ao menos 1 letra minúscula;
- Contém ao menos 1 letra maiúscula;
- Contém ao menos 1 caractere especial do conjunto `!@#$%^&*()-+`;
- Não possui caracteres repetidos;
- Não contém caracteres fora do conjunto permitido (dígitos, letras e os especiais acima), isso inclui rejeitar espaços em branco.

## Como executar

Pré-requisito: .NET SDK 9.0 ou superior.

Estando na pasta raiz da solution `\PasswordValidator>`

```bash
# build de toda a solução
dotnet build

# subir a API
dotnet run --project PasswordValidator.Api
```

A API sobe com Swagger habilitado em `http://localhost:5125/swagger` e/ou `https://localhost:7046/swagger` dependendo do profile selecionado.

### Testando o endpoint
#### Windows (CMD)
```bash
curl -X POST "https://localhost:7046/api/PasswordValidation/password-validation" ^
  -H "Content-Type: application/json" ^
  -d "{ \"password\": \"AbTp9!fok\" }"
```
#### Linux/macOS
```bash
curl -k -X 'POST' \
  'https://localhost:7046/api/PasswordValidation/password-validation' \
  -H 'accept: text/plain' \
  -H 'Content-Type: application/json' \
  -d '{
  "password": "AbTp9!fok"
}'
```

Resposta:

```json
{ "isValid": true }
```

## Como executar os testes

```bash
dotnet test
```

São dois níveis de teste:

- **`PasswordValidator.UnitTests`**: testa cada regra isoladamente e o serviço de validação completo, sem nenhuma dependência de HTTP.
- **`PasswordValidator.IntegrationTests`**: sobe a aplicação inteira em memória (`WebApplicationFactory`) e testa o endpoint via requisições HTTP reais.

Ambos os níveis usam exatamente os exemplos do enunciado como casos de teste. De modo que, qualquer regressão na regra de negócio quebra os testes em ambas as camadas.

## Estrutura do projeto

```
PasswordValidator.Domain/   # regras de negócio puras, sem dependência de framework web
PasswordValidator.Api/      # adaptador HTTP: controllers, DTOs, composição via DI
tests/
├── PasswordValidator.UnitTests/
└── PasswordValidator.IntegrationTests/
```

## Decisões de arquitetura

**Domain isolado da Api.** A camada `Domain` não referencia nada de ASP.NET Core, apenas as regras de dominio da aplicação, neste caso, as regras de validação da senha.

**Cada regra é uma classe, via Specification Pattern.** Em vez de um único método gigante com vários `if`, cada regra de senha (comprimento, dígito, letra minúscula, etc.) é uma classe que implementa `IPasswordRule`. O `PasswordValidationService` recebe uma coleção dessas regras via injeção de dependência e aplica todas, sem conhecer nenhuma implementação concreta. Isso mapeia diretamente para SOLID:

- **SRP** — cada regra tem uma única responsabilidade;
- **OCP** — uma nova regra (ex.: "não pode repetir a senha anterior") é adicionada criando uma nova classe e registrando-a no DI, sem tocar em nenhuma classe existente;
- **LSP** — todas as regras são substituíveis entre si através do mesmo contrato;
- **ISP** — a interface `IPasswordRule` tem um único método, sem métodos não usados pelos implementadores;
- **DIP** — tanto `PasswordValidationService` quanto o controller dependem de abstrações (`IPasswordRule`, `IPasswordValidator`), nunca de classes concretas.

**`POST` em vez de `GET`.** Senha é dado sensível; em uma requisição `GET` ela ficaria exposta na URL (logs de servidor, proxies, histórico do navegador). `POST` com o valor no corpo da requisição evita essa exposição.

**Regras registradas como singleton.** Todas as regras são *stateless* (não guardam dado entre chamadas), então não há motivo para instanciar uma nova por requisição.

## Premissas assumidas

1. **Verificação de caracteres repetidos é case-sensitive** e considera a senha como um todo, não por categoria.
2. **Ausência do campo `password` (nulo) retorna `400 Bad Request`**, enquanto uma `password` vazia (`""`) é tratada como um valor de entrada válido que simplesmente não atende às regras, retornando `200 OK` com `isValid: false`. A ideia é separar erro de contrato (cliente não mandou o campo esperado) de resultado de negócio (a senha enviada não é válida).
3. **O comprimento mínimo é parametrizável** na regra `MinimumLengthRule` (padrão 9), mantendo-a reutilizável fora do contexto da API.
