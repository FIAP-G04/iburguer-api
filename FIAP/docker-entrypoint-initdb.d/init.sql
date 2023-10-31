CREATE TABLE "Customers" (
    "Id" UUID NOT NULL,
    "CPF" TEXT NOT NULL,
    "FirstName" CHARACTER VARYING(30) NOT NULL,
    "LastName" CHARACTER VARYING(80) NOT NULL,
    "Email" CHARACTER VARYING(60) NOT NULL,
    "RegistrationDate" TIMESTAMP WITHOUT TIME ZONE NOT NULL,
    "LastUpdated" TIMESTAMP WITHOUT TIME ZONE NOT NULL,
    CONSTRAINT "PK_Customers" PRIMARY KEY("Id")
);

CREATE TABLE "Products" (
    "Id" UUID NOT NULL,
    "Name" CHARACTER VARYING(40) NOT NULL, 
    "Description" CHARACTER VARYING(300) NOT NULL,
    "Price" MONEY NOT NULL,
    "Category" TEXT NOT NULL,
    "PreparationTime" INTEGER NOT NULL, 
    "Enabled" BOOLEAN NOT NULL,
    CONSTRAINT "PK_Products" PRIMARY KEY("Id")
);

CREATE TABLE "Images" (
    "Id" UUID NOT NULL,
    "ProductId" UUID NOT NULL,
    "Url" TEXT NOT NULL,
    CONSTRAINT "PK_Images" PRIMARY KEY("Id"),
    CONSTRAINT "FK_Images_Products_ProductId" FOREIGN KEY("ProductId") REFERENCES "Products" ("Id") ON DELETE CASCADE 
);

CREATE TABLE "Orders" (
    "Id" UUID NOT NULL,
    "ShoppingCartId" UUID NOT NULL,
    "WithdrawalCode" TEXT NOT NULL,
    "OrderNumber" INTEGER NOT NULL,
    CONSTRAINT "PK_Orders" PRIMARY KEY("Id")
);

CREATE TABLE "Payments" (
    "Id" UUID NOT NULL,
    "ShoppingCartId" UUID NOT NULL,
    "Amount" MONEY NOT NULL,
    "PayedAt" TIMESTAMP WITHOUT TIME ZONE,
    "RefusedAt" TIMESTAMP WITHOUT TIME ZONE,
    "Status" TEXT NOT NULL,
    "Method" TEXT NOT NULL,
    CONSTRAINT "PK_Payments" PRIMARY KEY("Id")
);

CREATE TABLE "ShoppingCarts" (
    "Id" UUID NOT NULL,
    "CustomerId" UUID,
    "Closed" BOOLEAN NOT NULL,
    CONSTRAINT "PK_ShoppingCarts" PRIMARY KEY("Id")
);

CREATE TABLE "OrderTrackings" (
    "Id" UUID NOT NULL,
    "Status" TEXT NOT NULL,
    "When" TIMESTAMP WITHOUT TIME ZONE NOT NULL,
    "Order" UUID NOT NULL,
    CONSTRAINT "PK_OrderTrackings" PRIMARY KEY("Id"),
    CONSTRAINT "FK_OrderTrackings_Orders_Order" FOREIGN KEY("Order") REFERENCES "Orders" ("Id") ON DELETE CASCADE
);

CREATE TABLE "CartItems" (
    "Id" UUID NOT NULL,
    "ShoppingCart" UUID NOT NULL,
    "ProductId" UUID NOT NULL,
    "Price" MONEY NOT NULL,
    "Quantity" INTEGER NOT NULL,
    CONSTRAINT "PK_CartItems" PRIMARY KEY("Id"),
    CONSTRAINT "FK_CartItems_ShoppingCarts_ShoppingCart" FOREIGN KEY("ShoppingCart") REFERENCES "ShoppingCarts" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_CartItems_ShoppingCart" 
ON "CartItems" ("ShoppingCart");

CREATE INDEX "IX_OrderTrackings_Order"
ON "OrderTrackings" ("Order");

CREATE SEQUENCE sq_order_number START 1 MAXVALUE 10000000 INCREMENT 1;
