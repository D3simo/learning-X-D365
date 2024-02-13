# Quick info

Here is my X++ guide and documentation, usefull guides  found around the internet or in the books

## Table of content

1. [SQL](#SQL)
    1. [select](#Select)
    2. [while select](#WhileSelect)
    3. [join](#Join)
2. [X++](#X++)
    1. [Data types](#DataTypes)
    2. [Macros and consts](#Macros)

## **SQL** <a name="SQL"></a>

### **select statement** <a name="Select"></a>

### **Syntax example**

```SQL
-- declaration: X++ variables
CustTable custTable;
;

-- select * from custTable  select custTable
select custTable;
    where custTable.AccountNum == "100001";
info("AccountNum: " + custTable.AccountNum);
```

### **Good practises**

### Fieldlist

if code needs only specific fields then you should select Fieldlist instead of whole tableBuffer

```SQL
-- declaration: X++ variables
CustTable custTable;
;

-- select specific fields from custTable variable
select AccounNum, CustGroup from custTable;
    where custTable.AccountNum == "100001";
info("AccountNum: " + custTable.AccountNum);
info("CustGroup: " + custTable.CustGroup);
```

### firstonly

Similar to select top 1 in T-SQL, it tells the system to only take the very first record from the statement

```SQL
-- declaration: X++ variables
CustTable custTable;
;

select firstonly custTable;
    where custTable.AccountNum == "100001";
info("AccountNum: " + custTable.AccountNum);
```

### **while select statement** <a name="WhileSelect"></a>

Combination of SQL like syntax to retrieve values from the database, load them to into table buffer variable. Then we loop through results that we retrieved from the database

### **Getting Started**

We want to use while select statements when we loop through many results of select statement f.e. batch jobs

### **Syntax example**

```SQL
-- declaration: X++ variables
SalesTable salesTable;
;

while select salesTable
    where salesTable.SalesStatus == SalesStatus::Backorder
{
    Info(salesTable.SalesId);
}
```

### **Good practises**

### Nested while select into one while select

```SQL
-- declaration: X++ variables
SalesTable salesTable;
SalesLine salesLine;
;

while select salesTable 
    where salesTable.SalesStatus == SalesStatus::Backorder
{
    while select salesLine
        where salesLine.SalesId == salesTable.SalesId
    {
        Info(strFmt("SalesId: %1, LineNumber %2, ItemId %3", 
                    salesLine.SalesId, salesLine.LineNum, salesLine.ItemId));
    }
}
```

This will improve performance by around 20%

```SQL
-- declaration: X++ variables
SalesTable salesTable;
SalesLine salesLine;
;

while select salesTable 
    where salesTable.SalesStatus == SalesStatus::Backorder
    -- we swapped nested while with simple join salesLine
    join salesLine
        where salesLine.SalesId == salesTable.SalesId
{
    Info(strFmt("SalesId: %1, LineNumber %2, ItemId %3", 
                salesLine.SalesId, salesLine.LineNum, salesLine.ItemId));
}
```

But we can improve the performance even more

```SQL
-- declaration: X++ variables
SalesTable salesTable;
SalesLine salesLine;
;

-- we use fieldlist
while select SalesId, LineNum, ItemId from salesLine
    where salesLine.SalesId == salesTable.SalesId
    -- by switching the order, exist join validates if the record exists, it doesn't retrieve or load any data
    exists join salesTable
        where salesTable.SalesStatus == SalesStatus::Backorder
{
    Info(strFmt("SalesId: %1, LineNumber %2, ItemId %3", 
                salesLine.SalesId, salesLine.LineNum, salesLine.ItemId));
}
```

## **Common misunderstanding**

It does not display all the results, it takes records 1 by 1, not all at the same time


## **SQL joins** <a name="Join"></a>

### Join

It is the same as **Inner join** in **T-SQL**. This will return row when there is a match in both tables.
If there is **more than one row in the second table** that matches with record from the first table, this will result in **more rows in the result set** than what was found in the first table

### **Syntax**

```SQL
-- declaration: X++ variables
CustTable custTable;
SalesTable salesTable;
;

select firstonly salesTable;
    where salesTable.SalesId == "012506"
    join custTable
        where custTable.AccountNum == salesTable.CustAccount;

    --salesTable.RecId != 0 -> salesTable
    --custTable.RecId != 0 -> custTable
    if (salesTable
        & 
        custTable) 
    {
        Info(strFmt("Sales order %1 was found with customer name %2",
                    salesTable.SalesId,
                    custTable.name()
                    )
            );
    } 
    else 
    {
        Info("Sales order was not found");
    }
```

### Outer join

This is the same as "**Left outer join**" in **T-SQL**.
 It will return all records from the left table and matching records from the right table

### Exist join

This will return records from the first table,
 only if **records exist in second table**.
 It will not return any records from the second table.
 Number of records will not increase even iof there will be more records in second table. 
 System will stop looking after it finds one match in the second table

### NotExists join

This return a row in the first table, if there is **no matching records in the second table**

## **X++** <a name="X++"></a>

### Primitive Data Types <a name="DataTypes"></a>

- Anytype / var To store any data type
- Boolean To store true of false, This is a short number ( 1 or 0 )
- Enum To storeenumerated list values, a set of named constants. 
Element with its value set to 0 is treated as null value
- GUID To store a globally unqiue identifier    32 HEX        newGUID();
- Int64 To store whole number values. 0 is treated as null value. 
Internally a long number which is represen ted as 32 bit value
- Real To store numbers with decimal points. 0.0 is treated as null value
- Str To store numbers with decimal points. 
An empty string " or "" is treated as null value
- Date To store date ( day, month, year ) 1900-01-01 is treated as null value.
Max is 31.12.2154
- TimeOfDay To store time values, 00:00:00 is treated as null value
- UtcDateTime To store year, month, day, hours, minutes and seconds in UTC
Date portion as 1900-01-01 is treated as null value

### Macros and const <a name="Macros"></a>

Macros and const variables are used to define something that will not be modified

```X++
class test
{
    public static void main(Args _args) 
    {
        #define.myMacro("Macro value");
        const str constValue = "Constant value";

        info(strFmt("%1 - %2", #myMacro, constValue));
    }
}
```

```X++
static void MacroDemo(Args _args) 
{
    str mName;
    int mAge;
    #define.MyName('I am Desimo')
    #define.MyAge(30)

    mName = #MyName;
    mAge = #MyAge;

    print mName
    print mAge;

    pause;
}
```

#### If you have any Syntax errors(f.e brackets in macroValue), try locaMacros instead

```X++
class MacroExampleJob
{
    public static void main(Args _args)
    {
        CustTable   custTable;
        VendTable   ventTable;
        ;

        #localmacro.SelectCustomer
        select * from custTable
        #endmacro

        #localmacro.SelectVendor
        select * from vendTable
        #endmacro

        #localmacro.WhereClause
        where %1.AccoutNum == %2
            && %1.Currency == %3
        #endmacro

        #SelectCustomer
        #WhereClause(custTable, '100', 'USD');

        #SelectVendor
        #WhereClause('vendTable, '1000', 'PKR');
    }
}

```
