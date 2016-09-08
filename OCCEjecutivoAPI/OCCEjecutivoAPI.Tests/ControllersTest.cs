using System;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OCCEjecutivoAPI.Controllers;
using OCCEjecutivoAPI.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OCCEjecutivoAPI.Tests
{
    [TestClass]
    public class ControllersTest
    {
        [TestMethod]
        public void GetCandidate_ShouldBeThere_OrNot()
        {
            var controller = new CandidatesController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            var result = controller.GetCandidateData("1");

            Assert.IsNotNull(result);
            Candidate c;
            Assert.IsTrue(result.TryGetContentValue<Candidate>(out c));
            Assert.AreEqual("Juan", c.FirstName);

            //Sería así?? 
            result = controller.GetCandidateData("3");
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }

        [TestMethod]
        public void SaveCandidate_EnsureWorks_And_DataPersists()
        {
            Candidate candidate = new Candidate
            {
                id = "5",
                FirstName = "Eduardo",
                LastName_1 = "Perez",
                Email = "eduardo@perez.com",
                PhoneNumber = "(442) 123-4567"
            };

            var controller = new CandidatesController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            var result = controller.SaveCandidateData(candidate);
            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);

            result = controller.GetCandidateData("5");
            Assert.IsNotNull(result);
            Candidate c;
            Assert.IsTrue(result.TryGetContentValue<Candidate>(out c));
            Assert.AreEqual("Eduardo", c.FirstName);
        }

        [TestMethod]
        public void SaveCandidate_ValidateParams()
        {
            Candidate candidate = new Candidate
            {
                id = "5",
                FirstName = "Eduardo",
                LastName_1 = "Perez",
                Email = "eduardo@perez.com",
                PhoneNumber = "(442) 123-4567"
            };

            var controller = new CandidatesController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            var result = controller.SaveCandidateData(candidate, "6");
            Assert.AreEqual(HttpStatusCode.Conflict, result.StatusCode);

            candidate.id = null;
            result = controller.SaveCandidateData(candidate, "5");
            Assert.AreEqual(HttpStatusCode.Conflict, result.StatusCode);

            candidate.id = "5";
            candidate.Email = "";
            result = controller.SaveCandidateData(candidate, "5");
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);

            candidate.Email = "eduardo@perez.com";
            candidate.PhoneNumber = "";
            result = controller.SaveCandidateData(candidate);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);

            candidate.PhoneNumber = "(442) 123-4567";
            candidate.FirstName = null;
            result = controller.SaveCandidateData(candidate);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }


    }
}

