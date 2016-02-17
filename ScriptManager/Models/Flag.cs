using System;

namespace ScriptManager.Models
{
    /// <summary>
    /// A Quality analyst can flag a field for a specific agent
    /// </summary>
    public class Flag
    {
        public int Id { get; set; }
        public Field Field { get; set; }
        public Agent Agent { get; set; }
        public DateTime ShowFrom { get; set; }
        public DateTime ShowUpTo { get; set; }
        public string Comment { get; set; }
        public bool AgentDoneWithFlag { get; set; }
    }
}