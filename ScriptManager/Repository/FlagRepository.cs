using ScriptManager.Contexts;
using ScriptManager.Models;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

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
            var exisitingFlag = _context.Flags.FirstOrDefault(x => x.Id == flag.Id);

            if (exisitingFlag != null)
            {
                exisitingFlag.Agent = flag.Agent;
                exisitingFlag.Field = flag.Field;
                exisitingFlag.Screen = flag.Screen;
                exisitingFlag.Comment = flag.Comment;
            }
            else
                _context.Flags.Add(flag); 
            _context.Entry(flag.Agent).State = EntityState.Unchanged;
            _context.Entry(flag.Screen).State = EntityState.Unchanged;
            _context.Entry(flag.Field).State = EntityState.Unchanged;
            _context.SaveChanges();

            return true;
        }

        public Flag FlaggedForAfield(int screenId, int fieldId, int agentId)
        {
            return _context.Flags.FirstOrDefault(x => x.Screen.Id.Equals(screenId) && x.Field.Id.Equals(fieldId) && x.Agent.Id.Equals(agentId));
        }

        public IEnumerable<Flag> FlaggedIssues()
        {
            return _context.Flags
                .Include(x=>x.Agent)
                .Include(x => x.Field)
                .Include(x => x.Screen)
                .ToList();
        }

        public Flag FlaggedIssueById(int id)
        {
            return _context.Flags
                .Include(x => x.Agent)
                .Include(x => x.Field)
                .Include(x => x.Screen)
                .FirstOrDefault(x=>x.Id.Equals(id));
        }
    }
}