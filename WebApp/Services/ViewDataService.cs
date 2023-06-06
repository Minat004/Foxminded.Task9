using Azure.Core;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace WebApp.Services;

public class CancelService : ICancelable
{
    public void ViewDataReferer(ViewDataDictionary viewData, HttpRequest request)
    {
        if (!string.IsNullOrEmpty(request.Headers["Referer"].ToString()))
        {
            viewData["Referer"] = request.Headers["Referer"].ToString();
        }
    }
}