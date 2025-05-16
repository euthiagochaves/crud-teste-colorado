# crud-teste-colorado

Este projeto é uma aplicação web desenvolvida com ASP.NET Core que oferece funcionalidades de CRUD (Criar, Ler, Atualizar e Deletar) para gerenciamento de clientes. Ele combina uma interface MVC tradicional para interação do usuário e uma API RESTful para operações programáticas. O Entity Framework Core é utilizado como ORM (Object-Relational Mapping) com o banco de dados SQLite, permitindo uma abordagem Code-First para o gerenciamento do esquema do banco de dados.

🚀 Funcionalidades
Interface MVC: Permite aos usuários visualizar, criar, editar e deletar clientes por meio de páginas web.

API RESTful: Endpoints para operações CRUD sobre os clientes, facilitando a integração com outras aplicações.

Gerenciamento de Telefones: Cada cliente pode ter múltiplos números de telefone associados, com tipos e operadoras.

Swagger UI: Interface interativa para testar e documentar a API.

Migrações com EF Core: Gerenciamento do esquema do banco de dados por meio de migrações.

🛠️ Tecnologias e Bibliotecas Utilizadas
.NET 6.0

ASP.NET Core MVC

Entity Framework Core

SQLite

Swashbuckle.AspNetCore (Swagger)

📦 Dependências NuGet
Certifique-se de que os seguintes pacotes NuGet estão instalados no projeto:

Microsoft.EntityFrameworkCore

Microsoft.EntityFrameworkCore.Sqlite

Microsoft.EntityFrameworkCore.Design

Swashbuckle.AspNetCore

⚙️ Configuração do Banco de Dados
O projeto utiliza o Entity Framework Core com SQLite. Para configurar e aplicar as migrações, siga os passos abaixo:

1. Instalar as Ferramentas do EF Core
Se ainda não tiver as ferramentas do EF Core instaladas globalmente, execute:
dotnet tool install --global dotnet-ef

2. Criar a Primeira Migração
No diretório do projeto, execute:
dotnet ef migrations add InitialCreate

Este comando criará uma pasta Migrations com os arquivos necessários para a migração inicial.

3. Aplicar a Migração e Criar o Banco de Dados
Para aplicar a migração e criar o banco de dados SQLite, execute:
dotnet ef database update
Este comando criará o arquivo do banco de dados conforme definido na string de conexão no appsettings.json.

📄 Documentação da API
Após executar o projeto, acesse a documentação interativa da API fornecida pelo Swagger em:

bash
Copiar
Editar
https://localhost:5001/swagger
🧪 Testes
Para testar a aplicação:

Acesse a interface MVC em https://localhost:5001/Clientes.

Utilize o Swagger para testar os endpoints da API.

Verifique o banco de dados SQLite para confirmar as operações realizadas.

📝 Observações
Certifique-se de que a string de conexão no appsettings.json está correta e aponta para o local desejado do arquivo .db do SQLite.

Em ambientes de produção, é recomendável utilizar um banco de dados mais robusto, como SQL Server ou PostgreSQL.

Mantenha as migrações sob controle de versão para facilitar o gerenciamento do esquema do banco de dados.



Evidências de implementação:

![1](https://github.com/user-attachments/assets/3ce8c316-53fb-4873-91fb-27b07265cf94)



![2](https://github.com/user-attachments/assets/abeb7757-24da-41dd-b234-4bb54f42ce7b)


![3](https://github.com/user-attachments/assets/c73b44fe-1e08-4218-984d-97834729566e)


![4](https://github.com/user-attachments/assets/7a6c6d24-51c7-4dd2-b36d-b662d8b5c5c8)


![5](https://github.com/user-attachments/assets/716bfb57-b5cd-469f-92de-430a58dacd3f)


![6](https://github.com/user-attachments/assets/bb7a5aac-6ff6-4493-b283-183e67b4ef40)


![7](https://github.com/user-attachments/assets/c7108f90-8304-4229-8e7c-517367cdae5b)


![8](https://github.com/user-attachments/assets/a28a2e40-b0d3-42bf-bc23-b8bacb0d0be1)


![9](https://github.com/user-attachments/assets/abdf6279-d6bb-44e6-a7af-a2b8a268ebad)









