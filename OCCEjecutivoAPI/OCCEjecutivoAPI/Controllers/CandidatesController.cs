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
        //todo: ver con Karina si podemos o como deberiamos hacer esto para que UTest siempre jale


        Candidate[] allThePeople = new Candidate[]
        {
            new Candidate {
                Id = "1",
                FirstName = "Juan",
                LastName_1 = "Perez",
                Email = "juan@perez.com",
                PhoneNumber = "(442) 123-4567"
            },
            new Candidate {
                Id = "2",
                FirstName = "Gonzalo",
                LastName_1 = "Perez",
                Email = "gonzalo@perez.com",
                PhoneNumber = "(442) 123-4567"
            }
        };

        public IHttpActionResult GetCandidateData(string id)
        {
            try
            {
                var aPerson = allThePeople.First((p) => p.Id == id);
                if (aPerson == null)
                    return NotFound();
                else
                    return Ok(aPerson);

            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
