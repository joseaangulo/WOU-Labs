USE Faculty

--1.
SELECT Faculty.FirstName + ' ' + Faculty.LastName AS Name
FROM Faculty
WHERE Faculty.Salary < 20000;

--2.
SELECT Faculty.FirstName + ' ' + Faculty.LastName AS Name,
       Faculty.Salary
FROM Faculty
WHERE Faculty.Salary < 10000 OR
      Faculty.Salary > 30000;

--3.
SELECT Faculty.FirstName + ' ' + Faculty.LastName AS Name,
       Faculty.Phone,
	   Faculty.HireDate
FROM Faculty
WHERE Faculty.HireDate BETWEEN '1999-09-01' AND
                               '2003-09-01'
ORDER BY Faculty.HireDate ASC;

--4.
SELECT Faculty.FirstName + ' ' + Faculty.LastName AS Name
FROM Faculty
WHERE Faculty.DeptID IN (1,2,4)
ORDER BY Faculty.FirstName ASC;

--5.
SELECT Faculty.FirstName + ' ' + Faculty.LastName AS Name,
       Faculty.HireDate
FROM Faculty
WHERE Faculty.HireDate BETWEEN '1999-01-01' AND
                               '1999-12-31';

--6.
SELECT Faculty.FirstName + ' ' + Faculty.LastName AS Name,
       Faculty.Phone
FROM Faculty
WHERE Faculty.SupervisorID IS NULL;

--7.
SELECT Faculty.FirstName + ' ' + Faculty.LastName AS Name,
       Faculty.Salary,
	   Faculty.Stipend
FROM Faculty
WHERE Faculty.Stipend IS NOT NULL
ORDER BY Faculty.Salary ASC, Faculty.Stipend ASC;

--8.
SELECT Faculty.FirstName
FROM Faculty
WHERE Faculty.FirstName LIKE '__I%';

--9.
SELECT Faculty.FirstName,
       Faculty.LastName,
	   Faculty.DeptID, 
	   Faculty.SupervisorID
FROM Faculty
WHERE CHARINDEX('t', Faculty.LastName, CHARINDEX('t', Faculty.LastName) + 1) > CHARINDEX('t', Faculty.LastName) 
AND (Faculty.DeptID = 1 OR Faculty.SupervisorID = 1);

--10.
SELECT F.FirstName,
       F.LastName,
	   F.Salary
FROM Faculty AS F
WHERE F.Salary <> 10000 OR
      F.Salary <> 20000 OR
	  F.Salary <> 30000;

--11. Display the current date and label the column Date.
SELECT GETDATE() AS Date

--12.
SELECT F.FacultyID,
       F.FirstName,
	   F.LastName, 
	   F.Salary,
	   (F.Salary * 1.15) AS 'Raised Salary'
FROM Faculty AS F

--13.
SELECT F.FacultyID,
       F.FirstName,
	   F.LastName,
	   F.Salary,
	   (F.Salary * 1.15) AS 'Raised Salary', 
       (F.Salary * 1.15) - F.Salary AS 'Delta of Salary' 
FROM Faculty AS F

--14.
SELECT F.FirstName,
       F.LastName,
	   F.HireDate,
	   DATEDIFF(MM, F.HireDate, GETDATE()) 'Number Of Months'
FROM Faculty AS F

--15. Display all faculty members in the following format: Caldwell earns $3,000.00 monthly but 
--wants $15,000.00. (3 times their current monthly salary) Give the column the name Pipe Dream.
SELECT F.FirstName,
       F.LastName,
	   F.Salary / 12 AS 'Monthly Earnings',
	   (F.Salary / 4) AS 'Pipe Dream'
FROM Faculty AS F;

--16. Display the name and salary of all faculty members. Format the salary to be 15 characters 
--long, left-padded with $. Label the column salary.
SELECT F.FirstName,
       F.LastName, 
       REPLICATE('$',15 - LEN(F.Salary)) 
	      + CONVERT(VARCHAR, F.Salary) AS 'Padded Salary'
FROM Faculty AS F;

--17. Display the names, with only the first letter capitalized, and the length of 
--their name for all faculty members whose last name starts with a C, A, or D.
SELECT UPPER(SUBSTRING(F.FirstName,1,1)) 
          + LOWER(SUBSTRING(F.FirstName, 2, LEN(F.FirstName))) AS 'First Name', 
       UPPER(SUBSTRING(F.LastName,1,1)) 
	      + LOWER(SUBSTRING(F.LastName, 2, LEN(F.LastName))) AS 'Last Name',
       LEN(F.FirstName) + LEN(F.LastName) AS 'Length of Name'
FROM Faculty AS F
WHERE F.LastName LIKE 'C%' OR
      F.LastName LIKE 'A%' OR
	  F.LastName LIKE 'D%';

--18. Display the faculty name, hire date, and the day of the week they started. 
--Order the results by the day of the week starting with Monday.
SET DATEFIRST 1;
SELECT F.FirstName,
       F.LastName,
	   F.HireDate,
	   DATENAME(DW, F.HireDate) AS 'Week Day Of Hire'
FROM Faculty AS F
ORDER BY DATEPART(DW, F.HireDate);

--19. Display the name and stipend for all faculty members. If they don't have a 
--stipend, display "Not a Director or Chair".
SELECT F.FirstName,
       F.LastName,
       (CASE
	      WHEN F.Stipend IS NULL
		     THEN 'Not a Director or Chair'
	      ELSE CONVERT(VARCHAR, F.Stipend)
       END) AS 'Stipend'
FROM Faculty AS F;

--20. Display all faculty members’ last names followed by a row of asterisks which represent every $1000 
--of their salary. Order the data in descending order by their salaries.
SELECT F.LastName,
       REPLICATE('*', F.Salary / 1000) AS 'Salary in Thousands'
FROM Faculty AS F
ORDER BY F.Salary DESC;




