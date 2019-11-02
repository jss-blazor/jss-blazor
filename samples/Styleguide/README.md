# JSS Blazor Styleguide

This is a sample application to show off JSS Blazor in a Helix solution.

## Setup

1. Install Sitecore 9.2.
   - Configure the install path as `C:/inetpub/wwwroot/styleguide.sitecore`.
   - Configure the host name as `styleguide.sitecore`.
2. Install the JSS 12 package in Sitecore.
3. Open the [`Styleguide.sln`][1] solution and modify the following
   files in the `.config` folder:
   - [`JssBlazor.Project.Common.Dev.config`][2]
     - Change `sourceFolder` to the root path of `Styleguide` solution.
   - [`JssBlazor.Project.Styleguide.Dev.config`][3]
     - Change the `serverSideRenderingEngineEndpointUrl` to the IIS Express URL
       of the JSS Blazor Rendering Host app found in [`launchSettings.json`][4].
4. If you use an install path different than the one in step 1, create
   `PublishSettings.Sitecore.targets.user` next to the following two publish
   settings and set the `publishUrl` property to your website's install path
   (note that `*.user` files are intentionally ignored by Git so your custom
   settings will not be tracked by Git):
   1. Styleguide [`PublishSettings.Sitecore.targets`][5]
   2. JSS Blazor [`PublishSettings.Sitecore.targets`][6]
5. Publish the [`JssBlazor.LayoutService`][7] project in the
   [`JssBlazor.sln`][8] solution.
6. Open and build the [`Styleguide.sln`][1] solution.
   - This automatically deploys the solution to
     `C:/inetpub/wwwroot/styleguide.sitecore` through [Helix Publishing
     Pipeline][9].
7. Sync Unicorn at <http://styleguide.sitecore/unicorn.aspx>.
8. Launch the JSS Blazor Rendering Host from the [`JssBlazor.sln`][8] solution.
9. Navigate to the Styleguide at <http://styleguide.sitecore> and the
   application from the [`JssBlazor.Styleguide`][10] project should be
   server-side rendered.

[1]: Styleguide.sln
[2]: src/Project/Common/sitecore/App_Config/Environment/JssBlazor/JssBlazor.Project.Common.Dev.config
[3]: src/Project/Styleguide/sitecore/App_Config/Environment/JssBlazor/JssBlazor.Project.Styleguide.Dev.config
[4]: src/Project/Styleguide/rendering/Properties/launchSettings.json
[5]: build/PublishSettings.Sitecore.targets
[6]: ../../src/build/PublishSettings.Sitecore.targets
[7]: ../../src/JssBlazor.LayoutService/JssBlazor.LayoutService.csproj
[8]: ../../src/JssBlazor.sln
[9]: https://github.com/richardszalay/helix-publishing-pipeline
[10]: ../../src/JssBlazor.Styleguide/JssBlazor.Styleguide.csproj
