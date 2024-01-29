<!-- Funcionalidades do projeto -->

# 🧮 Funcionalidades

## Gerenciamento de Menu

O primeiro passo para a execução da API é o preenchimento do menu com itens que podem ser adicionados ao pedido. 

```POST /api/menu/products``` <br>Inclui um item no menu;

```PUT /api/menu/products``` <br>Atualiza um item do menu;

```DELETE /api/menu/products``` <br>Remove um item do menu;

```PATCH /api/menu/products/enabled``` <br>Habilita um item do menu;

```PATCH /api/menu/products/disabled``` <br>Desabilita um item do menu;

```GET /api/menu/products/{category}``` <br>Obtem todos os itens do menu a partir de uma determinada categoria;

![menufunc][menufunc]

## Gerenciamento de Clientes

O próximo passo é o cadastro dos clientes que farão os pedidos na lanchonete. Para esse fim são disponibilizados os seguintes endpoints:

```POST /api/customers``` <br>Cadastra um cliente;

```PUT /api/customers``` <br>Atualiza um cliente já cadastrado;

```GET /api/customers/{cpf}``` <br>Obtem um cliente a partir de seu CPF;

![customerfunc][customerfunc]

## Carrinho de Compras

Havendo um menu preenchido com itens e um cliente cadastrado buscando pelos mesmos, é necessário que um carrinho seja criado para o cliente. Isso pode ser feito através do endpoint:

```POST /api/carts``` <br>Cria carrinho a partir do ID de um cliente já cadastrado;

Em seguida, os itens do carrinho podem ser gerenciados através dos seguintes endpoints:

```POST /api/carts/{shoppingCartId}/item``` <br>Adiciona um item do menu ao carrinho de compras através de seu ID;

```PATCH /api/carts/{shoppingCartId}/item/{cartItemId}/increment``` <br>Incrementa a quantidade de um determinado item do carrinho através de seu ID;

```PATCH /api/carts/{shoppingCartId}/item/{cartItemId}/decrement``` <br>Decrementa a quantidade de um determinado item do carrinho através de seu ID;

```DELETE /api/carts/{shoppingCartId}/item/{cartItemId}``` <br>Remove um determinado item do carrinho através de seu ID;

```PATCH /api/carts/{shoppingCartId}/clear``` <br>Limpa o carrinho de compras;

![shoppingcartfunc][shoppingcartfunc]

## Pagamento

Após o carrinho de compras possuir todos os itens que o cliente deseja pedir, é necessário prosseguir para o pagamento:

```POST /api/checkout/cart/{shoppingCartId}``` <br>Cria um pagamento para um carrinho de compras existente;

Após a criação de um pagamento, o gerenciamento de seu *status* pode ser feito pelos seguintes endpoints:

```GET /api/checkout/{paymentId}/status``` <br>Retorna o *status* atual do pagamento;

```PUT /api/checkout/{paymentId}/confirm``` <br>Webhook que confirma o pagamento;

```PUT /api/checkout/{paymentId}/refuse``` <br>Webhook que recusa o pagamento;

![checkoutfunc][checkoutfunc]

## Pedidos

Após a criação de um pagamento, o pedido referente ao mesmo é criado. O seguinte endpoint pode ser utilizado para verificar todos os pedidos:

```GET /api/orders``` <br>Retorna os pedidos em uma lista paginada;

Após a confirmação do pagamento, o mesmo é enviado para a fila de pedidos. A seguir estão os endpoints utilizados para gerenciá-lo até sua finalização:

```GET /api/orders/queue``` <br>Retorna a fila de pedidos, com os pedidos em *status* Pronto, Em Preparação e Finalizado;

```PATCH /api/orders/{orderId}/start``` <br>Confirma o pedido, passando o mesmo para o *status* Em Preparação. 

```PATCH /api/orders/{orderId}/complete``` <br>Indica que o pedido foi preparado pela cozinha, passando o mesmo para o *status* Pronto;

```PATCH /api/orders/{orderId}/deliver``` <br>Indica que o pedido foi recebido pelo cliente, passando o mesmo para o *status* Finalizado;

```PATCH /api/orders/{orderId}/cancel``` <br>Cancela um pedido;

![orderfunc][orderfunc]

[menufunc]: ../.github/images/func_menu.png
[customerfunc]: ../.github/images/func_customer.png
[shoppingcartfunc]: ../.github/images/func_shopping_cart.png
[checkoutfunc]: ../.github/images/func_checkout.png
[orderfunc]: ../.github/images/func_order.png