USE MyGuitarShop


--1. Create a view named CustomerAddresses that shows the shipping and billing addresses for each customer in the MyGuitarShop database.
--This view should return these columns from the Customers table: CustomerID, EmailAddress, LastName and FirstName.
--This view should return these columns from the Addresses table: BillLine1, BillLine2, BillCity, BillState, BillZip, ShipLine1, ShipLine2, ShipCity, ShipState, and ShipZip.
GO
CREATE VIEW CustomerAddresses 
AS
SELECT Customers.CustomerID,
       Customers.LastName,
	   Customers.FirstName,
	   Customers.EmailAddress,
	   Addresses.Line1 AS 'BillLine1',
	   Addresses.Line2 AS 'BillLine2',
	   Addresses.City AS 'BillCity',
	   Addresses.[State] AS 'BillState',
	   Addresses.ZipCode AS 'BillZip'
FROM Customers
JOIN Addresses
   ON --Customers.CustomerID = Addresses.CustomerID AND
      Customers.BillingAddressID = Addresses.AddressID
   
UNION

SELECT Customers.CustomerID,
       Customers.LastName,
	   Customers.FirstName,
	   Customers.EmailAddress,
	   Addresses.Line1 AS 'ShipLine1',
	   Addresses.Line2 AS 'ShipLine2',
	   Addresses.City AS 'ShipCity',
	   Addresses.[State] AS 'ShipState',
	   Addresses.ZipCode AS 'ShipZip'
FROM Customers
JOIN Addresses
  ON --Customers.CustomerID = Addresses.CustomerID AND
	 Customers.ShippingAddressID = Addresses.AddressID;
GO
SELECT * FROM CustomerAddresses;
--Use the BillingAddressID and ShippingAddressID columns in the Customers table to determine which addresses are billing addresses and which are shipping addresses.

--Hint: You can use two JOIN clauses to join the Addresses table to the Customers table twice (once for each type of address).

-- 2. Write a SELECT statement that returns these columns from the CustomerAddresses view that you created in exercise 1: CustomerID, LastName, FirstName, BillLine1.
SELECT * FROM CustomerAddresses;
SELECT CustomerAddresses.CustomerID,
       CustomerAddresses.LastName,
	   CustomerAddresses.FirstName,
	   CustomerAddresses.BillLine1
FROM CustomerAddresses;

/*
----------------Needs Fix
I couldn't get it to show both the billing address and and shipping address
*/

-- 3. Write an UPDATE statement that updates the CustomerAddresses view you created in exercise 1 so it sets the first line of the shipping address to “1990 Westwood Blvd.” for the customer with an ID of 8.

UPDATE CustomerAddresses
SET BillLine1 = "1990 Westwood Blvd."
WHERE CustomerID = 8;

-- 4. Create a view named OrderItemProducts that returns columns from the Orders, OrderItems, and Products tables.

--This view should return these columns from the Orders table: OrderID, OrderDate, TaxAmount, and ShipDate.

--This view should return these columns from the OrderItems table: ItemPrice, DiscountAmount, FinalPrice (the discount amount subtracted from the item price), Quantity, and ItemTotal (the calculated total for the item).

--This view should return the ProductName column from the Products table.

CREATE VIEW OrderItemProducts
AS
SELECT Orders.OrderID,
       Orders.OrderDate,
	   Orders.TaxAmount,
	   Orders.ShipDate,
	   OrderItems.ItemPrice,
	   OrderItems.DiscountAmount,
	   OrderItems.ItemPrice - OrderItems.DiscountAmount AS 'FinalPrice',
	   OrderItems.Quantity,
	   OrderItems.Quantity * (OrderItems.ItemPrice - OrderItems.DiscountAmount) AS 'ItemTotal',
	   Products.ProductName
FROM Orders
JOIN OrderItems
   ON Orders.OrderID = OrderItems.OrderID
JOIN Products
   ON OrderItems.ProductID = Products.ProductID;

   SELECT * FROM OrderItemProducts;
-- 5. Create a view named ProductSummary that uses the view you created in exercise 4. This view should return some summary information about each product.

--Each row should include these columns: ProductName, OrderCount (the number of times the product has been ordered), and OrderTotal (the total sales for the product).

CREATE VIEW ProductSummary
AS
SELECT ProductName,
       (SELECT SUM(OrderItemProducts.Quantity)
	   FROM OrderItemProducts AS OI1
	   JOIN OrderItemProducts AS OI2
	      ON OI1.ProductName = OI2.ProductName
		WHERE OI1.ProductName = OI2.ProductName)
	   AS 'OrderCount'
FROM OrderItemProducts
GROUP BY ProductName;

SELECT * FROM ProductSummary;


-- 6. Write a SELECT statement that uses the view that you created in exercise 5 to get total sales for the five best selling products.

--Sort the results by the total sales in descending order.
SELECT TOP 5 OrderCount
FROM ProductSummary
ORDER BY ProductSummary.OrderCount DESC;