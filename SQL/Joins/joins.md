# **Joins**

## Join <a name="Join"></a>

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

## Outer join

This is the same as "**Left outer join**" in **T-SQL**. It will return all records from the left table and matching records from the right table

## Exist join

This will return records from the first table, only if **records exist in second table**. It will not return any records from the second table. Number of records will not increase even iof there will be more records in second table. System will stop looking after it finds one match in the second table

## NotExists join

This return a row in the first table, if there is **no matching records in the second table**
