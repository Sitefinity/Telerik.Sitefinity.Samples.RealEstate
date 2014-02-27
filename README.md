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

* Sitefinity 6.3 license

* .NET Framework 4

* Visual Studio 2012

* Microsoft SQL Server 2008R2 or later versions


### Prerequisites

You need to attach the database backeup files to your SQL Server. To do this:

1. In SQL Management Studio, open the context menu of _Databases_ and click _Attach..._
2. Click the _Add..._ button and navigate to the _SitefinityWebApp_ -> *App_Data* folder.
3. Select the **SitefinityRealEstateStarterKit.mdf** file and click _OK_.
4. Click _OK_.


### Installation instructions: SDK Samples from GitHub

1. Clone the [Telerik.Sitefinity.Samples.Dependencies](https://github.com/Sitefinty-SDK/Telerik.Sitefinity.Samples.Dependencies) repo to get all assemblies necessary to run for the samples.
2. Fix broken references in the class libraries, for example in **SitefinityWebApp** and **Telerik.Sitefinity.Samples.Common**:

  1. In Solution Explorer, open the context menu of your project node and click _Properties_.  
  
    The Project designer is displayed.
  2. Select the _Reference Paths_ tab page.
  3. Browse and select the folder where **Telerik.Sitefinity.Samples.Dependencies** folder is located.
  4. Click the _Add Folder_ button.


3. In Solution Explorer, navigate to _SitefinityWebApp_ -> *App_Data* -> _Sitefinity_ -> _Configuration_ and select the **DataConfig.config** file. 
4. Modify the **connectionString** value to match your server address.


### Integrate the OpenAccess enhancer


Sitefinity ships with OpenAccess ORM. To use OpenAccess in the data provider of a module, you must integrate the OpenAccess enhancer. To do this:


1. From the context menu of the project **Telerik.StarterKit.Modules.RealEstate**, click _Unload Project_.
2. From the context menu of the unloaded project, click _Edit Telerik.StarterKit.Modules.RealEstate.csproj_.
3. Find the **<ProjectExtensions>** tag and replace it with the following lines of code:
```xml
<ProjectExtensions>
  <VisualStudio>
    <UserProperties OpenAccess_EnhancementOutputLevel="1" OpenAccess_UpdateDatabase="False" OpenAccess_Enhancing="False"OpenAccess_ConnectionId="DatabaseConnection1" OpenAccess_ConfigFile="App.config" />
  </VisualStudio>
</ProjectExtensions>
<PropertyGroup>
  <OpenAccessPath>C:\GitHub\Telerik.Sitefinity.Samples.Dependencies</OpenAccessPath>
</PropertyGroup>
<Import Condition="Exists('$(OpenAccessPath)\OpenAccess.targets')" Project="$(OpenAccessPath)\OpenAccess.targets" />
```

4. In the **OpenAccessPath** element, place the path to the folder containing the **OpenAccess.targets** file. 

    This is the location of the **Telerik.Sitefinity.Samples.Dependencies** repository that you cloned locally. In the example above, the repository is cloned in **C:\GitHub**.
    
5. Save the changes.
6. From the context menu of the project, click _Reload Project_.
7. Repeat the above procedure for the **Telerik.StarterKit.Modules.Agents project**.
8. Build the solution.



### Login

To login to Sitefinity backend, use the following credentials: 

**Username:** admin

**Password:** password


### Additional resources

[Developer's Guide](http://www.sitefinity.com/documentation/documentationarticles/developers-guide)

[Integration with OpenAccess Enhancer](http://www.sitefinity.com/documentation/documentationarticles/developers-guide/sitefinity-essentials/modules/creating-custom-modules/creating-products-module/preparing-the-project/integrating-the-openaccess-enhancer)

