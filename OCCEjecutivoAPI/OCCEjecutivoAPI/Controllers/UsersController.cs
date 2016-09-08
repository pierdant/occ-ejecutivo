using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OCCEjecutivoAPI.Models;

namespace OCCEjecutivoAPI.Controllers
{
    public class UsersController : ApiController
    {
        //Get /api/Users/#
        public HttpResponseMessage GetUserData(string id)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        // Post /api/Users/{#} json candidate object
        public HttpResponseMessage SaveCandidateData(User user, string id = "0")
        {
            bool existingObject = false;

            #region FieldParamValidation

            if (id.CompareTo("0") != 0)
            {
                existingObject = true;
                if (String.IsNullOrEmpty(user.id))
                    return Request.CreateErrorResponse(HttpStatusCode.Conflict, "null or empty object_id does not matches instance_id");
                if (!id.Equals(user.id))
                    return Request.CreateErrorResponse(HttpStatusCode.Conflict, "instance_Id different from object_id");
            }

            if (String.IsNullOrEmpty(user.Email))
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "null or empty email");
            #endregion

            try
            {
                DataContext cn = new DataContext();


                if (existingObject)
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Created);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
