# **while select statement**

Combination of SQL like syntax to retrieve values from the database, load them to into table buffer variable. Then we loop through results that we retrieved from the database

## **Getting Started**

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

## **Good practises**

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
