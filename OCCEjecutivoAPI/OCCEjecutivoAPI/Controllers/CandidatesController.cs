using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OCCEjecutivoAPI.Models;

using System.Diagnostics;

namespace OCCEjecutivoAPI.Controllers
{
    public class CandidatesController : ApiController
    {

        List<Candidate> allThePeople = new List<Candidate>();

        public CandidatesController()
        {
            allThePeople.Add(new Candidate {
                id = "1",
                FirstName = "Juan",
                LastName_1 = "Perez",
                Email = "juan@perez.com",
                PhoneNumber = "(442) 123-4567"
            });

            allThePeople.Add(new Candidate
            {
                id = "2",
                FirstName = "Gonzalo",
                LastName_1 = "Perez",
                Email = "gonzalo@perez.com",
                PhoneNumber = "(442) 123-4567"
            });
        }

        //Get /api/Candidates/#
        public HttpResponseMessage GetCandidateData(string id)
        {
            try
            {
                var aPerson = allThePeople.FirstOrDefault((p) => p.id == id);
                if (aPerson == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                else
                    return Request.CreateResponse(aPerson);

            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        // Post /api/Candidates/{#} json candidate object
        public HttpResponseMessage SaveCandidateData(Candidate candidate, string id = "0")
        {
            bool existingObject = false;

            #region FieldParamValidation

            if (id.CompareTo("0") != 0 )
            {
                existingObject = true;
                if (String.IsNullOrEmpty(candidate.id))
                    return Request.CreateErrorResponse(HttpStatusCode.Conflict, "null or empty object_id does not matches instance_id");
                if( !id.Equals(candidate.id))
                    return Request.CreateErrorResponse(HttpStatusCode.Conflict, "instance_Id different from object_id");
            }

            if(String.IsNullOrEmpty(candidate.Email))
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "null or empty email");
            if (String.IsNullOrEmpty(candidate.PhoneNumber))
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "null or empty phone number");
            if (String.IsNullOrEmpty(candidate.FirstName))
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "null or empty first name");
            #endregion

            try
            {
                DataContext cn = new DataContext();


                if( existingObject )
                {
                    // do an update operation
                    allThePeople.Add(candidate); // todo: digamos que lo mandamos a la BD, solo hay que asegurar que si se va!
                    return Request.CreateResponse<Candidate>(HttpStatusCode.OK, candidate);
                }
                else
                {
                    candidate.id = Guid.NewGuid().ToString().Replace("-", "");
                    cn.Candidates_Data.InsertOneAsync(candidate).GetAwaiter().GetResult();
                    //allThePeople.Add(candidate); // todo: digamos que lo mandamos a la BD, solo hay que asegurar que si se va!
                    return Request.CreateResponse<Candidate>(HttpStatusCode.Created, candidate);
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


    }
}
