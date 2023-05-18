# Quick info

Here is my X++ guide and documentation, usefull guides  found around the internet or in the books

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

![output](InfoLog.png)

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

## Relational operators

like : Returns true if expression1 is like expression2

! : Not

!= : Inequality operator (not equal to)

&& : Logiccal AND

|| : Logical OR

< : Less than

<= : Less than or equal

## Ternary operator

In this example : Return value based on condition

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
