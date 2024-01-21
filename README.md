<!-- Permite  a funcionalidade de voltar ao topo -->
<a name="readme-top"></a>

<!-- Titulo do projeto -->
<div align="center" style="margin-bottom: 16px">
    <img src=".github/images/logo.png" alt="logo" />
</div>

___________________________________________________

<!-- Informações visuais do projeto -->
<div align="center">
    <img alt="Repository size" src="https://img.shields.io/github/repo-size/CarlosEduAC/tech-challenge-fiap?color=009bd9">
    <a href="https://github.com/CarlosEduAC/tech-challenge-fiap/commits/main">
        <img alt="GitHub last commit" src="https://img.shields.io/github/last-commit/CarlosEduAC/tech-challenge-fiap?color=009bd9">
    </a>
    <img alt="License" src="https://img.shields.io/badge/license-MIT-009db9">
    <a href="https://github.com/CarlosEduAC/tech-challenge-fiap/stargazers">
        <img alt="Stargazers" src="https://img.shields.io/github/stars/CarlosEduAC/tech-challenge-fiap?color=009db9&logo=github">
    </a>
    <img alt="Version" src="https://img.shields.io/badge/Version-8.0-3B19E5?logo=dotnet" />
</div>

<!-- Breve descrição sobre o projeto -->

<div align="center">
  <sub>Sistema com o objetivo de gerenciar os pedidos de uma lanchonete. Construído por <a href="https://github.com/CarlosEduAC">Carlos Cardoso</a>, <a href="https://github.com/LuanPCunha">Luan Cunha</a>, <a href="https://github.com/matheusantonio">Matheus Cardoso</a> e <a href="https://github.com/vinisaeta">Vinicius Saeta</a>.
  </sub>
</div>

<!-- Tabela de conteúdo do projeto -->

# 👉 Índice

