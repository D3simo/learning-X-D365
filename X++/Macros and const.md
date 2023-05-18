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
