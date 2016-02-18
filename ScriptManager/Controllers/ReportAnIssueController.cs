using ScriptManager.Models;
using ScriptManager.Repository;
using System.Web.Http;

namespace ScriptManager.Controllers
{
    /// <summary>
    /// To Report the mistakes agent made
    /// </summary>
    public class ReportAnIssueController : ApiController
    {
        /// <summary>
        /// Report an Issue
        /// </summary>
        /// <param name="flag">Instance of flag</param>
        /// <returns></returns>
        [Route("Report")]
        [ActionName("Report")]
        public IHttpActionResult Report(Flag flag)
        {
            if (ModelState.IsValid)
            {
                var result = new FlagRepository().ReportAnIssue(flag);
                return Ok(result);
            }
            return new System.Web.Http.Results.ExceptionResult(new System.Exception(), this);
        }
    }
}
