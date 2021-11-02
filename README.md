# Problem
### SQL to end user
I am building a html template engine (web component based) which allow end user to customise the page with data query language.

In some reason, the system cannot set the 'read-only-access' permission to make the system safe, we have to control it from programming level. Imagine that one of your client try to execute the delete statement, it absolutely is a big problem. 

I understand exposing the entire SQL database structure to end users is not a good idea, it will make our life difficult.

# The Concept of Simple Select Statement
Therefore, I am redesign a simple select statement to end user and named it as "simple select statement". Which is a mutated version of SQL.

Since the query is use in HTML file (XML based), I avoid use the symbol like &, <, >.

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


## Alias
In SSS, use @ to denote alias, example
```
students.[name@'student name]'
```
this will transpile to sql
```sql
SELECT name as 'student name' FROM students



