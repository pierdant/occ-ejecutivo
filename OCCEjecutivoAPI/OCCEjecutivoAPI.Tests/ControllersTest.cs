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
        string _inserted_id;

        /// <summary>
        /// Full CRUD tests... First we need to test Insert / Update, then we check exists and then we delete it
        /// </summary>
        [TestMethod]
        public void SaveCandidate_EnsureWorks_And_DataPersists()
        {

            Candidate candidate = new Candidate
            {
                FirstName = "Eduardo",
                LastName_1 = "Perez",
                Email = "eduardo@perez.com",
                PhoneNumber = "(442) 123-4567"
            };

            var controller = new CandidatesController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            //insert
            var result = controller.SaveCandidateData(candidate);
            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
            Candidate c;
            Assert.IsTrue(result.TryGetContentValue<Candidate>(out c));
            Assert.IsNotNull(c.id);
            Assert.AreEqual("Eduardo", c.FirstName);
            _inserted_id = c.id;

            //update
            candidate.id = _inserted_id;
            candidate.LastName_2 = "updated lastname_2";
            result = controller.SaveCandidateData(candidate, candidate.id);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            c = null;
            Assert.IsTrue(result.TryGetContentValue<Candidate>(out c));
            Assert.IsNotNull(c.id);
            Assert.AreEqual(candidate.LastName_2, c.LastName_2);

            result = controller.GetCandidateData(_inserted_id);

            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            c = null;
            Assert.IsTrue(result.TryGetContentValue<Candidate>(out c));
            Assert.AreEqual("Eduardo", c.FirstName);

            //luego buscar algo que no existe
            result = controller.GetCandidateData("3");
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);

            //y por ultimo el borrado de lo creado
            result = controller.DeleteCandidate(_inserted_id);
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

            //y  borrado de lo que no existe
            result = controller.DeleteCandidate("3");
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);


        }

        /// <summary>
        /// Ensure that what shouldn't work, really doesn't work (should that be a test?)
        /// </summary>
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

