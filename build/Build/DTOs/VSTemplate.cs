using System.Collections.Generic;
using System.Xml.Serialization;

namespace Build.DTOs;

[XmlRoot("VSTemplate", Namespace = "http://schemas.microsoft.com/developer/vstemplate/2005")]
public class VSTemplate
{
    [XmlAttribute]
    public string Version { get; set; } = "3.0.0";
    [XmlAttribute]
    public string Type { get; set; } = "Project";

    public required TemplateData TemplateData { get; set; }
    public required TemplateContent TemplateContent { get; set; }

    [XmlElement("WizardExtension")]
    public WizardExtension? WizardExtension { get; set; }

    [XmlElement("WizardData")]
    public WizardData? WizardData { get; set; }
}

public class TemplateData
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public string ProjectType { get; set; } = "CSharp";
    public required string ProjectSubType { get; set; }
    public int SortOrder { get; set; } = 1000;
    public bool CreateNewFolder { get; set; } = true;
    public required string DefaultName { get; set; }
    public bool ProvideDefaultName { get; set; } = true;
    public string LocationField { get; set; } = "Enabled";
    public bool EnableLocationBrowseButton { get; set; } = true;
    public string Icon { get; set; } = "__TemplateIcon.ico";
}

public class TemplateContent
{
    public required Project Project { get; set; }
}

public class Project
{
    [XmlAttribute]
    public required string TargetFileName { get; set; }
    [XmlAttribute]
    public required string File { get; set; }
    [XmlAttribute]
    public bool ReplaceParameters { get; set; } = true;

    [XmlElement("Folder", typeof(Folder))]
    [XmlElement("ProjectItem", typeof(ProjectItem))]
    public List<object> Items { get; set; } = [];
}

public class Folder
{
    [XmlAttribute]
    public required string Name { get; set; }
    [XmlAttribute]
    public required string TargetFolderName { get; set; }

    [XmlElement("Folder", typeof(Folder))]
    [XmlElement("ProjectItem", typeof(ProjectItem))]
    public List<object> Items { get; set; } = [];
}

public class ProjectItem
{
    [XmlAttribute]
    public bool ReplaceParameters { get; set; } = true;
    [XmlAttribute]
    public required string TargetFileName { get; set; }
    [XmlText]
    public required string Value { get; set; }
}

public class WizardExtension
{
    public required string Assembly { get; set; }
    public required string FullClassName { get; set; }
}

public class WizardData
{
    [XmlElement("packages")]
    public Packages? Packages { get; set; }
}

public class Packages
{
    [XmlAttribute]
    public required string repository { get; set; }
    [XmlAttribute]
    public required string repositoryId { get; set; }
    [XmlElement("package")]
    public List<Package> PackageList { get; set; } = [];
}

public class Package
{
    [XmlAttribute]
    public required string id { get; set; }
    [XmlAttribute]
    public required string version { get; set; }
}
