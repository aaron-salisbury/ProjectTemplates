using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PostProcessingWizard;

public class Wizard : IWizard
{
    public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
    {
        //TODO: Group using statements that start with $ext_safeprojectname$ together at the bottom of the usings block.

        //TODO: The Product element of SDK-style executable projects should be a friendly version of $ext_safeprojectname$

        //TODO: If not a SDK-style project and user selects to create solution in its own folder, update package hint paths to step up (..\).

        //// Get the project name from the replacements dictionary
        //if (replacementsDictionary.TryGetValue("$safeprojectname$", out var safeProjectName))
        //{
        //    // Prettify: insert spaces before capital letters
        //    string prettyName = Regex.Replace(safeProjectName, "(\\B[A-Z])", " $1");
        //    replacementsDictionary["$prettyprojectname$"] = prettyName;
        //}
    }

    public void ProjectFinishedGenerating(Project project) { }
    public void ProjectItemFinishedGenerating(ProjectItem projectItem) { }
    public void BeforeOpeningFile(ProjectItem projectItem) { }
    public void RunFinished() { }
    public bool ShouldAddProjectItem(string filePath) => true;
}

//TODO: 
//Register the Wizard in Your Template
//•	Build your wizard class into a DLL.
//•	Place the DLL in a location accessible to Visual Studio (e.g., in your VSIX or a known path).

//In the root vstemplate file, add a <WizardExtension> element:
//<WizardExtension>
//  <Assembly>PostProcessingWizard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null</Assembly>
//  <FullClassName>Wizard</FullClassName>
//</WizardExtension>

//Use the Custom Parameter in Your Template
//<Product>$prettyprojectname$</Product>

//Build and Deploy
//•	Ensure your wizard assembly is included in your VSIX or extension package.
//•	When the user creates a project from your template, the wizard will run, generate the prettified name, and substitute $prettyprojectname$ in the template files.