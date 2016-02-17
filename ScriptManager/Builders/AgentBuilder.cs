using ScriptManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrtiptManager.Models.Builders
{
    public class AgentBuilder : BaseBuilder<Agent>
    {
        Agent _agent;
        public AgentBuilder()
        {
            _agent = new Agent();
            base.CurrentObject = _agent;
        }
        public AgentBuilder WithId(int Id)
        {
            _agent.Id = Id;
            return this;
        }
        public AgentBuilder WithFirstName(string name)
        {
            _agent.FirstName = name;
            return this;
        }
        public AgentBuilder WithLastName(string name)
        {
            _agent.LastName = name;
            return this;
        }
        public AgentBuilder WithUserName(string name)
        {
            _agent.UserName = name;
            return this;
        }
    }
}
