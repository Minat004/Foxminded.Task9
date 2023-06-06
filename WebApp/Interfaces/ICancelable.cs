using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace WebApp.Interfaces;

public interface ICancelable
{
    public void ViewDataReferer(ViewDataDictionary viewData, HttpRequest request);
}