--1.Write a SELECT statement that returns these columns:
--The count of the number of orders in the Orders table
--The sum of the TaxAmount columns in the Orders table

SELECT COUNT(*) AS 'Number Of Orders',
       SUM(Orders.TaxAmount) AS 'Sum Tax Amount'
FROM Orders;

--2. Write a SELECT statement that returns one row for each category that has products with these columns:
--The CategoryName column from the Categories table
--The count of the products in the Products table
--The list price of the most expensive product in the Products table
--Sort the result set so the category with the most products appears first.

SELECT Categories.CategoryName AS 'Categories',
       Count(Products.ProductID) AS 'Number Of Products',
	   MAX(Products.ListPrice) AS 'Most Expensive Product'
FROM Categories
JOIN Products
   ON Products.CategoryID = Categories.CategoryID
GROUP BY Categories.CategoryName
ORDER BY [Number Of Products] DESC;

--3. Write a SELECT statement that returns one row for each customer that has orders with these columns:
/*The EmailAddress column from the Customers table
  The sum of the item price in the OrderItems table multiplied by the quantity in the OrderItems table
  The sum of the discount amount column in the OrderItems table multiplied by the quantiy in the OrderItems table
  Sort the result set in descending sequence by the item price total for each customer. */

SELECT Customers.EmailAddress,
       SUM(OrderItems.ItemPrice) * Count(OrderItems.ProductID) AS 'Item Price Total',
	   SUM(OrderItems.DiscountAmount) * Count(OrderItems.ItemID) AS 'Item Discount Total'
FROM Customers
JOIN Orders
   ON Customers.CustomerID = Orders.OrderID
JOIN OrderItems
   ON Orders.OrderID = OrderItems.OrderID
GROUP BY Customers.EmailAddress
ORDER BY [Item Price Total] DESC;

--4. Write a SELECT statement that returns one row for each customer that has orders with these columns:
/*The EmailAddress column from the Customers table
A count of the number of orders
The total amount for those orders (Hint: First, subtract the discount amount from the price. Then, multiply by the quantity.)
Return only those rows where the customer has more than 1 order.
Sort the result set in descending sequence by the sum of the line item amounts. */

SELECT Customers.EmailAddress,
       COUNT(Orders.OrderID) AS 'Number Of Orders',
	   SUM(OrderItems.ItemPrice - OrderItems.DiscountAmount) AS 'Item Final Price Total'
FROM Customers
JOIN Orders
   ON Customers.CustomerID = Orders.CustomerID
JOIN OrderItems
   ON Orders.CustomerID = Orders.CustomerID
GROUP BY Customers.EmailAddress
HAVING COUNT(Orders.OrderID) > 1
ORDER BY [Item Final Price Total] DESC;

--5. Modify the solution to exercise 4 so it only counts and totals line items that have an ItemPrice value that’s greater than 400.
SELECT Customers.EmailAddress,
       COUNT(Orders.OrderID) AS 'Number Of Orders',
	   SUM(OrderItems.ItemPrice - OrderItems.DiscountAmount) AS 'Item Final Price Total'
FROM Customers
JOIN Orders
   ON Customers.CustomerID = Orders.CustomerID
JOIN OrderItems
   ON Orders.CustomerID = Orders.CustomerID
WHERE OrderItems.ItemPrice > 400
GROUP BY Customers.EmailAddress
HAVING COUNT(Orders.OrderID) > 1
ORDER BY [Item Final Price Total] DESC;

--6. Write a SELECT statement that answers this question: What is the total amount ordered for each product? Return these columns:
SELECT Products.ProductName,
       SUM(OrderItems.ItemPrice - OrderItems.DiscountAmount) AS 'Total Amount Ordered'
FROM Products
JOIN OrderItems
   ON Products.ProductID = OrderItems.ProductID
GROUP BY Products.ProductName;

--7.
