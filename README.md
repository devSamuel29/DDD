# Introdução
Projeto voltado para estudo de arquitetura limpa, Domain Driven Design (DDD), utilizando o framework ASP.NET Core e ORM EntityFrameworkCore para o desenvolvimento. Projeto criado para simular o funcionamento de um E-Commerce.

## Autenticação
### Login:
***VIA POST***
```csharp
email: string(60),
password: string(8-16),
```

### Registro:
***VIA POST***
```csharp
firstname: string(3-20),
lastname: string(3-20),
cpf: string(11)
phone: string(11),
email: string(60),
password: string(8-16),
```
obs.: Não há problema em enivar o cpf/telefone com pontuações! Exemplo: 000.000.000-00 ou (00) 0 00000-0000, O banco sempre guardará sem nenhuma pontuação. Em específico o campo CPF deve estar matematicamente correto, exemplo: https://www.youtube.com/watch?v=xKmDr5YHt7A 
