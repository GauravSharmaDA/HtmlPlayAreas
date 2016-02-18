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

        /// <summary>
        /// Returns list of Reported Issue
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get()
        {
            return Ok(new FlagRepository().FlaggedIssues());
        }

        /// <summary>
        /// Returns Reported Issue by id
        /// </summary>
        /// <returns></returns>
        [Route("GetReportedIssueById")]
        [ActionName("GetById")]
        public IHttpActionResult Get(int id)
        {
            return Ok(new FlagRepository().FlaggedIssueById(id));
        }
    }
}
