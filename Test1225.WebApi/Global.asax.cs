using System.Configuration;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using MetaShare.Common.Core.Daos;
using MetaShare.Common.Core.Daos.SqlServer;
using Test1225.Daos;
using Test1225.Services;
namespace Test1225.WebApi
{
 
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            string connectionString = ConfigurationManager.ConnectionStrings["Test1225"].ConnectionString;
DaoFactory.Instance.ConnectionStringBuilder = new ConnectionStringBuilder(connectionString, typeof(SqlContext)){SqlDialectType = typeof(SqlServerDialect), SqlDialectVersionType = typeof(SqlServerDialectVersion)};

            RegisterDaos.RegisterAll(DaoFactory.Instance.ConnectionStringBuilder.SqlDialectType, DaoFactory.Instance.ConnectionStringBuilder.SqlDialectVersionType);
            RegisterServices.RegisterAll();

            //var format = GlobalConfiguration.Configuration.Formatters;
            //format.XmlFormatter.SupportedMediaTypes.Clear();
        }
    }
}