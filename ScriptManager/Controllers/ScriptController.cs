using ScriptManager.Models;
using ScriptManager.Repository;
using ScriptManager.ViewModels;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using System.Linq;
using System.Web.Http.Tracing;

namespace ScriptManager.Controllers
{
    /// <summary>
    /// All the functionality related to the script
    /// </summary>
    
    public class ScriptController : ApiController
    {
        /// <summary>
        /// Returns all scripts from the database
        /// </summary>
        /// <returns>All scripts from the database</returns>
        /// 
       // [Route("GetAllScripts")]
        [ResponseType(typeof(List<Script>))]
        public IEnumerable<Script> Get()
        {
            var repository = new ScriptRepository();
            return repository.GetAllScriptsWithFields();
        }

        /// <summary>
        /// Returns script by Matching to its Id number
        /// </summary>
        /// <param name="scriptId">Unique ID of a Script</param>
        /// <returns>Returns an object of type script.</returns>
        [Route("GetScriptById")]
        [ResponseType(typeof(Script))]
        public IHttpActionResult Get(int scriptId)
        {
            var repository = new ScriptRepository();
            var script = repository.ScriptById(scriptId);
            return Ok(script);
        }

        /// <summary>
        /// Search for a script
        /// </summary>
        /// <param name="request">Type of ScriptRequestViewModel</param>
        /// <remarks>Returns appropriate scrtpt as per the filters.</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [Route("ProvideScript")]
        [ResponseType(typeof(ScriptResponse))]
        [ActionName("ProvideResult")]
        public IHttpActionResult ProvideScript(ScriptRequestViewModel request)
        {
            var script = new ScriptRepository().ProvideScript(request);
            var response = new ScriptResponse()
            {
                Text = script.Text
            };
                

            return Ok(script);  
        }

        /// <summary>
        /// Search for a script
        /// </summary>
        /// <param name="request">Represents fields for a filter</param>
        /// <remarks>Returns appropriate scrtpt as per the filters.</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [Route("ProvideScriptsForAScreen")]
        [ResponseType(typeof(IEnumerable<Script>))]
        [ActionName("ProvideScriptsForAScreen")]
        public IHttpActionResult ProvideScriptsForAScreen(ScriptRequestViewModel request)
        {
            var scripts = new ScriptRepository().ProvideScriptsForAScreen(request);
            
            return Ok(scripts);
        }

        /// <summary>
        /// Saves an instance of a script
        /// </summary>
        /// <param name="script">Inserts a new or update an existing script</param>
        [Route("Save")]
        [ActionName("Save")]
        public IHttpActionResult Save(Script script)
        {
            if(ModelState.IsValid)
            {
                var result = new ScriptRepository().SaveScript(script);
                return Ok(result); 
            }
            return new System.Web.Http.Results.ExceptionResult(new System.Exception(), this);
        }
    }
}
