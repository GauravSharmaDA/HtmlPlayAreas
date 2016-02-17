
using ScriptManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrtiptManager.Models.Builders
{
    public class TeamBuilder : BaseBuilder<Team>
    {
        Team _team;
        public TeamBuilder()
        {
            _team = new Team();
            base.CurrentObject = _team;
        }

        public TeamBuilder WithId(int id)
        {
            _team.Id = id;
            return this;
        }

        public TeamBuilder WithAgent(Agent agent)
        {
            _team.Agents.Add(agent);
            return this;
        }

        public TeamBuilder WithTeamName(string teamName)
        {
            _team.TeamName = teamName;
            return this;
        }
    }
}
