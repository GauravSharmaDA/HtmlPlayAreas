using ScriptManager.Contexts;
using ScriptManager.Models;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System;

namespace ScriptManager.Repository
{
    public class ScreenRepository
    {
        private ScriptContext _context;
        public ScreenRepository()
        {
            _context = new ScriptContext();
        }

        public IEnumerable<Field> GetFieldsForAScreen(int screenId)
        {
            var screen = _context.Screens.Include(y=>y.Fields).FirstOrDefault(x => x.Id == screenId);
            if (screen == null)
                throw new NotFoundException(string.Format("Screen for id {0}",screenId));
            return screen.Fields;
        }

        public IEnumerable<Screen> GetScreens()
        {
            return _context.Screens.ToList();
        }

        public IEnumerable<Language> GetLanguages()
        {
            return _context.Languages.ToList();
        }

        internal IEnumerable<Product> GetProducts()
        {
            return _context.Products.ToList();
        }
    }
}
