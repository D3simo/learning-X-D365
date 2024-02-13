# Status message

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