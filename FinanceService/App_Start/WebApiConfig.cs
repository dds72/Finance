using System.Web.Http;
using FinanceService.Models;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;

namespace FinanceService
{
  public static class WebApiConfig
  {
    public static void Register(HttpConfiguration config)
    {
      ODataModelBuilder builder = new ODataConventionModelBuilder();
      builder.EntitySet<Plan>("Plans");
      builder.EntitySet<TargetCategory>("TargetCategories");
      builder.EntitySet<Transaction>("Transactions");
      config.MapODataServiceRoute(
        routeName: "ODataRoute",
        routePrefix: null,
        model: builder.GetEdmModel());
    }
  }
}
