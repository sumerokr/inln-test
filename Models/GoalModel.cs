using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace inln_test.Models
{
    public class GoalModel
    {
        [Key]
        public int GoalModelId { get; set; }

        [Display(Name = "Дата матча")]
        [DataType(DataType.Date)]
        public DateTime MatchDate { get; set; }

        [Display(Name = "Минута гола")]
        public int GoalTime { get; set; }

        public int PlayerId { get; set; }
        [ForeignKey("PlayerId")]
        public virtual PlayerModel Player { get; set; }

        public int TeamId { get; set; }
        [ForeignKey("TeamId")]
        public virtual TeamModel Team { get; set; }

        public virtual List<CommentModel> Comment { get; set; }

        public static List<GoalModel> GetGoals(string fileUrl)
        {
            List<GoalModel> goals = new List<GoalModel>();
            XDocument xmlDoc = XDocument.Load(fileUrl);
            foreach (XElement el in xmlDoc.Root.Elements("row"))
            {
                GoalModel goal = new GoalModel();

                goal.MatchDate = DateTime.ParseExact(el.Element("MATCHDATE").Value, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture).Date;
                goal.TeamId = int.Parse(el.Element("TEAMID").Value);
                goal.PlayerId = int.Parse(el.Element("PLAYERID").Value);
                goal.GoalTime = int.Parse(el.Element("GOALTIME").Value);

                goals.Add(goal);
            }
            return goals;
        }
    }
}