- [👉 Índice](#-índice)
- [📝 Sobre o Projeto](#-sobre-o-projeto)
  - [🎮 Tecnologias](#-tecnologias)
  - [🧮 Funcionalidades](#-funcionalidades)
    - [Gerenciamento de Menu](#gerenciamento-de-menu)
    - [Gerenciamento de Cliente](#gerenciamento-de-cliente)
    - [Gerenciamento de Carrinho de Compras](#gerenciamento-de-carrinho-de-compras)
    - [Gerenciamento de Pagamento](#gerenciamento-de-pagamento)
    - [Gerenciamento de Pedidos](#gerenciamento-de-pedidos)
- [➡️ Começando](#️-começando)
  - [🚧 Pré-requisitos](#-pré-requisitos)
  - [⚙️ Execução](#️-execução)
    - [📋 API](#-api)
- [💻 Documentação](#-documentação)
  - [ Swagger](#-swagger)
- [🔎 Testes](#-testes)
- [📕 License](#-license)

<!-- Descrição do projeto -->

# 📝 Sobre o Projeto

O projeto visa fornecer para a lanchonete Byte Burguer uma solução operacional de um sistema de automação e gestão. Ele fornece funcionalidades como gerenciamento de pedidos, sistemas de autoatendimento, sistemas de cadastramento e identificação de clientes e cardápio digital com o objetivo de aumentar a eficiência operacional e proporcionar uma experiência aprimorada para os clientes e oportunidades de crescimento para a lanchonete.

<!-- Tecnologias usadas no projeto -->

## 🎮 Tecnologias

Esse projeto foi feito utilizando as seguintes tecnologias:

- [C#](https://learn.microsoft.com/pt-br/dotnet/csharp/)
- [.NET 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/)
- [Docker](https://www.docker.com/)
- [Kubernetes](https://kubernetes.io/pt-br/)
- [Helm](https://helm.sh/)
- [Minikube](https://minikube.sigs.k8s.io/docs/)
- [Swagger](https://swagger.io/)

<!-- Funcionalidades do projeto -->

## 🧮 Funcionalidades

### Gerenciamento de Menu

![menufunc][menufunc]

É disponibilizado um CRUD de gerenciamento de menu onde são inseridos, alterados e removidos os itens do menu de pedidos. Da mesma forma, é disponibilizado um endpoint de busca de produtos por categoria que é utilizado para exibir o catálogo para o cliente.

### Gerenciamento de Cliente

![customerfunc][customerfunc]

No gerenciamento de cliente é possível cadastrar novos clientes e alterar clientes já cadastrados. Também é possível identificar clientes cadastrados através do CPF.

### Gerenciamento de Carrinho de Compras

![shoppingcartfunc][shoppingcartfunc]

O carrinho de compras permite ao cliente adicionar os produtos desejados. Da mesma forma, é possível gerenciar o carrinho removendo um produto, alterando sua quantidade ou limpando o carrinho, removendo todos os produtos presentes.

### Gerenciamento de Pagamento

![checkoutfunc][checkoutfunc]

O gerenciamento de pagamento disponibiliza um único *endpoint* que confirma o pagamento de um carrinho, gerando o pedido.

### Gerenciamento de Pedidos

![orderfunc][orderfunc]

Após a confirmação do pagamento, os pedidos são enviados para a fila que pode ser acessada no gerenciamento de pedidos. Aqui também estão contempladas as operações de alteração do status do pedido, desde o início de sua preparação até sua finalização.

<p align="right">(<a href="#readme-top">Volta ao topo</a>)</p>

<!-- Começando -->

# ➡️ Começando

<!-- Pré-requisitos para rodar o projeto -->

## 🚧 Pré-requisitos

Antes de começar, é necessário instalar o Kubernetes, o Helm e o Minikube. As instruções para instalação podem ser encontradas nas documentações oficiais em [🎮 Tecnologias](#-tecnologias). As instruções dessa documentação utilizarão como referência o Minikube, porém outras ferramentas semelhantes podem ser utilizadas.

Após a instalação das ferramentas, é necessário executar o seguinte comando para inicializar o Minikube:

``` minikube start ``` 

Após a inicialização do Minikube, para configurá-lo como o cluster Kubernetes atual, deve ser executado o seguinte comando:

``` kubectl config use-context minikube ``` 

<!-- Como rodar o projeto -->

## ⚙️ Execução

### 📋 API

A partir da pasta *FIAP*, executar o comando

``` helm template diner ./chart > output.yaml ``` 

Um arquivo output.yaml será gerado com os artefatos Kubernetes. Em seguida, o seguinte comando deve ser executado:

``` kubectl apply -f output.yaml ``` 

Após os recursos serem criados, para acessar um pod da API diner para teste, é necessário executar o comando:

``` kubectl get pods ``` 

Serão exibidos os pods da aplicação, cujo nome será um sufixo *diner-* com um valor aleatório. Copie o nome de um dos pods e substitua no comando abaixo:

``` kubectl port-forward [nome do pod] 5000:80 ```

Em seguida, a API estará disponível no endpoint *http://localhost:5000/*

<!-- Documentação do projeto -->

# 💻 Documentação

## ![swaggerlogo][swaggerlogo] Swagger

Para facilitar na visualização, desenvolvimento e documentação da API, foi utilizado
o [Swagger](https://swagger.io/).

Ele deve ser acessado através [desse link](http://localhost:5000/swagger) quando a aplicação estiver executando.

<p align="right">(<a href="#readme-top">Volta ao topo</a>)</p>

# 🔎 Testes

A execução dos testes pode ser feita através do *Visual Studio* ou executando o seguinte comando a partir da pasta *FIAP*:

``` dotnet test ```

# 📕 License

Lançado em 2023 📕 License

Construído por [Carlos Cardoso](https://github.com/CarlosEduAC), [Luan Cunha](https://github.com/LuanPCunha), [Matheus Cardoso](https://github.com/matheusantonio) e [Vinicius Saeta](https://github.com/vinisaeta) 🚀.
Esse projeto esta sobre [MIT license](./LICENSE).

<p align="right">(<a href="#readme-top">Volta ao topo</a>)</p>

[swaggerlogo]: .github/images/swagger.svg
[menufunc]: .github/images/func_menu.png
[customerfunc]: .github/images/func_customer.png
[shoppingcartfunc]: .github/images/func_shopping_cart.png
[checkoutfunc]: .github/images/func_checkout.png
[orderfunc]: .github/images/func_order.png
