using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FirebirdSql.Data.FirebirdClient;
using Newtonsoft.Json;
using WebApiFierbird.Models;
//https://www.youtube.com/watch?v=uNJkPiq0f1s
//https://blog.kloud.com.au/2017/02/27/remote-access-to-local-aspnet-core-apps-from-mobile-devices/

namespace WebApiFierbird.Controllers
{
    public class ValuesController : ApiController
    {
        EskanerIrakurriaDB _eskanerIrakurriaDB = new EskanerIrakurriaDB();
        // GET api/values
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //public string Get([FromBody]int id)
        //{
        //    return "stringa";
        //}

        // GET /api/values?albaranId=1
        public HttpResponseMessage Get(int albaranId)
        {
            string existDatabase = "true";
            existDatabase= _eskanerIrakurriaDB.ExistAlbaran(albaranId);
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(existDatabase);
            return response;
        }


        // GET /api/values?albaranId=1
        public HttpResponseMessage Get(int bultoId, string taula)
        {
            string existDatabase = "true";
            existDatabase = _eskanerIrakurriaDB.ExistBulto(bultoId);
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(existDatabase);
            return response;
        }



        // GET /api/values?albaran=1
        public HttpResponseMessage Get(string albaran)
        {
            string existDatabase = "true";
            string albRegisters = _eskanerIrakurriaDB.ExistAlbRegister(albaran);
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(albRegisters);
            return response;
        }



        // GET api/values/5
        //public string Get(int id)
        //{
        //    DataTable dt = _eskanerIrakurriaDB.GetAlbaran("albaran1");
        //    string JSONString = string.Empty;
        //    JSONString = JsonConvert.SerializeObject(dt);
        //    return JSONString;
        //}

        ////GET /api/values?albaran=albaran1
        //public string Get(string albaran)
        //{
        //    DataTable dt = _eskanerIrakurriaDB.GetAlbaran(albaran);
        //    string JSONString = string.Empty;
        //    JSONString = JsonConvert.SerializeObject(dt);
        //    return JSONString;
        //}


        //// GET /api/values?albaran=albaran1
        //public HttpResponseMessage Get(string codigoBerria)
        //{
        //    DataTable dt = _eskanerIrakurriaDB.GetAlbaran(codigoBerria);
        //    string JSONString = string.Empty;
        //    JSONString = JsonConvert.SerializeObject(dt);

        //    var response = this.Request.CreateResponse(HttpStatusCode.OK);
        //    response.Content = new StringContent(JSONString, System.Text.Encoding.UTF8, "application/json");
        //    return response;

        //    //return JSONString;
        //}



        //// GET /api/values?albaran=albaran1
        //[ResponseType(typeof(Oferta_Notes))]
        //public IHttpActionResult Get(string albaran)
        //{
        //    DataTable dt = _eskanerIrakurriaDB.GetAlbaran(albaran);
        //    string JSONString = string.Empty;
        //    JSONString = JsonConvert.SerializeObject(dt);

        //    var response = this.Request.CreateResponse(HttpStatusCode.OK);
        //    response.Content = new StringContent(JSONString, System.Text.Encoding.UTF8, "application/json");
        //    return response;

        //    //return JSONString;
        //}



        //// POST api/values
        //[HttpPost]

        //public void Post([FromBody]string value)
        //{

        //}


        //// POST api/values
        //[HttpPost]

        //public void Post([FromBody]string value)
        //{

        //}



        // POST api/values
        [HttpPost]

        public HttpResponseMessage Post(List<EskanerIrakurria> eskanerIrakurrias, string taula)
        {
            bool emaitza = true;
            List<EskanerIrakurria> eskanerIrakurriasPost = eskanerIrakurrias;
            try
            {
            
            if (taula.Equals("RECEP_SUBCON"))
                emaitza = _eskanerIrakurriaDB.InsertAlbaran(eskanerIrakurrias);
            else
                emaitza = _eskanerIrakurriaDB.Insert(eskanerIrakurrias);
            if (emaitza)
                //https://www.c-sharpcorner.com/article/all-about-api-http-response-message-part-three/
                return Request.CreateResponse(HttpStatusCode.Created, "201 sortu da");
            else
                return Request.CreateResponse(HttpStatusCode.BadRequest, "gaizki joan da");
        }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
}


        //////https://www.coderslexicon.com/solving-null-value-when-posting-simple-string-net-web-api/
        //[HttpPost]

        //public void Post(HttpRequestMessage request)

        //{

        //    // Now pull out the body from the request (in our case "body" will be set to "id=3&myvar=1&myothervar=2")

        //    string body = request.Content.ReadAsStringAsync().Result;



        //    // Heck let's get the headers as well...


        //}



        //////https://www.coderslexicon.com/solving-null-value-when-posting-simple-string-net-web-api/
        //[HttpPost]

        //public HttpResponseMessage Post(HttpRequestMessage request)

        //{

        //    // Now pull out the body from the request (in our case "body" will be set to "id=3&myvar=1&myothervar=2")

        //    string body = request.Content.ReadAsStringAsync().Result;



        //    // Heck let's get the headers as well...

        //    //var response = this.Request.CreateResponse(HttpStatusCode.OK);
        //    //response.Content = new StringContent("ondo joan da", System.Text.Encoding.UTF8, "application/json");
        //    //return response;

        //    //https://www.c-sharpcorner.com/article/all-about-api-http-response-message-part-three/
        //    return Request.CreateResponse(HttpStatusCode.OK, "ondo joan da");


        //}






        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        [HttpPost]
        [ActionName("Complex")]
        public string PostComplex(string update)
        {
            return update;
        }
    }
}
