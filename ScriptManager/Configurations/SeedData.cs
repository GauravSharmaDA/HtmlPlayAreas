using ScriptManager.Contexts;
using ScrtiptManager.Models.Builders;
using System;
using System.Data.Entity;
using System.Linq;

namespace ScriptManager.Models
{
    public class SeedData : CreateDatabaseIfNotExists<ScriptContext>
    {
        ScriptContext _context;
        
        protected override void Seed(ScriptContext context)
        {
            _context = context;
            EnsureSeedData();
            base.Seed(context);
        }
        public void EnsureSeedData()
        {
            if (!_context.Products.Any())
            {
                // Seed data
                _context.Products.Add(new ProductBuilder().WithName("Direct Axis").Build());
                _context.Products.Add(new ProductBuilder().WithName("Sanlam").Build());
                _context.Products.Add(new ProductBuilder().WithName("Call Direct").Build());
                _context.Products.Add(new ProductBuilder().WithName("Cash Power").Build());

                _context.SubProducts.Add(new SubProduct() { Name = "UPL" });
                _context.SubProducts.Add(new SubProduct() { Name = "Secured Loan" });
                _context.SubProducts.Add(new SubProduct() { Name = "Debt Consolidation" });

                _context.Languages.Add(new Language() { Name = "English" });
                _context.Languages.Add(new Language() { Name = "Afrikaans" });
                _context.Languages.Add(new Language() { Name = "Xhosa" });

                var qualScreen = new Screen() { Name = "Qualification" };
                qualScreen.Fields.Add(new Field() { Name = "Initialize" });
                qualScreen.Fields.Add(new Field() { Name = "AskLanguagePreference" });
                qualScreen.Fields.Add(new Field() { Name = "PermissionToPromote" });
                qualScreen.Fields.Add(new Field() { Name = "AccountHolder" });
                qualScreen.Fields.Add(new Field() { Name = "Salary" });
                qualScreen.Fields.Add(new Field() { Name = "SalaryInAccount" });
                qualScreen.Fields.Add(new Field() { Name = "AppliedSomeWhereElse" });
                qualScreen.Fields.Add(new Field() { Name = "Salary" });

                _context.Screens.Add(qualScreen);
                _context.Fields.AddRange(qualScreen.Fields);
                _context.Screens.Add(new Screen() { Name = "Manage Response" });
                _context.Screens.Add(new Screen() { Name = "Debtor Lookup" });
                _context.Screens.Add(new Screen() { Name = "Contact Us" });

                _context.CustomerTypes.Add(new CustomerType() { Name = "Repeat" });
                _context.CustomerTypes.Add(new CustomerType() { Name = "New" });


                // Fake Agents and teams
                string[] array = {
                    "1_craig_engelbrecht_craigen",
                    "2_gaurav_sharma_gauravs",
                    "3_nicholas_johnson_nicholasj",
                    "4_pedro_de_pedrod",
                    "5_ben_killian_benk",
                    "6_matthew_erispe_matthewe"
                };


                Team team = null;
                int i = 1;
                foreach (string s in array)
                {
                    if (team == null || team.Agents.Count == 2)
                    {
                        if (team != null)
                        {
                            _context.Teams.Add(team);
                            _context.Agents.AddRange(team.Agents);

                        }
                        team = new TeamBuilder().WithTeamName("Team_" + i.ToString()).Build();
                        i++;
                    }
                    var agent = s.Split('_');
                    team.Agents.Add(new AgentBuilder()
                        .WithFirstName(agent[1])
                        .WithLastName(agent[2])
                        .WithUserName(agent[3]).Build());

                }
                _context.Teams.Add(team);
                _context.Agents.AddRange(team.Agents);

                // Fake scripts
                //"Initialize" });
                //"AskLanguagePreference" });
                //PermissionToPromot
                //AccountHolder
                //Salary
                //SalaryInAccount
                //AppliedSomeWhereElse" 
                //Salary
                _context.SaveChanges();

                var script = new Script()
                {
                    Screen = _context.Screens.FirstOrDefault(x => x.Name == "Qualification"),
                    Language = _context.Languages.FirstOrDefault(x => x.Name == "English"),
                    Product = _context.Products.FirstOrDefault(x => x.Name == "Direct Axis"),
                    Field = _context.Fields.FirstOrDefault(x => x.Name == "Initialize"),
                    Text = "Hi Dear Customer, I am {agentname} from {productname}. How Can I help you today",
                    Title = "Qual_English_Init_V0"

                };

                var scriptafr = new Script()
                {
                    Screen = _context.Screens.FirstOrDefault(x => x.Name == "Qualification"),
                    Language = _context.Languages.FirstOrDefault(x => x.Name == "Afrikaans"),
                    Product = _context.Products.FirstOrDefault(x => x.Name == "Direct Axis"),
                    Field = _context.Fields.FirstOrDefault(x => x.Name == "Initialize"),
                    Text = "Hi Geagte kliënt, ek {agentname} van {productname}. Hoe kan ek jou help",
                    Title = "Qual_Afrikaans_Init_V0"
                };

                var scriptxho = new Script()
                {
                    Screen = _context.Screens.FirstOrDefault(x => x.Name == "Qualification"),
                    Language = _context.Languages.FirstOrDefault(x => x.Name == "Xhosa"),
                    Product = _context.Products.FirstOrDefault(x => x.Name == "Direct Axis"),
                    Field = _context.Fields.FirstOrDefault(x => x.Name == "Initialize"),
                    Text = "Hi Customer lotsandzekako, ngiyati {agentname} kusukela {igama lomkhiqizo}. Ngingabhekana Kanjani ukukusiza namuhla",
                    Title = "Qual_Xhosa_Init_V0"
                };
                _context.Scripts.Add(script);
                _context.Scripts.Add(scriptafr);
                _context.Scripts.Add(scriptxho);

                var scriptv1 = new Script()
                {
                    Screen = _context.Screens.FirstOrDefault(x => x.Name == "Qualification"),
                    Language = _context.Languages.FirstOrDefault(x => x.Name == "English"),
                    Product = _context.Products.FirstOrDefault(x => x.Name == "Direct Axis"),
                    Field = _context.Fields.FirstOrDefault(x => x.Name == "Initialize"),
                    Text = "[Version 1]Hi Dear Customer, I am {agentname} from {productname}. How Can I help you today",
                    Title = "Qual_English_Init"

                };

                var scriptafrv1 = new Script()
                {
                    Screen = _context.Screens.FirstOrDefault(x => x.Name == "Qualification"),
                    Language = _context.Languages.FirstOrDefault(x => x.Name == "Afrikaans"),
                    Product = _context.Products.FirstOrDefault(x => x.Name == "Direct Axis"),
                    Field = _context.Fields.FirstOrDefault(x => x.Name == "Initialize"),
                    Text = "[Version 1]Hi Geagte kliënt, ek {agentname} van {productname}. Hoe kan ek jou help",
                    Title = "Qual_Afrikaans_Init"
                };

                var scriptxhov1 = new Script()
                {
                    Screen = _context.Screens.FirstOrDefault(x => x.Name == "Qualification"),
                    Language = _context.Languages.FirstOrDefault(x => x.Name == "Xhosa"),
                    Product = _context.Products.FirstOrDefault(x => x.Name == "Direct Axis"),
                    Field = _context.Fields.FirstOrDefault(x => x.Name == "Initialize"),
                    Text = "[Version 1]Hi Customer lotsandzekako, ngiyati {agentname} kusukela {igama lomkhiqizo}. Ngingabhekana Kanjani ukukusiza namuhla",
                    Title = "Qual_Xhosa_Init"
                };
                _context.Scripts.Add(scriptv1);
                _context.Scripts.Add(scriptafrv1);
                _context.Scripts.Add(scriptxhov1);
                // Fake releases

                var team1 = _context.Teams.OrderBy(x=>x.Id).FirstOrDefault();
                var team2 = _context.Teams.OrderBy(x=>x.Id).Skip(1).FirstOrDefault();

                var releaseTV = new Release()
                {
                    ReleaseName = "Release TV",
                    Teams = new System.Collections.Generic.List<Team>
                    {
                        team1
                    },
                    IsActive = true,
                    Scripts = new System.Collections.Generic.List<Script>
                    {
                        script, scriptafr, scriptafr
                    },
                    CreatedAt = DateTime.Now
                };

                var releaseFulfilment = new Release()
                {
                    ReleaseName = "Release Fulfilment",
                    Teams = new System.Collections.Generic.List<Team>
                    {
                        team2
                    },
                    IsActive = true,
                    Scripts = new System.Collections.Generic.List<Script>
                    {
                        scriptv1, scriptafrv1, scriptafrv1
                    },
                    CreatedAt = DateTime.Now
                };
                _context.Releases.Add(releaseFulfilment);
                _context.Releases.Add(releaseTV);
                _context.SaveChanges();

            }
        }
    }
}
