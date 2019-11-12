# 🎨 JSS Blazor Styleguide

![JSS Blazor](../../assets/jss-blazor-banner.svg)

This is a sample application to showcase JSS Blazor in a [Helix solution][1].

## 🚀 Getting Started

### 🌐 Install Sitecore

1. Install a new instance of [Sitecore 9.2][2].
   - Configure the install path as `C:/inetpub/wwwroot/styleguide.sitecore`.
   - Configure the host name as `styleguide.sitecore`.
2. Install the [JavaScript Services 12][3] package in Sitecore.

### 📺 Create Rendering Host

JSS Blazor requires a new service called the Rendering Host for server-side
rendering. The Rendering Host is required for Experience Editor support.

1. Add a new website in IIS.
   - Configure the `Site Name` as `styleguide.renderinghost`.
   - Configure the `Physical path` as
     `C:\inetpub\wwwroot\styleguide.renderinghost` (create the folder on disk
     first).
   - The `Site Name` and `Physical path` should match the naming conventions of
     the corresponding Sitecore site (`styleguide.sitecore` in this case).
2. Add an entry to `C:\Windows\System32\drivers\etc\hosts` for the site.
   - e.g., `127.0.0.1 styleguide.renderinghost`
3. If you use a physical path different than
   `C:\inetpub\wwwroot\styleguide.renderinghost`, create a copy of
   [`PublishSettings.RenderingHost.targets`][4] called
   `PublishSettings.RenderingHost.targets.user` and set the `publishUrl`
   property to your Rendering Host's physical path.
4. Publish the [`JssBlazor.Project.Styleguide.RenderingHost`][5] project in
   [`Styleguide.sln`][6].

### 🌐 Configure Sitecore

1. If you use a physical path different than
   `C:\inetpub\wwwroot\styleguide.sitecore`, create copies of the following
   two files and set the `publishUrl` property to your website's install path:
   1. Copy [`PublishSettings.Sitecore.targets`][7] to
      `PublishSettings.Sitecore.Targets.user`.
   2. Copy [`PublishSettings.Sitecore.Client.targets`][8] to
      `PublishSettings.Sitecore.Client.Targets.user`.
2. Open [`Styleguide.sln`][6] solution and modify the following files in the
   `.config` folder:
   - [`JssBlazor.Project.Common.Dev.config`][9]
     - Change `sourceFolder` to the root path of `Styleguide` solution.
   - [`JssBlazor.Project.Styleguide.Dev.config`][10]
     - Change the `hostName` to the URL of your Sitecore instance if different
       from `styleguide.sitecore`.
     - Change the `serverSideRenderingEngineEndpointUrl` to the URL of your
       Rendering Host service if different than the default.
3. Build [`Styleguide.sln`][6].
   - This automatically deploys the solution to
     `C:/inetpub/wwwroot/styleguide.sitecore` through [Helix Publishing
     Pipeline][11].
4. Publish the [`JssBlazor.Project.Styleguide.Client`][12] project.
   - [Helix Publishing Pipeline][11] does not automatically deploy .NET Core
     projects on build.
5. Sync Unicorn at <http://styleguide.sitecore/unicorn.aspx>.
6. Navigate to the Styleguide at <http://styleguide.sitecore> and the
   Styleguide should load.

### 🔗 One-click Publish

The [JssBlazor.Project.Styleguide.Client][12] project has to be deployed to both
the Rendering Host and Sitecore when it is changed. Instead of publishing the
project separately to the Rendering Host and then to Sitecore, you can create
a symbolic link between the two so you only need to publish to the Rendering
Host. Below is a sample script to create the symbolic link:

```powershell
$sitecoreWebRoot = "C:\inetpub\wwwroot\styleguide.sitecore"
$renderingHostWebRoot = "C:\inetpub\wwwroot\styleguide.renderinghost"
$styleguideBundlePath = "\JssBlazor.Project.Styleguide.Client\dist\_framework"
$fullBundlePath = Join-Path $renderingHostWebRoot $styleguideBundlePath
New-Item -ItemType SymbolicLink -Path $sitecoreWebRoot -Name _framework -Value $fullBundlePath
```

Now any time changes are made to [`JssBlazor.Project.Styleguide.Client`][12],
simply publish [`JssBlazor.Project.Styleguide.RenderingHost`][5] and the
Styleguide Blazor app will be published to both locations.

🚨 **Note:** The symbolic link only handles changes to _code_ in the
[`JssBlazor.Project.Styleguide.Client`][12] project. You must publish the
[`JssBlazor.Project.Styleguide.Client`][12] project to Sitecore for changes to
static assets in `wwwroot` to be deployed to Sitecore.

[1]: https://helix.sitecore.net/
[2]: https://dev.sitecore.net/Downloads/Sitecore_Experience_Platform/92/Sitecore_Experience_Platform_92_Initial_Release.aspx
[3]: https://dev.sitecore.net/Downloads/Sitecore_JavaScript_Services/120/Sitecore_JavaScript_Services_1200.aspx
[4]: build/PublishSettings.RenderingHost.targets
[5]: src/Project/Styleguide/rendering/JssBlazor.Project.Styleguide.RenderingHost.csproj
[6]: Styleguide.sln
[7]: build/PublishSettings.Sitecore.targets
[8]: build/PublishSettings.Sitecore.Client.targets
[9]: src/Project/Common/sitecore/App_Config/Environment/JssBlazor/JssBlazor.Project.Common.Dev.config
[10]: src/Project/Styleguide/sitecore/App_Config/Environment/JssBlazor/JssBlazor.Project.Styleguide.Dev.config
[11]: https://github.com/richardszalay/helix-publishing-pipeline
[12]: src/Project/Styleguide/client/JssBlazor.Project.Styleguide.Client.csproj
