# PowerInspect Report Creator for Microsoft Excel

Visual Studio 2015 solution using Visual Basic.

Application to read QDAS files (created by Autodesk PowerInspect).

The application will start Microsoft Excel and copy the parsed data into an Excel worksheet.

The format of the Excel worksheet, columns/rows to put the data etc. is configurable.

To Contribute please see [Contribute.md](Contribute.md). 

The extension is distributed under the MIT license. See [LICENSE.txt](LICENSE.txt).

## Build

* Target Framework: .NET Framework 4.5

* Using text files in format QDAS (exported by PowerInspect addin "SPC.dll") to create (configurable) reports in Excel.

* Compile solution in  Microsoft Visual Studio 2015, target x64, configuration "Release"

* The installer project requires the (Microsoft Visual Studio Installer Projects)[https://marketplace.visualstudio.com/items?itemName=VisualStudioClient.MicrosoftVisualStudio2017InstallerProjects]
