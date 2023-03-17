USE MyGuitarShop

--1.
SELECT Categories.CategoryName,
       Products.ProductName,
	   Products.ListPrice
FROM Categories
JOIN Products
   ON Categories.CategoryID = Products.CategoryID
ORDER BY Categories.CategoryName, Products.ProductName ASC;

--2.
SELECT Customers.FirstName,
       Customers.LastName,
	   Addresses.Line1,
	   Addresses.City,
	   Addresses.State,
	   Addresses.ZipCode
FROM Customers
JOIN Addresses
ON Customers.CustomerID = Addresses.CustomerID
WHERE Customers.EmailAddress = 'allan.sherwood@yahoo.com';
--I'm selecting these columns from each table by Joining the Customers column to the Address column where there exists address information for the customer. Then I only want the customers with that email.

--3.
SELECT Customers.FirstName,
       Customers.LastName,
	   Addresses.Line1,
	   Addresses.City,
	   Addresses.ZipCode
FROM Customers
JOIN Addresses
ON Customers.CustomerID = Addresses.CustomerID
WHERE Customers.ShippingAddressID = Addresses.AddressID;

--4.
SELECT c.LastName,
       c.FirstName,
	   o.OrderDate,
	   p.ProductName,
	   oi.ItemPrice, oi.DiscountAmount,
	   oi.Quantity
FROM Customers AS c
JOIN Orders AS o
   ON c.CustomerID = o.CustomerID
JOIN OrderItems AS oi
   ON o.OrderID = oi.OrderID
JOIN Products AS p
   ON oi.ProductID = p.ProductID
ORDER BY c.LastName, o.OrderDate, p.ProductName;

--5.
SELECT DISTINCT Products1.ProductName, Products1.ListPrice
FROM Products AS Products1
JOIN Products AS Products2
   ON Products1.ListPrice = Products2.ListPrice

--6.
--SELECT Categories.CategoryName,
--       Products.ProductID
SELECT *
FROM Categories
LEFT JOIN Products
   ON Categories.CategoryID = Products.CategoryID
WHERE Products.CategoryID IS NULL;

--7.
SELECT Orders.OrderID,
       Orders.OrderDate,
	   'Ship Status'
FROM Orders
WHERE Orders.ShipDate IS NOT NULL

UNION

SELECT Orders.OrderID,
       Orders.OrderDate,
	   'Not Shipped'
FROM Orders
WHERE Orders.ShipDate IS NULL;