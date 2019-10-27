# JSS Blazor SDK

The JSS Blazor SDK consists of four different packages:

1. `JssBlazor.Core` - core functionality shared by all other packages such as
   Layout Service models, the Component Factory, and general Blazor extensions.
2. `JssBlazor.Components` - field helper components to render Sitecore fields
   like `<TextField />`, `<Image />`, and `<Placeholder />`; Blazor component
   models; and Blazor component utilities.
3. `JssBlazor.Tracking` - models and services for tracking visitor behavior
   through the JSS Tracking API.
4. `JssBlazor.RenderingHost` - everything needed to create a .NET Core
   Rendering Host to do server-side rendering of JSS Blazor apps in integrated
   mode. This is required for Experience Editor support.

## Prerequisites

All of the following must be installed on your machine to build and work with
the JSS Blazor SDK:

1. The latest [.NET Core 3.0 SDK][1].
2. The latest [.NET Core 3.0 Runtime & Hosting Bundle][1].
3. The latest [Visual Studio 2019 Preview][2].

## Rendering Host Installation

The Rendering Host can be run with IIS Express or hosted on a web server. For
ease of publishing and faster development, it's recommended to run the
Rendering Host in IIS as follows:

1. Create a new site in IIS called `styleguide.renderinghost` with
   `C:\inetpub\wwwroot\styleguide.renderinghost` as the physical path.
   - The name and path should match the naming conventions of the Sitecore site
     the Rendering Host is for (`styleguide.sitecore` in this case).
2. Add a mapping to `C:\Windows\System32\drivers\etc\hosts` for the site.
   - e.g., `127.0.0.1 styleguide.renderinghost`
3. If you use a physical path different than the one in step 1, create
   `PublishSettings.RenderingHost.targets.user` next to
   [`PublishSettings.RenderingHost.targets`][3] and set the `publishUrl`
   property to your Rendering Host's physical path.
4. Publish the [`JssBlazor.Styleguide.RenderingHost`][4] project in
   [`Styleguide.sln`][5].
5. Set up a symbolic link in the Sitecore instance webroot (e.g.,
   `C:\inetpub\wwwroot\styleguide.sitecore`) that points to the
   `JssBlazor.Styleguide\dist\_framework` folder in the Rendering Host's
   webroot*. This is a sample PowerShell script to do that:
   ```powershell
   cd C:\inetpub\wwwroot\styleguide.sitecore
   New-Item -ItemType SymbolicLink -Path . -Name _framework -Value C:\inetpub\wwwroot\styleguide.renderinghost\JssBlazor.Styleguide\dist\_framework
   ```

*The Styleguide's Blazor bundle needs to be deployed to both Sitecore and the
Rendering Host to work. This symbolic link is a sloppy way to do it for now.

## Sitecore Setup

Configure the `serverSideRenderingEngineEndpointUrl` for your JSS app to point
to the Rendering Host. See [JssBlazor.Project.Styleguide.Dev.config][6] in the
Styleguide for an example.

[1]: https://dotnet.microsoft.com/download/dotnet-core/3.0
[2]: https://visualstudio.microsoft.com/vs/preview/
[3]: ../samples/Styleguide/PublishSettings.RenderingHost.targets
[4]: ../samples/Styleguide/src/Project/Styleguide/rendering/JssBlazor.Styleguide.RenderingHost.csproj
[5]: ../samples/Styleguide/Styleguide.sln
[6]: ../samples/Styleguide/src/Project/Styleguide/sitecore/App_Config/Environment/JssBlazor/JssBlazor.Project.Styleguide.Dev.config
