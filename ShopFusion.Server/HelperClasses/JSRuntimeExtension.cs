using Microsoft.JSInterop;

namespace ShopFusion.Server.HelperClasses
{
    public static class JSRuntimeExtension
    {
        public static async ValueTask ShowSuccessToastrNotification(this IJSRuntime _JSRuntime, string message) 
        {
            await _JSRuntime.InvokeVoidAsync("ShowToastrNotification", "success", message);
        }

        public static async ValueTask ShowFailureToastrNotification(this IJSRuntime _JSRuntime, string message)
        {
            await _JSRuntime.InvokeVoidAsync("ShowToastrNotification", "error", message);
        }
    }
}
