using System;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OCCEjecutivoAPI.Controllers;
using OCCEjecutivoAPI.Models;

namespace OCCEjecutivoAPI.Tests
{
    [TestClass]
    public class ControllersTest
    {
        [TestMethod]
        public void GetCandidate_ShouldBeThere()
        {
            var controller = new CandidatesController();
            var result = controller.GetCandidateData("2") as OkNegotiatedContentResult<Candidate>;

            Assert.IsNotNull(result);
            Assert.AreEqual("Gonzalo", result.Content.FirstName);


            result = controller.GetCandidateData("3") as OkNegotiatedContentResult<Candidate>;
            Assert.IsNull(result);
        }

    }
}

