using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiFierbird.Models;

namespace WebApiFierbird.Controllers
{
    public class EskanerController : ApiController
    {
        EskanerIrakurriaDB _eskanerIrakurriaDB = new EskanerIrakurriaDB();


        public string Get(int id)
        {
            return "stringa";
        }



        // POST api/Eskaner
        [HttpPost]

        public IHttpActionResult Post(ArrayList checkSubconList)
        {
            ArrayList listSubconCheked = new ArrayList();
            listSubconCheked = _eskanerIrakurriaDB.checkSubcon(checkSubconList);
            //if (emaitza)
            //    //https://www.c-sharpcorner.com/article/all-about-api-http-response-message-part-three/
            //    return Created("sortu dituk", eskanerIrakurrias);
            //else
            //    return BadRequest("gaizki mezua");
            return Ok(listSubconCheked);
        }



        //// POST api/Eskaner
        //[HttpPost]

        //public IHttpActionResult Post(List<EskanerIrakurria> eskanerIrakurrias)
        //{
        //    List<EskanerIrakurria> eskanerIrakurriasPost = eskanerIrakurrias;

        //    bool emaitza = _eskanerIrakurriaDB.Insert(eskanerIrakurrias);
        //    if (emaitza)
        //        //https://www.c-sharpcorner.com/article/all-about-api-http-response-message-part-three/
        //        return Created("sortu dituk", eskanerIrakurrias);
        //    else
        //        return BadRequest("gaizki mezua");
        //}


    }
}
