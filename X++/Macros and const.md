# Macros and const

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

If you have any Syntax errors(f.e brackets in macroValue), try locaMacros instead

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
