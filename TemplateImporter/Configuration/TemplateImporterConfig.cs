using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.GenericContent.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;


namespace TemplateImporter.Configuration
{
    public class TemplateImporterConfig : ContentModuleConfigBase
    {
        protected override void InitializeDefaultProviders(ConfigElementDictionary<string, DataProviderSettings> providers)
        {
            //providers.Add(new DataProviderSettings(this.Providers)
            //{
            //    Name = "OpenAccessDataProvider",
            //    Description = "A provider that stores TemplateImporter data in a database using OpenAccess ORM."

            //});
        }

        protected override void InitializeDefaultViews(ConfigElementDictionary<string, ContentViewControlElement> contentViewControls)
        {
            //contentViewControls.Add(TemplateImporterDefinitions.DefineTemplateImporterBackendContentView(contentViewControls));
        }
    }
}