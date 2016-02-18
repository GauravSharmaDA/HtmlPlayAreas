using ScriptManager.Contexts;
using ScriptManager.Models;
using ScriptManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web.Http;

namespace ScriptManager.Repository
{
    public class ScriptRepository
    {
        private ScriptContext _context;

        public ScriptRepository()
        {
            _context = new ScriptContext();
        }

        public IEnumerable<Script> GetAllScriptsWithFields()
        {
            return _context.Scripts
                .Include(x => x.Language)
                .Include(x => x.Screen)
                .Include(x => x.Field)
                .Include(x => x.Product)
                .Include(x => x.SubProduct)
                .ToList();
        }

        public Script ProvideScript(ScriptRequestViewModel viewModel)
        {
            var agent = _context.Agents.FirstOrDefault(x => x.UserName.Equals(viewModel.AgentName, System.StringComparison.InvariantCultureIgnoreCase));
            var language = _context.Languages.FirstOrDefault(x => x.Name.Equals(viewModel.LanguagePreference, System.StringComparison.InvariantCultureIgnoreCase));
            var screen = _context.Screens.FirstOrDefault(x => x.Name.Equals(viewModel.ScreenName, System.StringComparison.InvariantCultureIgnoreCase));
            var field = _context.Fields.FirstOrDefault(x => x.Name.Equals(viewModel.FieldName, System.StringComparison.InvariantCultureIgnoreCase));
            var team = _context.Teams.FirstOrDefault(x => x.Agents.Any(ag => ag.Id.Equals(agent.Id)));
            var release = _context.Releases.Include("Scripts").FirstOrDefault(x => x.Teams.Any(te => te.Id.Equals(team.Id)));
            if (release == null)
                throw new Exception("Nothing has been released to this agent.");

            var script = release.Scripts.FirstOrDefault
                (la => la.Language.Id.Equals(language.Id) && la.Screen.Id.Equals(screen.Id)
                && la.Field.Id.Equals(field.Id));

            return script;
        }

        public Script ScriptById(int scriptId)
        {
            return _context.Scripts
                .Include(x => x.Language)
                .Include(x => x.Screen)
                .Include(x => x.Field)
                .Include(x => x.Product)
                .Include(x => x.SubProduct)
                .FirstOrDefault(x => x.Id == scriptId);
        }

        public bool SaveScript(Script script)
        {
            Script existingScript = null;
            var tempScript = _context.Scripts.FirstOrDefault(x => x.Title == script.Title && x.Id != script.Id);
            if (tempScript != null)
            {
                throw new Exception("Another script with similar name already exists.");
            }
            if (script.Id != 0)
            {
                existingScript = _context.Scripts.FirstOrDefault(x => x.Id == script.Id);
            }


            if (existingScript != null)
            {
                existingScript.Title = script.Title;
                existingScript.Language = script.Language;
                existingScript.Product = script.Product;
                existingScript.Screen = script.Screen;
                existingScript.SubProduct = script.SubProduct;
                existingScript.Text = script.Text;
                existingScript.CustomerType = script.CustomerType;
                existingScript.Field = script.Field;
            }
            else
            {
                _context.Scripts.Add(script);
            }
            _context.Entry(script.Language).State = EntityState.Modified;
            _context.Entry(script.Product).State = EntityState.Modified;
            _context.Entry(script.Screen).State = EntityState.Modified;
            _context.Entry(script.Field).State = EntityState.Modified;

            _context.SaveChanges();
            return true;
        }

        ///
        public ScreenResponse ProvideScriptsForAScreen(ScriptRequestViewModel viewModel)
        {
            var agent = _context.Agents.FirstOrDefault(x => x.UserName.Equals(viewModel.AgentName, System.StringComparison.InvariantCultureIgnoreCase));
            var language = _context.Languages.FirstOrDefault(x => x.Name.Equals(viewModel.LanguagePreference, System.StringComparison.InvariantCultureIgnoreCase));
            var screen = _context.Screens.FirstOrDefault(x => x.Name.Equals(viewModel.ScreenName, System.StringComparison.InvariantCultureIgnoreCase));
            var team = _context.Teams.FirstOrDefault(x => x.Agents.Any(ag => ag.Id.Equals(agent.Id)));
            var release = _context
                .Releases
                .Include(x => x.Scripts)
                .Include(x => x.Scripts.Select(y => y.Language))
                .Include(x => x.Scripts.Select(y => y.Screen))
                .Include(x => x.Scripts.Select(y => y.Field))
                .Include(x => x.Scripts.Select(y => y.Product))
                .Include(x => x.Scripts.Select(y => y.SubProduct))                
                .FirstOrDefault(x => x.Teams.Any(te => te.Id.Equals(team.Id)));
            if (release == null)
                throw new Exception("Nothing has been released to this agent.");

            var scripts = release.Scripts.Where
                (la => 
                    (language == null || la.Language.Id.Equals(language.Id)) 
                    && la.Screen.Id.Equals(screen.Id)
                ).ToList();

            var screenResponse = new ScreenResponse() { ScreenName = viewModel.ScreenName };
            screenResponse.Scripts = (from s in scripts
                                      select new ScriptResponse {
                                          Text = s.Text,
                                          Screen = s.Screen,
                                          Field = s.Field
                                      }).ToList();
            var flagRepository = new FlagRepository();
            foreach (var script in screenResponse.Scripts)
            {
                var flag = flagRepository.FlaggedForAfield(script.Screen.Id, script.Field.Id, agent.Id);
                if (flag!=null)
                {
                    script.FlagMessage = flag.Comment;
                    script.IsFlaggedForAgent = true;
                }
            }
            return screenResponse;
        }
    }
}
