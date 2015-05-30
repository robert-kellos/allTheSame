using System.Configuration;
using Microsoft.Owin.Hosting;
using TechTalk.SpecFlow;

namespace AllTheSame.WebAPI.Test.AcceptanceTests
{
    [Binding]
    public static class TestSetup
    {
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            var url = ConfigurationManager.AppSettings["WebAPI.Test.ServerBaseURL"];
            WebApp.Start<Startup>(url);
        }
    }
}