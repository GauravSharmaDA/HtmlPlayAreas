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
        public Screen Screen { get; set; }
        public Agent Agent { get; set; }
        public string Comment { get; set; }
    }
}