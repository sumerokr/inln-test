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
    public class PlayerModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PlayerId { get; set; }
        [Display(Name = "Имя игрока")]
        public string PlayerName { get; set; }

        public static List<PlayerModel> GetPlayers(string fileUrl)
        {
            List<PlayerModel> players = new List<PlayerModel>();
            XDocument xmlDoc = XDocument.Load(fileUrl);
            foreach (XElement el in xmlDoc.Root.Elements("row"))
            {
                PlayerModel player = new PlayerModel();

                player.PlayerId = int.Parse(el.Element("PLAYERID").Value);
                player.PlayerName = el.Element("PLAYERNAME").Value;

                players.Add(player);
            }
            return players;
        }
    }
}