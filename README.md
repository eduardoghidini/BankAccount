# Bank Account Control (Warren)

Projeto para realização de operações de conta corrente (depósito, pagamento e retiradas).

## Aplicação
- 1 projeto client side desenvolvido em Angular.
- 1 projeto server side desenvolvido em .Net Core.

### Client Side

Desenvolvido em Angular 10 com e os componentes do Angular Material.

#### Instalação

1.  Rodar a instalação dos pacotes
`> npm install`

2. Rodar a aplicação
`> ng server`

###Server Side
Desenvolvido em .NET Core com acesso a dados através do ORM Entity Framework Core e base de dados MySql.

Consiste em 1 projeto de API web com autenticação através de token JWT. Além disso, possui 2 projetos que rodam Jobs gerenciados pelo HangFire. 

As principais camadas são:
1. API: Jobs gerenciados pelo hangfire e API WEB.
2. Application: Camada de Domínio e de Application.
3. Infrastrucuture: Camada de acesso a dados e abstração do Hangfire para MySql.

**Principais Frameworks utilizados:** EntityFramework Core, Hangfire, MediatR e FluentValidation.

#### Funcionalidade Geral do Sistema

Para utilizar a API deve-se ter um registro na tabela de Usuários e seu vínculo com a tabela de Account.

Ao realizar o login é salvo nas claims o account id vinculado ao token. Com isso, todas as operações de conta são realizadas a partir do account id vinculado ao token.

As operações que o usuário realiza são "OperationsRequest" que são requisições de operação. Ao criar uma requisição, o usuário pode informar a data que deseja que a operação seja realizada. No momento que o usuário registra uma requisição de operação, é adicionada na fila do hangfire uma requisição de realização do job referente a realização da operação em si. Nota-se que podem ser agendados operações para datas futuras.
O job de realização de transação é responsável por identificar qual tipo de operação e realizar o procedimento corretamente.  Caso tudo esteja correto com as validações de cada operação, a operação em si é criada e o saldo atualizado. Existe uma factory que identifica qual tipo de operação e devolve o comando correto. Caso, futuramente, sejam criados novos tipos de operação, seria necessário apenas criar um comandhandler (mediatr) vinculando, na factory, o comando e o tipo.
O terceiro job (recurrencyjob) acontece uma vez ao dia e é responsável por rentabilizar o dinheiro da conta corrente do usuário, através do %¨cid configurado na aplicação.



