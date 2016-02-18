using ScriptManager.Contexts;
using ScriptManager.Models;

namespace ScriptManager.Repository
{
    public class FlagRepository 
    {
        ScriptContext _context;
        public FlagRepository()
        {
            _context = new ScriptContext();
        }

        public bool ReportAnIssue(Flag flag)
        {
            _context.Entry(flag.Agent).State = System.Data.Entity.EntityState.Modified;
            _context.Entry(flag.Screen).State = System.Data.Entity.EntityState.Modified;
            _context.Entry(flag.Field).State = System.Data.Entity.EntityState.Modified;
            _context.Flags.Add(flag);
            _context.SaveChanges();
            return true;
        }
    }
}