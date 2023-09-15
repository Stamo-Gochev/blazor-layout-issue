# blazor-layout-issue

Link to the public bug report:
https://github.com/dotnet/aspnetcore/issues/50724

### Describe the bug

When a Blazor component is used in `MainLayout.razor` in a Blazor app with [Blazor Server interactive render mode](https://learn.microsoft.com/en-au/aspnet/core/blazor/components/render-modes?view=aspnetcore-8.0#enable-support-for-interactive-render-modes), passing this component as a cascading parameter results in the component having the value `null`.

### Details

> **Note:** The explanations below are extracted from the code we are using, which is proprietary. This is why the scenario is a bit simplified and might look like the functionality can be achieved in a different way, but please focus on how a component passed as a cascading parameter in `MainLayout.razor` becomes null - this is the main issue we are facing. I am not able to directly show the real code as it is licensed.

The Blazor app renders a [RootComponent](https://github.com/Stamo-Gochev/blazor-layout-issue/blob/master/BlazorLayoutIssue/BlazorComponents/RootComponent.razor), which basically re-renders its child content. The component wraps the `@Body` element inside [MainLayout.razor](https://github.com/Stamo-Gochev/blazor-layout-issue/blob/master/BlazorLayoutIssue/BlazorAppNet8RC1/Components/Layout/MainLayout.razor#L18-L20) and it is used together with a [ContentComponent](https://github.com/Stamo-Gochev/blazor-layout-issue/blob/master/BlazorLayoutIssue/BlazorComponents/ContentComponent.razor.cs#L18-L29) that appears on the [Home.razor](https://github.com/Stamo-Gochev/blazor-layout-issue/blob/master/BlazorLayoutIssue/BlazorAppNet8RC1/Components/Pages/Home.razor#L11-L17) page.

When the app is run, the first time the `ContentComponent` is initialized, the `RootComponent` has its correct value, but on consecutive calls it becomes `null`.


![blazor-layout-issue](https://github.com/dotnet/aspnetcore/assets/1857705/3ca8731d-7d70-40a9-ae7f-0dc5ea2035ff)


### Expected Behavior

The component that is passed down the hierarchy as a cascading parameter should **not** be null.

> **Note 1:** The problem is not reproducible when exactly the same configuration is used in a .NET 6.0 project, so I suppose this is related to the new interactive modes and how they work. Please advise if some configuration is incorrect, but for now, this seems like a regression to us.

> **Note 2:** We do not want to multi-target in order to "workaround" this in some why (if you can suggest a workaround) as we want to avoid this following the suggestion in https://learn.microsoft.com/en-au/aspnet/core/blazor/components/render-modes?view=aspnetcore-8.0#apply-a-render-mode-to-a-component-definition - _"Component authors should avoid coupling a component's implementation to a specific render mode"_

> **Note 3:** The code works if the `RootComponent` is moved to wrap the whole [Home.razor](https://github.com/Stamo-Gochev/blazor-layout-issue/blob/master/BlazorLayoutIssue/BlazorAppNet8RC1/Components/Pages/Home.razor) page, but this is **not** an option for us - we want to keep the compatibility of the configuration in [MainLayout.razor](https://github.com/Stamo-Gochev/blazor-layout-issue/blob/master/BlazorLayoutIssue/BlazorAppNet8RC1/Components/Layout/MainLayout.razor#L18-L20). 

### Steps To Reproduce

1. Clone https://github.com/Stamo-Gochev/blazor-layout-issue
2. Run the app and open the [Home.razor](https://github.com/Stamo-Gochev/blazor-layout-issue/blob/master/BlazorLayoutIssue/BlazorAppNet8RC1/Components/Pages/Home.razor) page
3. Observe the log for the error or set a breakpoint in [ContentComponent](https://github.com/Stamo-Gochev/blazor-layout-issue/blob/master/BlazorLayoutIssue/BlazorComponents/ContentComponent.razor.cs#L20-L30) and see the Debug window when the `RootComponent` value becomes `null` or see the attached [GIF](https://github.com/dotnet/aspnetcore/assets/1857705/3ca8731d-7d70-40a9-ae7f-0dc5ea2035ff)

### Exceptions (if any)

The cascading value is `null`.

### .NET Version

8.0.100-rc.1.23455.8

### Anything else?

```
$ dotnet --info
.NET SDK:
 Version:   8.0.100-rc.1.23455.8
 Commit:    e14caf947f

Runtime Environment:
 OS Name:     Windows
 OS Version:  10.0.19044
 OS Platform: Windows
 RID:         win-x64
 Base Path:   C:\Program Files\dotnet\sdk\8.0.100-rc.1.23455.8\

.NET workloads installed:
There are no installed workloads to display.

Host:
  Version:      8.0.0-rc.1.23419.4
  Architecture: x64
  Commit:       92959931a3
  RID:          win-x64

.NET SDKs installed:
  8.0.100-rc.1.23455.8 [C:\Program Files\dotnet\sdk]
```
