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
   `PublishSettings.Sitecore.targets.user` next to
   [`PublishSettings.Sitecore.targets`][5] and set the `publishUrl` property to
   your website's install path (note that `*.user` files are intentionally
   ignored by Git so your custom settings will not be checked in).
5. Open and build the [`Styleguide.sln`][1] solution.
   - This automatically deploys the solution to
     `C:/inetpub/wwwroot/styleguide.sitecore` through [Helix Publishing
     Pipeline][6].
6. Sync Unicorn at <http://styleguide.sitecore/unicorn.aspx>.
7. Launch the JSS Blazor Rendering Host from the [`JssBlazor.sln`][7] solution.
8. Navigate to the Styleguide at <http://styleguide.sitecore> and the
   application from the [`JssBlazor.Project.Styleguide.Client`][8] project
   should be server-side rendered.

[1]: Styleguide.sln
[2]: src/Project/Common/sitecore/App_Config/Environment/JssBlazor/JssBlazor.Project.Common.Dev.config
[3]: src/Project/Styleguide/sitecore/App_Config/Environment/JssBlazor/JssBlazor.Project.Styleguide.Dev.config
[4]: src/Project/Styleguide/rendering/Properties/launchSettings.json
[5]: build/PublishSettings.Sitecore.targets
[6]: https://github.com/richardszalay/helix-publishing-pipeline
[7]: ../../src/JssBlazor.sln
[8]: src/Project/Styleguide/client/JssBlazor.Project.Styleguide.Client.csproj
