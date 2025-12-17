<p align="left">
  <img src="https://raw.githubusercontent.com/aaron-salisbury/ProjectTemplates/refs/heads/master/content/logo.png" width="175" alt=".Net Framework Toolkit Logo">
</p>

Project Templates
==========================

Visual Studio extension for mostly legacy project templates just in case I find myself in a restricted development scenario.

The templates help me when I want to quickly get started with a utility application that feels idiomatic.

For explicitly targeting modern versions of Windows, I recommend [Windows Template Studio](https://marketplace.visualstudio.com/items?itemName=WASTeamAccount.WindowsTemplateStudio).

Info
----
The extension has project templates that create a sample application that targets specific generations of Windows and/or .Net frameworks and contain example screens and processes.

Each legacy solution contains three projects, following a three-tier design. Data, Business, and Presentation. The app (presentation) project should be set as the startup project. For utilities that I know will always remain very small, I've found this design to suffice. If your app could be more than that however, it may be worth it to break the architecture down further into say services, domains, models, infrastructure, etc. Whatever your design pattern calls for.

The sample applications used to generate those project templates are also included in this repository.

Included Project Templates
--------------------------
  - Windows 98 App Solution Template (WinForms / .NET Framework 2.0)
  - Windows XP App Solution Template (WinForms / .NET Framework 3.5)
  - Windows 7 App Solution Template (WPF / .NET Framework 4.5.1)

## Versioning
This project uses [Semantic Versioning](https://semver.org/).

- **MAJOR** version: Incompatible API changes
- **MINOR** version: Backward-compatible functionality
- **PATCH** version: Backward-compatible bug fixes

Installation
------------
The latest release is available on the [Marketplace](https://marketplace.visualstudio.com/items?itemName=AaronSalisbury.WindowsProjectTemplates).

For the current development version, download and extract this repository, open the solution file using Visual Studio, and build the ProjectTemplates VSIX project. Then navigate to the bin folder of that project and inside the Debug (or Release depending on how you built it) folder and double-click the ProjectTemplates.vsix file. The next time you start Visual Studio, the project templates will be available for use when creating a new project.

## Build Requirements
- This repo contains a custom build solution/project, the VSIX solution contains a post-processing wizard project and the extension project, and there is a project for each template source. The build targets the LTS version of the [.NET SDK](https://dotnet.microsoft.com/en-us/download), the VSIX projects target .Net Framework 4.7.2, and the template source projects target their respective frameworks as specified above.
- The Build project uses [Cake](https://cakebuild.net/) (C# Make) as the build orchestrator and can be launched from your IDE or via script.

	- On OSX/Linux run:
	```bash
	./build.sh
	```
	- If you get a "Permission denied" error, you may need to make the script executable first:
	```bash
	chmod +x build.sh
	```

	- On Windows PowerShell run:
	```powershell
	./build.ps1
	```
