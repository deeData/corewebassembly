// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace WebApplication.WebAss.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\deidr\source\repos\CoreWebAssemblyArchit\CoreWebAssemblyArchit\WebApplication.WebAss\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\deidr\source\repos\CoreWebAssemblyArchit\CoreWebAssemblyArchit\WebApplication.WebAss\_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\deidr\source\repos\CoreWebAssemblyArchit\CoreWebAssemblyArchit\WebApplication.WebAss\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\deidr\source\repos\CoreWebAssemblyArchit\CoreWebAssemblyArchit\WebApplication.WebAss\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\deidr\source\repos\CoreWebAssemblyArchit\CoreWebAssemblyArchit\WebApplication.WebAss\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\deidr\source\repos\CoreWebAssemblyArchit\CoreWebAssemblyArchit\WebApplication.WebAss\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\deidr\source\repos\CoreWebAssemblyArchit\CoreWebAssemblyArchit\WebApplication.WebAss\_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\deidr\source\repos\CoreWebAssemblyArchit\CoreWebAssemblyArchit\WebApplication.WebAss\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\deidr\source\repos\CoreWebAssemblyArchit\CoreWebAssemblyArchit\WebApplication.WebAss\_Imports.razor"
using WebApplication.WebAss;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\deidr\source\repos\CoreWebAssemblyArchit\CoreWebAssemblyArchit\WebApplication.WebAss\_Imports.razor"
using WebApplication.WebAss.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "C:\Users\deidr\source\repos\CoreWebAssemblyArchit\CoreWebAssemblyArchit\WebApplication.WebAss\_Imports.razor"
using Core.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "C:\Users\deidr\source\repos\CoreWebAssemblyArchit\CoreWebAssemblyArchit\WebApplication.WebAss\_Imports.razor"
using MyApp.ApplicationLogic;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/")]
    public partial class ProjectsComponent : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 21 "C:\Users\deidr\source\repos\CoreWebAssemblyArchit\CoreWebAssemblyArchit\WebApplication.WebAss\Pages\ProjectsComponent.razor"
       
    IEnumerable<Project> projects;

    protected override async Task OnInitializedAsync()
    {
        projects = await ProjectsScreenUseCases.ViewProjectsAsync();
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IProjectsScreenUseCases ProjectsScreenUseCases { get; set; }
    }
}
#pragma warning restore 1591
