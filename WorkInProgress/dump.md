Summary: minor modifications

==Introduction==
The RunBaseBatch [[class]] extends the RunBase class and allows one to create classes (jobs) that can be added to the batch queue. Once the job has been added to the queue, it will be executed by the batch server. This concept is known as [[batch processing]].

==Input data==
Usually a class contains logic only and does not have a graphical user interface to interact with. Because the RunBaseBatch class inherits from the RunBase class, it uses the [[Dialog]] framework to interactively prompt users for input data (parameters). The parameters are class member variables and are added to the dialog box. The type of control that is used on the dialog depends on the [[Extended Data Types|Extended Data Type]] that is used when adding the controls to the dialog. Certain properties of the controls can be modified on the Extended Data Type itself. E.g. with Base enums it is possible to set the style property to Auto, Combo box or Radio button.

If a control is added using variable of type Boolean, it will be created on the dialog as a checkbox, enum as a combo box, string as a text edit control. Each dialog control has a corresponding class variable which is used to set and get the values from the dialog controls and to persist the values for use later when the job is executed by the batch server. Usually the controls are added to the dialog in code. Because the controls are added during runtime, overriding the methods of the controls requires a different approach compared to a standard form. Instead of adding the controls in code, it is possible to use an existing dialog that has been created in the AOT. This gives the same level of control over the controls as a standard form.

The parameters are used to control the logic of the job. This improves the flexibility and re-usability of jobs. In other words, a job can behave differently depending on the selected parameters. For example, a job that cleans up a log table might accept a days parameter allowing the user to specify how "old" the records should be to delete. If the user enters 30, the job deletes all records that are older than 30 days.

==Persistence==
The parameters are class variables and their values are persisted (packed) in the database by the SysLastValue framework. The parameters are packed when the job is added to the batch queue and are retrieved (unpacked) from the database by the SysLastValue framework when the job is executed by the batch server. The class variables are re-instantiated to their original values in the unpack method. In order for a class to persist the values of class variables, the variables must added to a local macro list and the class must override and implement the pack and unpack methods correctly. In fact, because the RunbaseBatch class implements the SysPackable interface, all child classes of RunBaseBatch must override the pack and unpack methods.

The pack method is called by the RunBaseBatch framework when a job is placed in the batch queue by the user. The unpack method is called before the job is executed by the batch server. When the job is executed by the batch server, the new, unpack and run methods are the only methods called by the RunBaseBatch framework. The dialog is not shown because the job is executed by another computer, or server, and there is no user interaction expected at this point. The run method should be overridden and the core code of the class should be placed here.

==Query==
The RunBaseBatch class supports the use of a [[query]] to make a selection on underlying data. If the class uses a query, and the queryRun method is overridden, the RunBase framework automatically displays the query ranges and their values on the dialog. By clicking on the "Select" button, the user can modify the query by setting or removing query values or by adding new ranges. It is possible to extend the query by adding relations (joins) to related tables and lastly by changing the sort order. If the query is going to be used re-used, it is recommended to create the query in the [[AOT]] instead of in code.

==Operation progress==
If a job performs an operation that takes a considerable length of time, it is recommended to display a progress dialog. The RunBase framework uses the Operation Progress framework to display the progress of an operation. The RunBaseBatch class inherits this functionality from the RunBase class. In order to display the current progress, code must be added to the run method of the class.

==Making it run==
By default a class is not "executable" i.e. it cannot be opened from the main menu or from a form with the use a menu item. In order to make a class "executable", it must have a static main method (entry point) with a single Args parameter. When a class is executed, the kernel calls the static main method of the class and passes it an Args object. The main method is called interactively and is not called by the batch server.

The Args object contains properties that can be used by the class. E.g. if the Args object contains a reference to an active buffer, the main method can use this buffer to construct specialised child classes of the parent class depending on the values of the fields in the buffer. This is known as the constructor based object-oriented design pattern and is used extensively throughout the application. In addition to having an active buffer, the Args object might contain a reference to the class’s caller which can be a form or an object. The Args class provides a convenient mechanism to communicate bewteen objects and is widely used in the application. With the use of a menu item, the class (job) can be accessed from the main menu or from a form. It is important to always set the security key of new menu items.

==Considerations==
===Three tier===
Reading and writing of files usually occurs on the client tier. Because the job is executed by the batch server, the path that is used must be accessible to the batch server or the AOS depending on which tier the job is executed. Stack trace errors will occur if a path on the client tier is used because the batch server and the AOS cannot access the file system of the client.

