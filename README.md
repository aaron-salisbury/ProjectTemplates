Project Templates
==========================

Visual Studio extension for legacy project templates just in case I find myself in a restricted development scenario.

For Windows 10 I recommend [Windows Template Studio](https://marketplace.visualstudio.com/items?itemName=WASTeamAccount.WindowsTemplateStudio) instead.

Info
----
The extension has project templates that create a sample application that targets specific generations of Windows and .Net frameworks and contain example screens and processes.

Each solution contains two projects, the app project which should be set as the startup project, and a "Core" library. The core library is setup for business logic, models, and domains, but does not presume a design pattern and could be further split a part however desired.

The sample applications used to generate those project templates are also included in this repository.

To install the extension, download and extract this repository, open the solution file using Visual Studio, and build the ProjectTemplates VSIX project. Then navigate to the bin folder of that project and inside the Debug (or Release depending on how you built it) folder and double click the ProjectTemplates.vsix file.

The next time you start Visual Studio, the project templates will be available for use when creating a new project.

Included project templates:

  - Windows 7 App Solution Template (WPF / .NET Framework 4.5.1)
