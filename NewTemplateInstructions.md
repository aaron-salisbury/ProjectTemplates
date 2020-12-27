Step 1: Create Visual Studio Extension Project
----------------------------------------------

For our final template to be used in a way that maintains all the references to projects and nuget packages that we provide, we need to host it all in a Visual Studio extension that manage it all behind the scenes.

Create a VSIX project if you do not already have one. Right-click the VSIX project in the Solution Explorer and add a new folder, name it Packages. Add another new folder and name it ProjectTemplates. Open the file with the .vsixmanifest extension and make note of the Product ID in there. We will need it in a later step.

Step 2: Generate Project Templates
-----------------------------------

This process must be done for each project that you want your template to generate separately. Later we will combine them into one root template.

Get each of your projects to the point that you want to use them as the basis of a complete template.

For each one, go to Project -> Export Template..., then select 'Project Template' and in the drop-down control select the project.

On the next screen, you can give your template a simple name and description, but the combined template name and description we do later will be the ones that users ultimately see. Leave the automatic import option unchecked. No need for a special icon here either.

The project template zip files should be exported to C -> Users -> your-user-name -> Documents -> Visual Studio visual-studio-version -> My Exported Templates

Step 3: Edit Generated Project Templates
----------------------------------------

The default generation process is designed for a single project only. We need to make edits in order to preserve nuget packages and project references to each other. Do the following for each project template that you generated.

Extract each zipped project template. In each class that has references to another project that we also templated, we need to modify its using statement using a parameter.

  - If the statement looks like: using OtherProject.Core.Services;

  - Make it: using $ext_safeprojectname$.Core.Services;

Similarly, to preserve the reference to that project, open the csproj and find the ProjectReference element.

  - If it looks like:
  
```xml
    <ProjectReference Include="..\OtherProject.Core\OtherProject.Core.csproj">
      <Project>{bebcbf6c-fa12-429d-add3-d0bdfc63654b}</Project>
      <Name>OtherProject</Name>
    </ProjectReference>
```

  - Make it look like:
  
```xml
    <ProjectReference Include="..\$ext_safeprojectname$.Core\$ext_safeprojectname$.Core.csproj">
      <Project>{bebcbf6c-fa12-429d-add3-d0bdfc63654b}</Project>
      <Name>$ext_safeprojectname$.Core</Name>
    </ProjectReference>
```

While in the csproj, we can update the path to any nuget packages that later we will include in a VSIX project.
*Can SKIP this step if the package references are not pathed, like in UWP projects.

  - If the package reference looks like:
  
```xml
    <Reference Include="Serilog, Version=2.0.0.0, Culture=neutral, PublicKeyToken=24c2f752a8e58a10, processorArchitecture=MSIL">
      <HintPath>..\packages\Serilog.2.10.0\lib\net45\Serilog.dll</HintPath>
    </Reference>
```

  - Make it look like by stepping up one directory:
  
```xml
    <Reference Include="Serilog, Version=2.0.0.0, Culture=neutral, PublicKeyToken=24c2f752a8e58a10, processorArchitecture=MSIL">
      <HintPath>..\..\packages\Serilog.2.10.0\lib\net45\Serilog.dll</HintPath>
    </Reference>
```

Finally open/edit the .vstemplate file. We need add instructions for the project template wizard to install the nuget packages.
*Can SKIP these two wizard steps if the package references are not pathed, like in UWP projects.

Add the following as a child element of VSTemplate:
  
```xml
  <WizardExtension>
    <Assembly>NuGet.VisualStudio.Interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a</Assembly>
    <FullClassName>NuGet.VisualStudio.TemplateWizard</FullClassName>
  </WizardExtension>
```
  
Then, below that and as another child of VSTemplate:
  
```xml
  <WizardData>
    <packages repository="extension" repositoryId="ProjectTemplates.10af0e32-efeb-48aa-b1ad-0f95a249367d">
        <package id="Serilog" version="2.10.0" />
        <package id="System.ComponentModel.Annotations" version="5.0.0" />
    </packages>
  </WizardData>
```

Replace the repositoryId property value with the Product ID from the VSIX project's manifest that we made note of earlier.

Replace the id and version property values with the nuget packages from the packages.config file of your project.

Step 4: Combine Templates into One Root Template
------------------------------------------------

Ultimately, we want the user to be able to select one template to generate all of our solution project structure.

Make a new folder and name it after what that one template will be called.

Move the extracted project template folders that we've been working in so far into the new folder.

If you would like your final template to have an icon, add that to the new folder as well.

Next create a new text file and save it to the folder as Root.vstemplate

Copy and paste the following into the text file:

