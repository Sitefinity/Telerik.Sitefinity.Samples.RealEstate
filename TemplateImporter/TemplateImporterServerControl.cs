using System;
using System.Text;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;
using Telerik.Web.UI;
using System.Drawing;

namespace TemplateImporter
{

    public class TemplateImporterServerControl : SimpleView
    {
        protected override void InitializeControls(GenericContainer container)
        {
            
            this.RadUploadControl.AllowedFileExtensions = new string[] { ".zip" };
            this.RadUploadControl.ControlObjectsVisibility = ControlObjectsVisibility.None;
            this.RadUploadControl.OverwriteExistingFiles = true;
            this.RadUploadControl.TargetPhysicalFolder = AppDomain.CurrentDomain.BaseDirectory;
            this.RadUploadControl.MaxFileSize = 2147483647;

            this.ImportButton.Text = "Import template";
            this.ImportButton.Click += new EventHandler(UploadButton_Click);

            this.ImageControl.ImageUrl = this.Page.ClientScript.GetWebResourceUrl(typeof(TemplateImporterServerControl), "TemplateImporter.Web.Controls.importer_bg.jpg");
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            if (RadUploadControl.InvalidFiles.Count > 0)
            {
                StringBuilder response = new StringBuilder("<br/>There was an error while registering your template<br/>");

                UploadedFile file = RadUploadControl.InvalidFiles[0];
                if (file.ContentLength >= RadUploadControl.MaxFileSize)
                {
                    response.Append("Maximum file size: " + RadUploadControl.MaxFileSize.ToString() + " bytes");
                }
                else if (!RadUploadControl.AllowedFileExtensions.Contains(file.GetExtension()))
                {
                    response.Append("Allowed file extensions: " + string.Join(",", RadUploadControl.AllowedFileExtensions));
                }
                else
                {
                    response.Append("Your template could not be uploaded");
                }


                response.Append("<br/><br/>");

                ErrorLabel.Text = response.ToString();
                ErrorLabel.ForeColor = Color.Red;
            }

            else if (RadUploadControl.UploadedFiles.Count == 1)
            {
                string physicalPath = AppDomain.CurrentDomain.BaseDirectory;
                var templateteImporter = new TemplateImporter(
                    RadUploadControl.UploadedFiles[0].GetName(),
                    physicalPath
                );

                bool success = templateteImporter.Import();

                if (success)
                    ErrorLabel.Text = "Upload successful";
                else
                    ErrorLabel.Text = "An error occured while registering the template";

                ErrorLabel.ForeColor = Color.LightGreen;
            }
        }

        protected override string LayoutTemplateName
        {
            get
            {
                return null;
            }
        }

        public override string LayoutTemplatePath
        {
            get
            {
                var path = "~/SFTemplateImporter/" + "TemplateImporter.Web.Controls.TemplateImporterControl.ascx";
                return path;
            }
            set
            {
                base.LayoutTemplatePath = value;
            }
        }

        protected virtual RadUpload RadUploadControl
        {
            get
            {
                return base.Container.GetControl<RadUpload>("RadUploadControl", true);
            }
        }

        protected virtual RadButton ImportButton
        {
            get
            {
                return base.Container.GetControl<RadButton>("ImportButton", true);
            }
        }

        protected virtual Label ErrorLabel
        {
            get
            {
                return base.Container.GetControl<Label>("ErrorLabel", true);
            }
        }

        protected virtual System.Web.UI.WebControls.Image ImageControl
        {
            get
            {
                return base.Container.GetControl<System.Web.UI.WebControls.Image>("importerImage", true);
            }
        }

    }
}
