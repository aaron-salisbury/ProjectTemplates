using System.Collections.Generic;
using System.Xml.Serialization;

namespace Build.DTOs;

[XmlRoot("VSTemplate", Namespace = "http://schemas.microsoft.com/developer/vstemplate/2005")]
public record VSTemplate
{
    [XmlAttribute]
    public string? Version { get; set; }
    [XmlAttribute]
    public string? Type { get; set; }

    public required TemplateData TemplateData { get; set; }
    public required TemplateContent TemplateContent { get; set; }

    [XmlElement("WizardExtension")]
    public WizardExtension? WizardExtension { get; set; }

    [XmlElement("WizardData")]
    public WizardData? WizardData { get; set; }
}

[XmlType("TemplateData")]
public record TemplateData
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public string? ProjectType { get; set; }
    public required string ProjectSubType { get; set; }
    public int SortOrder { get; set; } = 1000;
    public bool CreateNewFolder { get; set; } = true;
    public required string DefaultName { get; set; }
    public bool ProvideDefaultName { get; set; } = true;
    public string? LocationField { get; set; }
    public bool EnableLocationBrowseButton { get; set; } = true;
    public string? Icon { get; set; }
    public string? LanguageTag { get; set; }
    public string? PlatformTag { get; set; }
    public string? ProjectTypeTag { get; set; }
}

[XmlType("TemplateContent")]
public record TemplateContent
{
    public Project? Project { get; set; }
    public ProjectCollection? ProjectCollection { get; set; }
}

[XmlType("Project")]
public record Project
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

[XmlType("Folder")]
public record Folder
{
    [XmlAttribute]
    public required string Name { get; set; }
    [XmlAttribute]
    public required string TargetFolderName { get; set; }

    [XmlElement("Folder", typeof(Folder))]
    [XmlElement("ProjectItem", typeof(ProjectItem))]
    public List<object> Items { get; set; } = [];
}

[XmlType("ProjectItem")]
public record ProjectItem
{
    [XmlAttribute]
    public bool ReplaceParameters { get; set; } = true;
    [XmlAttribute]
    public required string TargetFileName { get; set; }
    [XmlText]
    public required string Value { get; set; }
}

[XmlType("WizardExtension")]
public record WizardExtension
{
    public required string Assembly { get; set; }
    public required string FullClassName { get; set; }
}

[XmlType("WizardData")]
public record WizardData
{
    [XmlElement("packages")]
    public Packages? Packages { get; set; }
}

[XmlType("Packages")]
public record Packages
{
    [XmlAttribute]
    public required string repository { get; set; }
    [XmlAttribute]
    public required string repositoryId { get; set; }
    [XmlElement("package")]
    public List<Package> PackageList { get; set; } = [];
}

[XmlType("Package")]
public record Package
{
    [XmlAttribute]
    public required string id { get; set; }
    [XmlAttribute]
    public required string version { get; set; }
}

[XmlType("ProjectCollection")]
public record ProjectCollection
{
    [XmlElement("ProjectTemplateLink")]
    public List<ProjectTemplateLink>? ProjectTemplateLinks { get; set; }
}

[XmlType("ProjectTemplateLink")]
public record ProjectTemplateLink
{
    [XmlAttribute("ProjectName")]
    public string? ProjectName { get; set; }

    [XmlAttribute("CopyParameters")]
    public bool CopyParameters { get; set; }

    [XmlText]
    public string? Value { get; set; }
}
