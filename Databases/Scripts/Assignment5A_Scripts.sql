/*
Jose Angulo
Assignment 5A Scripts
*/

USE MyGuitarShop;
--1. Write a script that declares a variable and sets it to the count of all products in the Products table. If the count is greater than or equal to 7, the script should display a message that says, “The number of products is greater than or equal to 7”. Otherwise, it should say, “The number of products is less than 7”.
DECLARE @CountOfProducts varchar(255);
SET @CountOfProducts = (SELECT COUNT(*) FROM Products);
IF @CountOfProducts >= 7
   PRINT 'The number of products is greater than or equal to 7'
ELSE
   PRINT 'The number of products is less than 7';

--2. Write a script that uses two variables to store (1) the count of all of the products in the Products table and (2) the average list price for those products. If the product count is greater than or equal to 7, the script should print a message that displays the values of both variables. Otherwise, the script should print a message that says, “The number of products is less than 7”.
DECLARE @CountOfProducts2 varchar(255), @AverageListPrice money;
SET @CountOfProducts2 = (SELECT COUNT(*) FROM Products);
SET @AverageListPrice = (SELECT AVG(Products.ListPrice) FROM Products);
IF @CountOfProducts2 >= 7
   BEGIN
      PRINT 'The count of all the products in the Products table is ' + @CountOfProducts2 + '.';
      PRINT 'The average list price of these Products is $' + @AverageListPrice + '.';
   END
ELSE
   PRINT 'THe number of products is less than 7';

/* 3. Write a script that calculates the common factors between 10 and 20. To find a common factor, you can use the modulo operator (%) to check whether a number can be evenly divided into both numbers. Then, this script should print lines that display the common factors like this:

Common factors of 10 and 20

1

2

5

*/
DECLARE @Number1 int, @Number2 int, @CommonFactor decimal;
SET @Number1 = 10;
SET @Number2 = 20;
SET @CommonFactor = 1;

WHILE @Number1 >= @CommonFactor
   BEGIN
      IF @Number2 % @CommonFactor = 0 AND @Number1 % @CommonFactor = 0
	  BEGIN
	     PRINT @CommonFactor;
		 SET @CommonFactor = @CommonFactor + 1;
	  END
	  ELSE
	     SET @CommonFactor = @CommonFactor + 1;
	END

--4. Write a script that attempts to insert a new category named “Guitars” into the Categories table. If the insert is successful, the script should display this message:

--SUCCESS: Record was inserted.

--If the insert is unsuccessful, the script should display a message something like this:

--FAILURE: Record was not inserted.

--Error 2627: Violation of UNIQUE KEY constraint 'UQ__Categori__8517B2E0A87CE853'. Cannot insert duplicate key in object 'dbo.Categories'. The duplicate key value is (Guitars).
DECLARE @CategoryName varchar(255);
SET @CategoryName = 'Guitars';

BEGIN TRY
   INSERT INTO Categories
      VAlUES(@CategoryName);
   PRINT 'SUCCESS: Record was inserted';
END TRY
BEGIN CATCH
   PRINT 'FAILURE: Record was not inserted.';
   PRINT 'Error ' + CONVERT(varchar, ERROR_NUMBER(), 1) + ': ' + ERROR_MESSAGE();
END CATCH;
