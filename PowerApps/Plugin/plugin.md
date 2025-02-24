# **Power Apps Plugin**

1. Create a c# class library (.NET framework)
2. Delete default class file.
3. Manage Nuget Packages.
4. Install Microsoft.CrmSdk.CoreAssemblies
5. Add plugin base class.
6. Add plugin Tracer: <https://github.com/rappen/RappCanary365/blob/master/Plugin/CanaryTracer.cs>
7. Add plugin code
8. Add schema name of particular fields
9. Add logic what need to be done
10. Update fields
11. Go to Project properties (Right side -> Properties)
12. Go to Signing
13. Go to Sign in the assembly and Choose New
14. Fill the key
15. Save and verify if file .snk has been created in the solution
16. Build the project and deploy to the dataverse
