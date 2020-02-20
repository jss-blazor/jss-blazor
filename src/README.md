# 🔧 JSS Blazor SDK

![JSS Blazor](../assets/jss-blazor-banner.svg)

The JSS Blazor SDK contains everything you need to build a new website with JSS
Blazor. It consists of four packages:

1. [`JssBlazor.Core`][1] - core functionality shared by all other packages such as
   the Layout Service and general Blazor extensions.
2. [`JssBlazor.Components`][2] - field helper components to render Sitecore fields
   like [`<TextField />`][3], [`<Image />`][4], and [`<Placeholder />`][5]; a
   [`ComponentFactory` implementation][6]; Blazor component models; and Blazor
   component utilities.
3. [`JssBlazor.RenderingHost`][7] - everything needed to create a .NET Core
   Rendering Host to do server-side rendering of JSS Blazor apps in integrated
   mode. This is required for Experience Editor support.
4. [`JssBlazor.Tracking`][8] - models and services for tracking visitor behavior
   through the JSS Tracking API.

## 🚀 Getting Started

To create a new website with JSS Blazor, you will need to create at a minimum
two projects:

1. A Blazor WebAssembly project for your website's components with references to
   [`JssBlazor.Components`][2] and optionally [`JssBlazor.Tracking`][8].
2. A .NET Core web application project for the Rendering Host with a reference
   to [`JssBlazor.RenderingHost`][7] and the Blazor WebAssembly project for
   your website.

At the moment, the easiest way to start a new JSS Blazor website is to use the
[Styleguide solution][9] as a starter kit. See its [`README`][10] for steps on
getting started.

[1]: JssBlazor.Core/JssBlazor.Core.csproj
[2]: JssBlazor.Components/JssBlazor.Components.csproj
[3]: JssBlazor.Components/TextField.razor
[4]: JssBlazor.Components/Image.razor
[5]: JssBlazor.Components/Placeholder.razor
[6]: JssBlazor.Components/Services/DefaultComponentFactory.cs
[7]: JssBlazor.RenderingHost/JssBlazor.RenderingHost.csproj
[8]: JssBlazor.Tracking/JssBlazor.Tracking.csproj
[9]: ../samples/Styleguide/Styleguide.sln
[10]: ../samples/Styleguide/README.md
