# **select statement** 

## **Syntax example**

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
