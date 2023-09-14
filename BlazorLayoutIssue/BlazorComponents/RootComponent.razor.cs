using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponents
{
    public partial class RootComponent : ComponentBase, IDisposable
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        internal List<ComponentBase> InitializedComponents { get; set; } = new List<ComponentBase>();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
        }

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            return base.OnAfterRenderAsync(firstRender);
        }

        public void Dispose()
        {
        }
    }
}
