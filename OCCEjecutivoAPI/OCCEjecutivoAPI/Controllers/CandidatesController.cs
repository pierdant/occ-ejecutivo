using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OCCEjecutivoAPI.Models;
using MongoDB.Driver;


namespace OCCEjecutivoAPI.Controllers
{
    public class CandidatesController : ApiController
    {

        //Get /api/Candidates/#
        public HttpResponseMessage GetCandidateData(string id)
        {
            try
            {
                DataContext cn = new DataContext();
                var filter = Builders<Candidate>.Filter.Eq("id", id);
                var list = cn.Candidates_Data
                    .Find(filter)
                    .Limit(1)
                    .ToListAsync()
                    .GetAwaiter().GetResult();
                if( list.Count > 0)
                    return Request.CreateResponse(list[0]);
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);

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
                    // there's two ways: a) take the whole incomming object and simply replace it, or,
                    // b) look for the object at the db, check which incomming fields are not null and have changed, replace them and then update the object at the DB
                    // for simplicity, I'm going ahead with option A)!!! :)

                    var filter = Builders<Candidate>.Filter.Eq("id", candidate.id);
                    var result = cn.Candidates_Data.ReplaceOneAsync(filter, candidate).GetAwaiter().GetResult();
                    if (result.IsAcknowledged && (result.ModifiedCount > 0))
                        return Request.CreateResponse<Candidate>(HttpStatusCode.OK, candidate);
                    else
                        return Request.CreateResponse(HttpStatusCode.NotFound);
                }
                else
                {
                    candidate.id = Guid.NewGuid().ToString().Replace("-", "");
                    cn.Candidates_Data.InsertOneAsync(candidate).GetAwaiter().GetResult();
                    return Request.CreateResponse<Candidate>(HttpStatusCode.Created, candidate);
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage DeleteCandidate(string id)
        {
            try
            {
                DataContext cn = new DataContext();
                var filter = Builders<Candidate>.Filter.Eq("id", id);
                var result = cn.Candidates_Data.DeleteOneAsync(filter).GetAwaiter().GetResult();
                if (result.IsAcknowledged && (result.DeletedCount > 0))
                    return Request.CreateResponse(HttpStatusCode.OK);
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

        }

    }
}
