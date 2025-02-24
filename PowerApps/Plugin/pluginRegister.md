# **Register a plugin**

1. Open Plugin Registartion Tool
2. Register -> Register new assemly
3. Go to VS -> Solution Explorer -> Right click on the project -> Open in file explorer
4. Go to bin -> Debug
5. Find file YOURProjectName.dll and copy it's path
6. Go to Plugin Registartion Tool
7. Click 3 dots in Step 1 to Load Assembly.
8. Paste the path and choose YOURProjectName.dll file
9. Choose Sandbox then Database.
10. Plugin has been registered. Now you need an event. It can be a change of field.
11. Go to Plugin Registartion Tool
12. Register -> Register New Step
13. Message: Update
    Primary entity: primary entity
    Filtering attributes -> 3 dots and select only attributes you want track changes of
    Event pipeline Stage of execution: there are 4 stages, PreValidation 10, PreOperation 20, Co-Operation 30, Post Operation 40. We choose Post Operation, which hapopens when change has been committed to the database
    Leave other fields on default preferably
14. Test on Power Apps if your plugin works

## **LOCAL DEBUGGING ON PRODUCTION**

1. To debug, you need to install Profiler
2. Click Start Profiling
3. Specify profile storage -> Persist to Entity
4. Test in Power Apps
5. In Dynamics 365 -> Settings -> Extensions -> Plug in Profiles you can see all the executions
6. Place a breakpoint in VS, then go to Debug -> Attach to process -> search for plugin -> choose PluginRegistration process -> click Attach
7. In Plugin Registration Tool click Stop profiling.
8. Click Debug and select profile. Specify assemly location. Then click Start execution.
9. Target only contains values that are beeing updated. How to get values from the Target? Register an image in Plugin Registration Tool.
10. Register -> Register new image. Pick your step and name the Image. Change Target from variables to Name of your new Image. Start profiling again. Fill data, Save the new record. Now Stop profiling. This is now your image for debugging.
11. Debug in Plugin Registration Tool.
12. Build your project.
13. After debugging you need to update the assembly. Click Update button on the assembly in Plugin Registration Tool. Select All and click Update Selected Plugins.

## **Debug via traces**

1. add context.Trace($"Value for Show/Hide: {isConditionMet}"); to the code
2. Save and Build
3. Update the assembly in Plugin Registration Tool
4. Use Plugin Trace Viewer Tool to track all the traces
5. Refresh mode: Auto
6. Do changes
7. Click Retrieve logs
