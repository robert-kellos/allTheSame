using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace AllTheSame.Entity.Model.Metadata.Tests
{
    [TestClass()]
    public class PersonMetadataTests
    {
        [TestMethod()]
        public void ValidateTest()
        {
            var p = new global::AllTheSame.Entity.Model.Person();
            var pMeta = new Metadata.PersonMetadata();

            var result = pMeta;

            Assert.IsNotNull(result);
        }
    }
}
