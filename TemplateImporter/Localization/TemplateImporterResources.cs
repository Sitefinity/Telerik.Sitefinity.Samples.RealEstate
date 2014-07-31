using Telerik.Sitefinity.Localization;

namespace TemplateImporter.Localization
{
    [ObjectInfo(typeof(TemplateImporterResources), Title = "TemplateImporterResourcesTitle", Description = "TemplateImporterResourcesDescription")]
    public class TemplateImporterResources : Resource
    {
        public TemplateImporterResources()
        { 
        }

        [ResourceEntry("TemplateImporterViewTitle",
                Value = "TemplateImporter",
                Description = "The title of the TemplateImporter module.",
                LastModified = "2011/05/09")]
        public string TemplateImporterViewTitle
        {
            get
            {
                return this["TemplateImporterViewTitle"];
            }
        }

        [ResourceEntry("TemplateImporterResourcesTitle",
                        Value = "TemplateImporterResources",
                        Description = "The title of this class.",
                        LastModified = "2009/04/30")]
        public string TemplateImporterResourcesTitle
        {
            get
            {
                return this["TemplateImporterResourcesTitle"];
            }
        }

        [ResourceEntry("TemplateImporterResourcesDescription",
                        Value = "Contains localizable resources for TemplateImporter module labels.",
                        Description = "The description of this class.",
                        LastModified = "2009/04/30")]
        public string TemplateImporterResourcesDescription
        {
            get
            {
                return this["TemplateImporterResourcesDescription"];
            }
        }

        [ResourceEntry("TemplateImporterResourcesTitlePlural",
            Value = "TemplateImporterResources",
            Description = "The title plural of this class.",
            LastModified = "2009/04/30")]
        public string TemplateImporterResourcesTitlePlural
        {
            get
            {
                return this["TemplateImporterResourcesTitlePlural"];
            }
        }

        [ResourceEntry("TemplateItemApplicationsUploadTitle",
            Value = "TemplateItem Application Form",
            Description = "The title of the TemplateItemApplicationUploadWidget.",
            LastModified = "2010/04/30")]
        public string TemplateItemApplicationsUploadTitle
        {
            get
            {
                return this["TemplateItemApplicationsUploadTitle"];
            }
        }

        [ResourceEntry("TemplateItemApplicationsUploadDescription",
            Value = "A widget for TemplateItem application submissions.",
            Description = "The description of the TemplateItemApplicationUploadWidget.",
            LastModified = "2010/04/30")]
        public string TemplateItemApplicationsUploadDescription
        {
            get
            {
                return this["TemplateItemApplicationsUploadDescription"];
            }
        }
    }
}