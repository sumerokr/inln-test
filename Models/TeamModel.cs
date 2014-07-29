using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace inln_test.Models
{
    public class TeamModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TeamId { get; set; }
        [Display(Name = "Название команды")]
        public string TeamName { get; set; }

        public static List<TeamModel> GetTeams(string fileUrl)
        {
            List<TeamModel> teams = new List<TeamModel>();
            XDocument xmlDoc = XDocument.Load(fileUrl);
            foreach (XElement el in xmlDoc.Root.Elements("row"))
            {
                TeamModel team = new TeamModel();

                team.TeamId = int.Parse(el.Element("TEAMID").Value);
                team.TeamName = el.Element("TEAMNAME").Value;

                teams.Add(team);
            }
            return teams;
        }
    }
}