# Quick info

Here is my X++ guide and documentation, usefull guides  found around the internet or in the books

## Table of content

1. [SQL](#SQL)
    1. [select](#Select)
    2. [while select](#WhileSelect)
    3. [join](#Join)
2. [X++](#X++)
    1. [Data types](#DataTypes)
    2. [Access modifiers](#AccessModifiers)
    3. [Macros and consts](#Macros)
    4. [Operators](#Operators)
       1. [Relational operators](#RelationalOperators)
       2. [Ternary operators](#TernaryOperators)
    5. [TTS](#TTS)
    6. [Infolog](#Infolog)
    7. [Regex](#Regex)
    8. [Class](#Class)
       1. [Template](#Template)

<a name="SQL"></a>

## **SQL**

<a name="Select"></a>

### **select statement**

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

<a name="WhileSelect"></a>

### **while select statement**

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

<a name="Join"></a>

## **SQL joins**

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

<a name="X++"></a>

## **X++**

<a name="DataTypes"></a>

### Primitive Data Types

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

<a name="AccessModifiers"></a>

## Access modifiers

Public: Default behaviour for classess and methods. A public class can be inherited and class methods can be overridden in subclasses and called outside the class.
Protected: Only methods can be protected. A protected method can be overridden in subclasses, but can only be used inside the class hierarchy.
Private: Both classes and methods can be set as private. However this will only affects methods. A private method can only be used within the current class.

<a name="Macros"></a>

## Macros and const

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

### If you have any Syntax errors(f.e brackets in macroValue), try locaMacros instead

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

<a name="Operators"></a>

## Operators

<a name="RelationalOperators"></a>

### Relational operators

    like : Returns true if expression1 is like expression2
    
    ! : Not

    != : Inequality operator (not equal to)

    && : Logiccal AND

    || : Logical OR

    < : Less than

    <= : Less than or equal

<a name="TernaryOperators"></a>

### Ternary operator


It's most commonly used in assignment operations, although it has other uses as well. 
The ternary operator ? is a way of shortening an if-else clause, and is also called an immediate-if statement in other languages (IIf(condition,true-clause,false-clause) in VB, for example)
In this example: Return value based on condition

```X++
class test1
{
    public static void main(Args _args) 
    {
        int i;
        i = (4000 > 4) ? 1: 5;
        info(int2str(i));
    }
}
```

<a name="TTS"></a>

## ttsBegin, ttsCommit, ttsAbort

It's a transaction block for data changes between them.
They store in the memory all the changes that we are making to one or many records
ttsBegin: mark's the beginning of a transaction. This ensures data integrity and guarantees that all the updates performed until the transaction ends (by ttsCommit or ttsAbort) are consistent (all or none).

### TTS inside loops

If an error is thrown, only the last record will be reverted

<a name="Infolog"></a>

## Output Info log syntax

By using different Infolog/Output methods there are different popup bars in D365

```X++
class test
{
    public static void main(Args _args) 
    {
        info("Information notice to the interface");
        warning("Warning notice to the interface");
        error("Error notice to the interface");
    }
}
```

![output](/includes/InfoLog.png)

<a name="Regex"></a>

## How to use Regular Expression in Dynamics AX

A regular expression is a pattern that the regular expression engine attempts to match in input text. Dynamics AX also provides extensive support for regular expression. Using Regular Expression in Dynamics AX is very simple, due to the ability to call .NET objects from AX.

```X++
public static void TestRegex(Args _args) 
{
    #localmacro.VISARegex
        '^4[0-9]{12}(?:[0-9]{3})2$'
    #endmacro

    System.Text.RegularExpression.Match         regexMatch;
    System.Text.RegularExpression.Regex         regex;
    ;

    regex = new  System.Text.RegularExpression.Regex(#VISARegex);
    regexMatch = regex.match('7213628173657346');

    if (regexMatch.get_Success()) 
    {
        info("Success");
    }
}
```

<a name="Class"></a>

## Class

<a name="Templates"></a>

### Templates

<a name="new"></a>

#### new

```X++
int new(int _fileId)
{
    ;

    // fileId declared in for example classDeclaration
    fileId = _fileId

    return _fileId;
}
```

<a name="StatusMessage"></a>

#### Status message

```X++
str getStatusMessage(int status)
{
    str statusMessage;
    ;

    switch(status)
    {
        case -1:
            statusMessage = "Status unknown";
            break;
        case 0:
            statusMessage = "Success";
            break;
        case 1:
            statusMessage = "Custom Error";
            break;
        case 2:
            statusMessage = "Other custom error";
            break;
    }

    return statusMessage;
}
```

<a name="Description"></a>

#### Description

```X++
static ClassDescription description()
{
    return "Custom class description";
}
```
