# PowerInspect Apps

The App Launcher is a Visual Studio 2015 project using C#.

The apps are Visual Studio 2015 projects using Visual Basic.

* CopyTransform

The Autodesk PI CopyTransform app helps Autodesk PowerInspect users to copy, move or rotate features from the sequence tree. A copy of the selected characteristic is always created and the properties are changed according to the entries.

* DocModifier

The Autodesk PI DocModifier supports the Autodesk PowerInspect user in globally changing the properties of characteristics from the sequence tree that are to be evaluated in the test report.

* Miscellaneous

Autodesk PI Apps Miscellaneous provides the user of PowerInspect with a selection of different functions that are not included in the standard software.

## Build

* PiAppLauncher Target Framework: .NET Framework 4.5.1

* Apps Target Framework: .NET Framework 4.5

* Using/referencing PowerInspect: PowerInspect 2017 (17.1.0), x64

* Compile solution in VS2015, target x64, configuration "Release" => create dll, register dll as addin in PI
