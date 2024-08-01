# How to find classes in Axapta

How to find class id or class name:

```X++
static void Find_Class(Args_args)
{
    ;
    info(classId2Name(ClassId))
    info(int2Str(className2Id('ClassName')));
}
```
