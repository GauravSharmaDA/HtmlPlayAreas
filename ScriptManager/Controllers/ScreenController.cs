using ScriptManager.Models;
using ScriptManager.Repository;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace ScriptManager.Controllers
{
    /// <summary>
    /// Returns different objects related to screen
    /// </summary>
    public class ScreenController : ApiController
    {
        /// <summary>
        /// Returns fields for a screen
        /// </summary>
        /// <returns></returns>
        [Route("GetFieldsbyScreen")]
        [ResponseType(typeof(Field))]
        public IHttpActionResult Get(int screenId)
        {
            var repository = new ScreenRepository();
            var fields = repository.GetFieldsForAScreen(screenId);
            return Ok(fields);
        }

        /// <summary>
        /// Returns list for screens
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(Field))]
        public IHttpActionResult Get()
        {
            var repository = new ScreenRepository();
            var fields = repository.GetScreens();
            return Ok(fields);
        }


        [Route("GetProducts")]
        [ResponseType(typeof(IEnumerable<Product>))]
        public IHttpActionResult GetProducts()
        {
            var repository = new ScreenRepository();
            return Ok(repository.GetProducts());
        }

        [Route("GetLanguage")]
        [ResponseType(typeof(IEnumerable<Language>))]
        public IHttpActionResult GetLanguages()
        {
            var repository = new ScreenRepository();
            return Ok(repository.GetLanguages());
            
        }
    }
}
