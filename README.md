## Problem:
### SQL to end user
I am building a web application which allow end user to customise the page, grid and report by amending the SQL. It was requested by business people.


In some reason, the system cannot set the 'read-only-access' permission to make the system safe, we have to control it from programming level. Imagine that one of your client try to execute the delete statement, it absolutely is a big problem. 

I understand exposing the entire SQL database structure to end users is not a good idea, it will make our life difficult.

# Simple Select Statement
Therefore, I am design a very simple select statement to end user, I just named it as "simple select statement"

Since the query is use in XML file, I avoid use the symbol like &, <, >.

The structure of the statement is very simple:
```
<table name>.[<field name>]| <filter>||<order field>
```
Example:
```
students.[name, address, contact]|name.sw('John')||name
```
this statement will translate it to sql
```sql
SELECT name, address, contact FROM students WHERE name LIKE 'John%' ORDER BY name
```


## Filter
There are various of operators supported in simple select statement.
| sql   |      sss      |  example |
|----------|:-------------:|------:|
| = |  eq() | name.eq('john') |
| != |    ne()   |   name.ne('john') |
| > | gt() |    age.gt(15)|
| >=| gte() | age.gte(15)|
|< | lt() | age.lt(20) |
| <= | lte() | age.lte(20) |
|in | in() | name.in('john','ali', 'ming')|
|not in | nin() | name.nin('john','ali')|
|like '%%'| ct()|name.ct('j')|
|like 'x%'| sw()| name.sw('j'|
|like '%x'| ew()| name.ew('n'|






