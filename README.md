Telerik.Sitefinity.Samples.RealEstate
=====================================

The Real Estate starter kit is developed for companies in the fields of real estate, tourism, and hospitality. Such companies want to effectively showcase their properties, keep information up to date, and present attractive offers. This ultimately results in more sales and profits. 

This starter kit provides companies with a head start on their project. In addition, the starter kit gives developers a template for adding custom features to Sitefinity. Some of the starter kit's features are:

* property listing module
* property search
* agent directory
* photo galleries
* maps


### Requirements

* Sitefinity license

* .NET Framework 4

* Visual Studio 2012

* Microsoft SQL Server 2008R2 or later versions


### Prerequisites

Clear the NuGet cache files. To do this:

1. In Windows Explorer, open the **%localappdata%\NuGet\Cache** folder.
2. Select all files and delete them.

You need to attach the database backup files to your SQL Server. To do this:

1. In SQL Management Studio, open the context menu of _Databases_ and click _Attach..._
2. Click the _Add..._ button and navigate to the _SitefinityWebApp_ -> *App_Data* folder.
3. Select the **SitefinityRealEstateStarterKit.mdf** file and click _OK_.
4. Click _OK_.


### Installation instructions: SDK Samples from GitHub



1. In Solution Explorer, navigate to _SitefinityWebApp_ -> *App_Data* -> _Sitefinity_ -> _Configuration_ and select the **DataConfig.config** file. 
2. Modify the **connectionString** value to match your server address.


The project refers to the following NuGet packages:

**SitefinityWebApp** library

* Telerik.Sitefinity.All.nupkg

**Telerik.Sitefinity.Samples.Common** library

* Telerik.Sitefinity.Core.nupkg

* Telerik.DataAccess.Core.nupkg

* Telerik.Sitefinity.Content.nupkg

**Telerik.StarterKit.Modules.Agents** library

* Telerik.Sitefinity.Core.nupkg

* Telerik.DataAccess.Fluent.nupkg
 
* Telerik.DataAccess.Core.nupkg

* Telerik.Sitefinity.Content.nupkg

* Telerik.Web.UI.nupkg

**Telerik.StarterKit.Modules.RealEstate** library

* Telerik.Sitefinity.Core.nupkg

* Telerik.DataAccess.Fluent.nupkg

* Telerik.DataAccess.Core.nupkg

* Telerik.Sitefinity.Content.nupkg

* Telerik.Web.UI.nupkg

**Telerik.StarterKit.Widgets.Events** library

* Telerik.Sitefinity.Core.nupkg

* Telerik.DataAccess.Core.nupkg

* Telerik.Sitefinity.Content.nupkg

* Telerik.Web.UI.nupkg

**Telerik.StarterKit.Widgets.Facebook** library

* Telerik.Sitefinity.Core.nupkg

* Telerik.Sitefinity.Content.nupkg

**Telerik.StarterKit.Widgets.News** library

* Telerik.DataAccess.Core.nupkg

* Telerik.Sitefinity.Core.nupkg

* Telerik.Sitefinity.Content.nupkg

* Telerik.Web.UI.nupkg

**Telerik.StarterKit.Widgets.Twitter** library

* Telerik.DataAccess.Core.nupkg

* Telerik.Sitefinity.Core.nupkg

* Telerik.Sitefinity.Content.nupkg

* Telerik.Web.UI.nupkg


**TemplateImporter** library

* Telerik.Sitefinity.Content.nupkg

* Telerik.Web.UI.nupkg

* Telerik.Sitefinity.Core.nupkg

* Telerik.DataAccess.Core.nupkg

You can find the packages in the official [Sitefinity Nuget Server](http://nuget.sitefinity.com).



### Login

To login to Sitefinity backend, use the following credentials: 

**Username:** admin

**Password:** password


### Additional resources

[Developer's Guide](http://www.sitefinity.com/documentation/documentationarticles/developers-guide)

[Integration with OpenAccess Enhancer](http://www.sitefinity.com/documentation/documentationarticles/developers-guide/sitefinity-essentials/modules/creating-custom-modules/creating-products-module/preparing-the-project/integrating-the-openaccess-enhancer)

