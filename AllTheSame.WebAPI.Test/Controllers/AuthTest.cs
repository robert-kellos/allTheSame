using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Accushield.WebAPI.Test.Controllers
{
    [TestClass]
    public class Auth : BaseServiceTest
    {
        public override string Uri
        {
            get { return "/Token"; }
        }

        public void GetTokenValid()
        {
        }
    }
}