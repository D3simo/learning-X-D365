# Quick info

Here is my X++ guide and documentation, usefull guides  found around the internet or in the books

## Table of content
1. [SQL](#SQL)
    2. [select](#Select)

## **SQL** <a name="SQL"></a>

## **select statement** <a name="Select"></a>

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

## **Good practises**

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

This is the same as "**Left outer join**" in **T-SQL**. It will return all records from the left table and matching records from the right table

### Exist join

This will return records from the first table, only if **records exist in second table**. It will not return any records from the second table. Number of records will not increase even iof there will be more records in second table. System will stop looking after it finds one match in the second table

### NotExists join

This return a row in the first table, if there is **no matching records in the second table**
