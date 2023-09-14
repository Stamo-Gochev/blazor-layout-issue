using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponents
{
    public partial class ContentComponent : ComponentBase, IDisposable
    {
        [CascadingParameter]
        protected RootComponent RootComponent { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (RootComponent == null)
            {
                throw new Exception("Root component is not available");
            }
            else
            {
                RootComponent.InitializedComponents.Add(this);
            }

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
