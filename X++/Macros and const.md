# Macros and const

Macros and const variables are used to define somehtign that will not be modified

```X++
class test
{
    public static void main(Args _args) 
    {
        #define.macroValue("Macro value");
        const str constValue = "Constant value";

        info(strFmt("%1 - %2", #macroValue, constValue));
    }
}
```
