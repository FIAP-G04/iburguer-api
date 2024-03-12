<!-- Permite  a funcionalidade de voltar ao topo -->
<a id="topo"></a>

<!-- Titulo do projeto -->
<div align="center" style="margin-bottom:10px">
    <img src=".github/images/logo.png" alt="logo" />
</div>

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

<div align="left">
  <h4>O Iburguer é um sistema projetado para gerenciar os pedidos da lanchonete fictícia Byte Burguer, como parte do desafio tecnológico de Arquitetura de Software do curso de Pós-Graduação em Tecnologia da FIAP, Turma 4SOAT. Construído por <a href="https://github.com/CarlosEduAC">Carlos Cardoso</a>, <a href="https://github.com/LuanPCunha">Luan Cunha</a>, <a href="https://github.com/matheusantonio">Matheus Cardoso</a> e <a href="https://github.com/vinisaeta">Vinicius Saeta</a>.
  </h4>
</div>

# Índice

- [🆘 O Problema](#Problema)
- [📝 Sobre o Sistema IBurguer](#Sobre)
- [💡 Iburger-api](#API)
- [💻 Arquitetura](#Arquitetura)
- [🧮 Funcionalidades](#Funcionalidades)
- [➡️ Requisitos](#Requisitos)
- [🔎 Testes](#Teste)
- [💻 Vídeo](#Videos)
- [📕 Licença](#Licenca)

<a id="Problema"></a>
# 🆘 O Problema 

A lanchonete Byte Burguer está experimentando um grande sucesso e está buscando expandir seu negócio. No entanto, sem um sistema eficaz de controle de pedidos, o atendimento aos clientes pode se tornar caótico e desorganizado. Por exemplo, considere um cenário em que um cliente faz um pedido personalizado, como um hambúrguer com ingredientes específicos, acompanhado de batatas fritas e uma bebida. O pedido pode ser registrado em um papel e enviado à cozinha, mas não há garantia de que será preparado corretamente.

A ausência de um sistema de controle de pedidos pode resultar em falta de comunicação entre os atendentes e a cozinha, ocasionando atrasos na preparação e entrega dos pedidos. Isso pode levar a erros na execução dos pedidos, como perdas, interpretações equivocadas ou esquecimentos, o que resulta na insatisfação dos clientes e na perda de negócios.

Em suma, um sistema de controle de pedidos é essencial para garantir que a lanchonete possa atender seus clientes de maneira eficiente, gerenciando seus pedidos e estoques de forma adequada. Sem essa ferramenta, a expansão da lanchonete pode não ser bem-sucedida, prejudicando a satisfação do cliente e impactando negativamente nos negócios.

<a id="Sobre"></a>
# 📝 Sobre o Sistema IBurguer

A lanchonete planeja introduzir um sistema de autoatendimento de fast food, composto por uma variedade de dispositivos e interfaces, incluindo um totem e um aplicativo intuitivo. Isso permitirá aos clientes fazerem pedidos sem a necessidade de interação com um atendente, personalizando suas escolhas entre várias opções de lanches, acompanhamentos, bebidas e sobremesas.

O objetivo é viabilizar à lanchonete Byte Burguer uma solução operacional completa, com funcionalidades como gerenciamento de pedidos, sistemas de autoatendimento, cadastro e identificação de clientes, além de um cardápio digital. Essas características visam aprimorar a eficiência operacional, oferecer uma experiência melhor para os clientes e criar oportunidades de crescimento para o negócio.


<a id="API"></a>
# 💡 Iburger-api

Este repositório é dedicado ao projeto Iburguer-Api. A API tem como responsabilidade receber e enviar requisições REST, obedecendo as regras de negócio estabelecidas pelo especialista da lanchonete Byte Burguer.
<details>
<summary>🎮 Tecnologias</summary>

---

Esse projeto foi feito utilizando as seguintes tecnologias:

- [C#](https://learn.microsoft.com/pt-br/dotnet/csharp/)
- [.NET 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/)
- [Docker](https://www.docker.com/)
- [Kubernetes](https://kubernetes.io/pt-br/)
- [Helm](https://helm.sh/)
- [Minikube](https://minikube.sigs.k8s.io/docs/)
- [Swagger](https://swagger.io/)
- [K6](https://k6.io/)</details>

<details>
<summary>📦 Build & Tests (TODO)</summary>

---

| CI/CD | Status |
| --- | --- | 
| Build & Unit Tests | [![.NET Build and Test](https://github.com/g12-4soat/tech-lanches/actions/workflows/build-tests.yml/badge.svg)](https://github.com/g12-4soat/tech-lanches/actions/workflows/build-tests.yml)
| TechLanches API | [![Build API Docker Image](https://github.com/g12-4soat/tech-lanches/actions/workflows/dockerfile-api-build-ci.yml/badge.svg)](https://github.com/g12-4soat/tech-lanches/actions/workflows/dockerfile-api-build-ci.yml)
| TechLanches Worker FilaPedidos | [![Build Worker Docker Image](https://github.com/g12-4soat/tech-lanches/actions/workflows/dockerfile-worker-build-ci.yml/badge.svg)](https://github.com/g12-4soat/tech-lanches/actions/workflows/dockerfile-worker-build-ci.yml)
| Docker Compose | [![Build Docker Compose](https://github.com/g12-4soat/tech-lanches/actions/workflows/docker-compose-build-ci.yml/badge.svg)](https://github.com/g12-4soat/tech-lanches/actions/workflows/docker-compose-build-ci.yml)
| API Docker Publish Develop | [![API Docker Publish Develop](https://github.com/g12-4soat/tech-lanches/actions/workflows/docker-publish-api-develop.yml/badge.svg)](https://github.com/g12-4soat/tech-lanches/actions/workflows/docker-publish-api-develop.yml) | 
| Worker Docker Publish Develop | [![Worker Docker Publish Develop](https://github.com/g12-4soat/tech-lanches/actions/workflows/docker-publish-worker-develop.yml/badge.svg)](https://github.com/g12-4soat/tech-lanches/actions/workflows/docker-publish-worker-develop.yml) | 
| API Docker Publish | [![API Docker Publish](https://github.com/g12-4soat/tech-lanches/actions/workflows/docker-publish-api.yml/badge.svg)](https://github.com/g12-4soat/tech-lanches/actions/workflows/docker-publish-api.yml) | 
| Worker Docker Publish | [![Worker Docker Publish](https://github.com/g12-4soat/tech-lanches/actions/workflows/docker-publish-worker.yml/badge.svg)](https://github.com/g12-4soat/tech-lanches/actions/workflows/docker-publish-worker.yml) | 

</details>

<a id="Arquitetura"></a>
# 💻 Arquitetura
- [Arquitetura](docs/arquitetura.md)

<a id="Funcionalidades"></a>
# 🧮 Funcionalidades
- [Funcionalidades](docs/funcionalidades.md)

<a id="Requisitos"></a>
# ➡️ Requisitos
- [Requisitos](docs/requisitos.md)

<a id="Testes"></a>
# 🔎 Testes
- [Testes](docs/teste.md)

<a id="Videos"></a>
# 💻 Vídeos

### Fase 2
* O detalhamento da **arquitetura** está disponibilizados no seguinte [vídeo](https://www.youtube.com/watch?v=QVkNK2sfK38).

### Fase 3 (TODO)
* O detalhamento da **infraestrutura** está disponibilizados no seguinte [vídeo]().   

<a id="Licenca"></a>
# 📕 Licença

Lançado em 2023

Construído por [Carlos Cardoso](https://github.com/CarlosEduAC), [Luan Cunha](https://github.com/LuanPCunha), [Matheus Cardoso](https://github.com/matheusantonio) e [Vinicius Saeta](https://github.com/vinisaeta) 🚀.
Esse projeto esta sobre [MIT license](./LICENSE).


[De volta ao topo](#topo)

[swaggerlogo]: .github/images/swagger.svg
[menufunc]: .github/images/func_menu.png
[customerfunc]: .github/images/func_customer.png
[shoppingcartfunc]: .github/images/func_shopping_cart.png
[checkoutfunc]: .github/images/func_checkout.png
[orderfunc]: .github/images/func_order.png
[diagramaimplantacaok8s]: .github/images/diagrama-de-implantacao-k8s.png
[visaoinfra1]: .github/images/visao-de-infraestrutura-parte-1.png
[visaoinfra2]: .github/images/visao-de-infraestrutura-parte-2.png
[visaomacro]: .github/images/visao-macro.png
