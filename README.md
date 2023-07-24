# Introdução
Projeto voltado para estudo de arquitetura limpa, Domain Driven Design (DDD), utilizando o framework ASP.NET Core e ORM EntityFrameworkCore para o desenvolvimento. Projeto criado para simular o funcionamento de um E-Commerce.

## Autenticação
### Login:
***VIA POST***
```csharp
email: string(60),
password: string(8-16)
```

- **Possíveis respostas** <br/>

  | Código   |      Mensagem      |
  |----------|-------------:|
  | 200 |  left-aligned |
  | 404 |    centered   |  
  | 500 | right-aligned | 

### Registro:
***VIA POST***
```csharp
firstname: string(3-20),
lastname: string(3-20),
cpf: string(11)
phone: string(11),
email: string(60),
password: string(8-16)
```
- **Possíveis respostas** <br/>

  | Código   |      Mensagem      |
  |----------|-------------:|
  | 200 |  left-aligned |
  | 404 |    centered   |  
  | 500 | right-aligned | 