===New method===
Because classes that inherit from RunBaseBatch can be executed in a batch journal, the new method of should not have any arguments. The reason for this is that object variable types might cause stack trace errors when the system enumerates all child classes of RunBaseBatch during the process of setting up batch journals.

===Version number===
Class variables are added to a local macro as a "list" of variables so that their values can be persisted by the SysLastValue framework. The variable list has a corresponding version number in order to keep track of the different versions of lists that might exist. The version number usually starts at one and is increased every time that the number of variables in the list is changed. The unpack method should be modified accordingly to allow the values to the variables to be assigned correctly and to prevent stack trace errors from occurring when the size of the persisted variables does not match the size of the local macro list.

==Methods==
===new===
Creates a new instance of the class. Called when executed in batch.

===pack===
Class variables are persisted by using a local macro. Called when the dialog box is closed by selecting "OK".

===unPack===
The class variables in the local macro are re-instantiated. The unpack method is called before the run. Called when executed in batch.

===dialog===
Controls are added to the dialog box and the values of the controls are set by the the values of their corresponding class variables.

===getFromDialog===
The values of the class variables are assigned by the values of the controls that have been added to the dialog box.

===validate===
The input parameters can be validated. If the input are invalid, an info log can be displayed so the user can take action.

===run===
The central method of the class.. Called when executed in batch.

===initParmDefault===
Called when the unPack method returns false i.e. no usage data found. Class variables that are in the macro list should be initialised in the initParmDefault method.

===queryRun===
If the class uses a query, this method should return a queryRun object.

===canGoBatchJournal===
Indicates if the class(job) can be included in a batch journal. Default is false.

===canGoBatch===
Indicates if the class can be executed in batch. The framework adds the "Batch" tab page to the dialog if this method returns true. Default is true.

===runsImpersonated===
Indicates if the job should be run using the submitting user’s account or the batch user’s account. The default is to use batch user’s account. If true is returned, the batch server will execute the job using the X++ "runAs" statement. Version 4 and later. Default is false.

===description (static)===
Returns the description of the (base) class and will be displayed as the description in the batch list and determines the title of the dialog

===caption===
The description of the class can be overridden by returning the description of the child class. It will be displayed as the description in the batch list and determines the title of the dialog.

==Creating a basic batch job step by step==

From the AOT create a class and extend it from RunBaseBatch

class AV_RunbaseBatchDemo extends RunbaseBatch
{
}


In the class declaration add the following

NoYes displayMessage;
Description message;

DialogField fieldDisplayMessage;
DialogField fieldMessage;

#define.CurrentVersion(1)

#localmacro.CurrentList
displayMessage,
message
#endmacro


Override the pack method

public container pack()
{
return [#CurrentVersion, #CurrentList];
}


Override the unpack method

public boolean unpack(container _packedClass)
{
Integer version = RunBase::getVersion(_packedClass);
;

switch (version)
{
case #CurrentVersion:
[version, #CurrentList] = _packedClass;
break;

default :
return false;
}

return true;
}


Create the description method, this will be displayed in the batch queue and it will the caption of the dialog.

public static ClassDescription description()
{
return "Runbase batch demo";
}


Create the construct method

public static AV_RunbaseBatchDemo construct()
{
return new AV_RunbaseBatchDemo();
}


Override the initParmDefault method

public void initParmDefault()
{
super();
displayMessage = NoYes::Yes;
message = "Hello World!";
}


Override the dialog method

protected Object dialog()
{
DialogRunbase dlg;
;

dlg = super();
fieldDisplayMessage = dlg.addFieldValue(typeId(NoYes), displayMessage, "Display message", "Indicates whether to display a message or not.");
fieldMessage = dlg.addFieldValue(typeId(Description), message, "Message", "The message to display");

return dlg;
}


Override the getFromDialog method

public boolean getFromDialog()
{
boolean ret;
;

ret = super();
displayMessage = fieldDisplayMessage.value();
message = fieldMessage.value();

return ret;
}


Override the run method

public void run()
{
if (displayMessage)
{
info(message);
}
}


Create the static main method

public static void main(Args _args)
{
AV_RunbaseBatchDemo runbaseBatchDemo = AV_RunbaseBatchDemo::construct();
;
if (runbaseBatchDemo.prompt())
{
runbaseBatchDemo.run();
}
}