```xml
<VSTemplate Version="3.0.0" Type="ProjectGroup" xmlns="http://schemas.microsoft.com/developer/vstemplate/2005">
  <TemplateData>
    <Name>Template Name</Name>
    <Description>Template description.</Description>
    <DefaultName>Win7_Solution</DefaultName>
    <Icon>__TemplateIcon.ico</Icon>
    <ProjectType>CSharp</ProjectType>
    <ProjectSubType>
    </ProjectSubType>
    <SortOrder>1000</SortOrder>
    <CreateNewFolder>true</CreateNewFolder>
    <ProvideDefaultName>true</ProvideDefaultName>
    <LocationField>Enabled</LocationField>
    <EnableLocationBrowseButton>true</EnableLocationBrowseButton>
  </TemplateData>
  
  <TemplateContent>
    <ProjectCollection>
      <ProjectTemplateLink ProjectName="$safeprojectname$.Core">
        ProjectOne\MyTemplate.vstemplate
      </ProjectTemplateLink>
      <ProjectTemplateLink ProjectName="$safeprojectname$.App" CopyParameters="true">
        ProjectTwo\MyTemplate.vstemplate
      </ProjectTemplateLink>
    </ProjectCollection>
  </TemplateContent>
</VSTemplate>
```

Replace the Name, Description, DefaultName, and Icon element values with your own information. You can copy the generic template icon into the new root folder from one of the generated template folders, or use your own value in that element.

In the <ProjectCollection> element, we need a ProjectTemplateLink for each project we generated templates for and want to combine into this root template. Name them how you want them to be generated, but using $safeprojectname$ will plug in what the user choose during the template wizard.

In the value of the ProjectTemplateLink, replace with the template folder structure (the folder you added to the new root folder).

Now save the file. Select (highlight) each file & folder in the root folder. Right-click and select 'Send To' -> 'Compressed (zipped) folder'.

Name the zip file what you want the template to be called.

Step 5: Add Template to Visual Studio Extension Project
-------------------------------------------------------

Now we can go back to the VSIX project that we setup at the beginning. 

Copy each nupkg file that we annotated in the WizardData elements earlier into the Packages folder and then add to the project (add existing new item). Make sure each file is set to Copy always in the properties. If the same package was used in more than one project, only one needs to be copied over here, the wizard can use it over again for each project that needs it.

Move the project template that we zipped into the ProjectTemplates folder and add it to the project. Then open the manifest file and select the Assets tab. Press the New button, choose the ProjectTemplate type, File on filesystem Source, and browse for the Path to the zipped template we just moved into the ProjectTemplates folder. Save that.

Finally, right-click the manifest file and View Code. As children to the Assets element, we need to include our nuget package assets, like the following:

        <Asset Type="ModernUI.WPF.1.0.9.nupkg" d:Source="File" Path="Packages\ModernUI.WPF.1.0.9.nupkg" d:VsixSubPath="Packages" />
        <Asset Type="MvvmLightLibsStd10.5.4.1.1.nupkg" d:Source="File" Path="Packages\MvvmLightLibsStd10.5.4.1.1.nupkg" d:VsixSubPath="Packages" />
        <Asset Type="Serilog.2.10.0.nupkg" d:Source="File" Path="Packages\Serilog.2.10.0.nupkg" d:VsixSubPath="Packages" />
        <Asset Type="System.ComponentModel.Annotations.5.0.0.nupkg" d:Source="File" Path="Packages\System.ComponentModel.Annotations.5.0.0.nupkg" d:VsixSubPath="Packages" />

You will replace the Type property value with the name of your nuget files and use the same name in the Path property value's path, after "Packages\".

Step 6: Build and Use
---------------------

Now you can build the VSIX project, by right-clicking it and selected Build or Rebuild. Then navigate to the bin folder of the project and either in Debug or Release (depending on how you built it) double-click on the VSIX file. After closing Visual Studio, the extension will install and the next time you run Visual Studio the template will be available to use when you go to create a new project.

If you ever add more templates to the extension project or update the existing one, go into the manifest file and increment the version number. Then rebuild and then double-click that VSIX file again. The new version will install.

References
----------

- https://blog.jayway.com/2015/03/13/visual-studio-how-to-create-a-solution-template-with-multiple-projects/
- https://docs.microsoft.com/en-us/nuget/visual-studio-extensibility/visual-studio-templates
- https://docs.microsoft.com/en-us/visualstudio/extensibility/how-to-use-wizards-with-project-templates?view=vs-2019
- https://medium.com/@alexandre.malavasi/how-to-create-project-templates-and-extension-for-visual-studio-2019-7fe0b6423b8b
- https://www.automatetheplanet.com/visual-studio-templates-installer/
- https://docs.microsoft.com/en-us/visualstudio/ide/template-parameters?view=vs-2019
- https://docs.microsoft.com/en-us/visualstudio/ide/how-to-create-multi-project-templates?view=vs-2019
