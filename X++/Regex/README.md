# How to use Regular Expression in Dynamics AX

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
