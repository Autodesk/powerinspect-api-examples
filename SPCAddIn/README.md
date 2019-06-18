# SPC Add-In

PowerInspect add-in for Statistical Process Control (SPC).

## How to build SPC
1. Install pre-requisites.
    * Visual Studio 2017.
    * Current version of PowerInspect.
2. Register PowerInspect using the `Make Autodesk PowerInspect XXXX the Current Version` shortcut.
3. Open the solution `SPCAddIn.sln` in Visual Studio and build the SPC project.

## How to use
1. Start PowerInspect.
2. In the file menu, choose Options and select the Add-In Manager.
3. In the Add-In Manager dialog, select Browse and navigate to the build folder of your project.
4. Check the check box in the list to select the SPC add-in and click OK.

## How to run the tests
1. Build the SPCNUnit project.
2. Run the tests using the Visual Studio Test Explorer window.
