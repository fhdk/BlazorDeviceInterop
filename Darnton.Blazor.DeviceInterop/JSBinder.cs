using Microsoft.JSInterop;

namespace Darnton.Blazor.DeviceInterop
{
    internal class JsBinder
    {
        internal readonly IJSRuntime JsRuntime;
        private readonly string _importPath;
        private Task<IJSObjectReference> _module;

        public JsBinder(IJSRuntime jsRuntime, string importPath)
        {
            JsRuntime = jsRuntime;
            _importPath = importPath;
        }

        internal async Task<IJSObjectReference> GetModule()
        {
            return await (_module ??= JsRuntime.InvokeAsync<IJSObjectReference>("import", _importPath).AsTask());
        }

        public async ValueTask DisposeAsync()
        {
            if (_module != null)
            {
                var module = await _module;
                await module.DisposeAsync();
            }
        }
    }
}
