# JSS Blazor Style Guide

This is a sample application to show off JSS Blazor in a Helix solution.

## Setup

1. Install the MVP preview of Sitecore 9.2.
   - Configure the install path as `C:/inetpub/wwwroot/styleguide.sitecore`.
   - Configure the host name as `styleguide.sitecore`.
2. Install the JSS 12 MVP preview package in Sitecore.
3. Open the [`StyleGuide.sln`][1] solution and modify the following
   files in the `.config` folder:
   - [`JssBlazor.Project.Common.Dev.config`][2]
     - Change `sourceFolder` to the root path of `StyleGuide` solution.
   - [`JssBlazor.Project.StyleGuide.Dev.config`][3]
     - Change the `serverSideRenderingEngineEndpointUrl` to the IIS Express URL
       of the JSS Blazor Rendering Host app found in [`launchSettings.json`][4].
   - If you have a different installation path you must add a `PublishSettings.Sitecore.targets.user` and add the location of the root folder to the `publishUrl` property.
4. Open and build the solution.
   - This automatically deploys the solution to
     `C:/inetpub/wwwroot/styleguide.sitecore` through [Helix Publishing
     Pipeline][5].
5. Sync Unicorn at <http://styleguide.sitecore/unicorn.aspx>.
6. Launch the JSS Blazor Rendering Host from the [`JssBlazor.sln`][6] solution.
7. Navigate to the Style Guide at <http://styleguide.sitecore> and the
   application from the [`JssBlazor.Client`][7] project should be server-side
   rendered.

[1]: StyleGuide.sln
[2]: src/Project/Common/sitecore/App_Config/Environment/JssBlazor/JssBlazor.Project.Common.Dev.config
[3]: src/Project/StyleGuide/sitecore/App_Config/Environment/JssBlazor/JssBlazor.Project.StyleGuide.Dev.config
[4]: ../../src/JssBlazor.RenderingHost/Properties/launchSettings.json
[5]: https://github.com/richardszalay/helix-publishing-pipeline
[6]: ../../src/JssBlazor.sln
[7]: ../../src/JssBlazor.Client/JssBlazor.Client.csproj
