using System.Collections.Generic;

namespace ScriptManager.Models
{
    /// <summary>
    /// This represents script as standard response to a consumer application
    /// </summary>
    public class ScriptResponse
    {
        public string Text { get; set; }
        public string FlagMessage { get; set; }
        public bool IsFlaggedForAgent { get; set; }
    }

    /// <summary>
    /// This represents screen as standard response to a consumer application
    /// </summary>
    public class ScreenResponse
    {
        public string ScreenName { get; set; }
        public List<ScriptResponse> Scripts { get; set; }
    }
}