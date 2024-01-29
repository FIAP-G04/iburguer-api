<!-- Começando -->

# ➡️ Requisitos

<!-- Pré-requisitos para rodar o projeto -->

Antes de começar, é necessário instalar o Kubernetes e o Minikube. As instruções para instalação podem ser encontradas nas documentações oficiais em [Tecnologias](sobre_o_projeto.md). As instruções dessa documentação utilizarão como referência o Minikube, porém outras ferramentas semelhantes podem ser utilizadas.

Após a instalação das ferramentas, é necessário executar o seguinte comando para inicializar o Minikube:

``` minikube start ``` 

Após a inicialização do Minikube, para configurá-lo como o cluster Kubernetes atual, deve ser executado o seguinte comando:

``` kubectl config use-context minikube ``` 

### Helm

Caso seja necessário gerar novamente os artfatos Kubernetes, a partir da pasta *FIAP* execute:

``` helm template iburguer ./chart > k8s/artifacts.yaml ``` 

### Testes

Para a execução dos testes é necessário instalar o [Visual Studio](https://visualstudio.microsoft.com/pt-br/vs/professional/) e o [.NET 7.0](https://dotnet.microsoft.com/pt-br/download/dotnet/7.0).