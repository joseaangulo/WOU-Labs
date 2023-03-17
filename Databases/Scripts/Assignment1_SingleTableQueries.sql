USE MyGuitarShop

--1.
SELECT P.ProductCode AS 'Product Code'
, P.ProductName AS 'Product Name'
, P.ListPrice AS 'List Price'
, P.DiscountPercent 'Discount Percent'
FROM Products as P
ORDER BY P.ListPrice DESC;

--2.
SELECT C.LastName + ', ' + C.FirstName AS 'FullName'
FROM Customers AS C
WHERE C.LastName LIKE '[M-Z]%'
ORDER BY C.LastName ASC;

--3.
SELECT P.ProductName
, P.ListPrice
, P.DateAdded
FROM Products AS P
WHERE P.ListPrice BETWEEN 500 AND 2000
ORDER BY P.DateAdded DESC;

--4.
SELECT P.ProductName
, P.ListPrice
, P.DiscountPercent
, P.ListPrice * (P.DiscountPercent / 100) AS 'DiscountAmount'
, P.ListPrice * ((100 - P.DiscountPercent) / 100) AS 'DiscountPrice'
FROM Products AS P
ORDER BY DiscountPrice DESC;

--5.
SELECT O.ItemID
, O.ItemPrice
, O.DiscountAmount
, O.Quantity
, O.Quantity * O.ItemPrice AS 'PriceTotal'
, O.Quantity * O.DiscountAmount AS 'DiscountTotal'
, (O.ItemPrice - O.DiscountAmount) * O.Quantity AS 'ItemTotal'
FROM OrderItems AS O
WHERE (O.ItemPrice - O.DiscountAmount) * O.Quantity > 500
ORDER BY (O.ItemPrice - O.DiscountAmount) * O.Quantity DESC;

--6.
SELECT O.OrderID
, O.OrderDate
, O.ShipDate
FROM Orders AS O
WHERE O.ShipDate IS NULL;

--7.
DECLARE @Price int = 100
, @TaxRate float = .07
, @TaxAmount float = 100 * .07
, @Total float = 100 + (100 * .07)
SELECT @Price AS 'Price'
, @TaxRate AS 'TaxRate'
, @TaxAmount AS 'TaxAmount'
, @Total AS 'Total';

