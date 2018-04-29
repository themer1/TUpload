INTRODUCTION:

+ Simple ASP .NET web application to upload an excel file (.xlsx format)

+ Once the file uploads, application stores it in the MS SQL Server Database

+ Also shows the count of records saved successfully and lists the records that have not been saved

+ User can also view existing transactions in the database

+ Transactions are validated based off the currency and amount

+ If the transaction currency is not valid or transaction amount is not a decimal number, then transaction is considered incorrect

KNOWN ISSUES:

- file format is not being checked. It might throw an exception

- file is not being closed gracefully


TECH STACK USED:

+ ASP .NET and Web Tools (2017)

+ C# .NET

+ Entity Framework

+ Microsoft SQL Server 2017
