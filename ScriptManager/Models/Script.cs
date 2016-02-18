using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScriptManager.Models
{
    /// <summary>
    /// Class Represents the release to one or more team(s)
    /// </summary>
    public class Release
    {
        public Release()
        {
            Teams = new List<Team>();
            Scripts = new List<Script>();
        }
        public int Id { get; set; }
        public string ReleaseName { get; set; }
        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }
        public List<Team> Teams { get; set; }
        public List<Script> Scripts { get; set; }
    }

    /// <summary>
    /// Represents a group of agents
    /// </summary>
    public class Team
    {
        public Team()
        {
            Agents = new List<Agent>();
        }
        public int Id { get; set; }
        public string TeamName { get; set; }
        public List<Agent> Agents { get; set; }
        public List<Release> Releases { get; set; }
    }

    /// <summary>
    /// Represents an agent 
    /// </summary>
    public class Agent
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
    }

    /// <summary>
    /// Represents a unique script for a field, a language, a product released to specific team
    /// </summary>
    public class Script
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        public Language Language { get; set; }
        public Product Product { get; set; }
        public SubProduct SubProduct { get; set; }
        public Screen Screen { get; set; }
        public Field Field { get; set; }
        public CustomerType CustomerType { get; set; }
        [Column(TypeName = "ntext")]
        public string Text { get; set; }
    }

    /// <summary>
    /// Type of customer
    /// </summary>
    public class CustomerType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    /// <summary>
    /// Represents language
    /// </summary>
    public class Language
    {

        public int Id { get; set; }

        public string Name { get; set; }
    }

    /// <summary>
    /// Represents a product
    /// </summary>
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }


    /// <summary>
    /// Represents a subproduct
    /// </summary>
    public class SubProduct
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    /// <summary>
    /// Represents a screen
    /// </summary>
    public class Screen
    {
        public Screen()
        {
            Fields = new List<Field>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Field> Fields { get; set; }
    }

    /// <summary>
    /// Represents a field
    /// </summary>
    public class Field
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
    }
}