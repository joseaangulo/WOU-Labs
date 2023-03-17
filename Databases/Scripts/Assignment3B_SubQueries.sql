USE MyGuitarShop

--1. Write a SELECT statement that returns the same result set as this SELECT statement, but don’t use a join. Instead, use a subquery in a WHERE clause that uses the IN keyword.

SELECT DISTINCT CategoryName
FROM Categories c JOIN Products p
ON c.CategoryID = p.CategoryID
ORDER BY CategoryName

SELECT DISTINCT CategoryName
FROM Categories c 
WHERE c.CategoryID IN
   (SELECT p.CategoryID
    FROM Products p)
ORDER BY CategoryName;


--2.Write a SELECT statement that answers this question: Which products have a list price that’s greater than the average list price for all products?
--Return the ProductName and ListPrice columns for each product.
--Sort the results by the ListPrice column in descending sequence.

SELECT Products.ProductName,
       Products.ListPrice
FROM Products
WHERE Products.ListPrice >
      (SELECT AVG(Products.ListPrice)
	   FROM Products)
ORDER BY Products.ListPrice DESC;


--3. Write a SELECT statement that returns the CategoryName column from the Categories table.
--Return one row for each category that has never been assigned to any product in the Products table. To do that, use a subquery introduced with the NOT EXISTS operator.

SELECT Categories.CategoryName
FROM Categories
WHERE Categories.CategoryID NOT IN 
      (SELECT Products.CategoryID
	   FROM Products)

--4.Write a SELECT statement that returns three columns: EmailAddress, OrderID, and the order total for each customer. To do this, you can group the result set by the EmailAddress and OrderID columns. In addition, you must calculate the order total from the columns in the OrderItems table.
--Write a second SELECT statement that uses the first SELECT statement in its FROM clause. The main query should return two columns: the customer’s email address and the largest order for that customer. To do this, you can group the result set by the EmailAddress column.
SELECT EmailAddress,
       Orders.OrderID,

FROM Customers
JOIN Orders
   ON Customers.CustomerID = Orders.OrderID


--5. Write a SELECT statement that returns the name and discount percent of each product that has a unique discount percent. In other words, don’t include products that have the same discount percent as another product.
--Sort the results by the ProductName column.
SELECT P1.ProductName,
       P1.DiscountPercent
FROM Products AS P1
WHERE P1.DiscountPercent NOT IN
      (SELECT P2.DiscountPercent
	   FROM Products AS P2
	   WHERE P1.ProductName <> P2.ProductName)
ORDER BY ProductName;

--6.Use a correlated subquery to return one row per customer, representing the customer’s oldest order (the one with the earliest date). Each row should include these three columns: EmailAddress, OrderID, and OrderDate

SELECT Customers.EmailAddress,
       --O.OrderID,
	   --O.OrderDate
FROM Customers
WHERE Customers.CustomerID IN
      (SELECT O.CustomerID
	   FROM Orders AS O
	   WHERE O.OrderDate =
	   (SELECT MIN(O2.OrderDate)
	    FROM Orders AS O2
		WHERE O.OrderID = O2.OrderID))