using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using inln_test.Models;
using System.Data.Objects;
using System.Xml.Linq;
using System.Globalization;

namespace inln_test.Controllers
{
    public class HomeController : Controller
    {
        public ContextDb db = new ContextDb();

        public ActionResult Index(string date = "", int id = 1)
        {
            ViewBag.CurrentDate = DateTime.Now.Date;
            ViewBag.TotalPages = 0;
            IEnumerable<GoalModel> allGoals = db.Goals;
            IEnumerable<GoalModel> model = null;
            if (date != "")
            {
                DateTime tDate = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture).Date;
                ViewBag.CurrentDate = tDate;
                model = null;
                IEnumerable<GoalModel> goalsByDate = allGoals.Where(g => g.MatchDate == tDate);
                model = goalsByDate.OrderByDescending(g => g.MatchDate).Skip((id - 1) * 10).Take(10);
                var tPages = Math.Ceiling((decimal)goalsByDate.Count() / 10);
                ViewBag.TotalPages = tPages;
            }
            ViewBag.ActivePage = id;
            return View(model);
        }

        public ActionResult GetXml(string date)
        {
            List<DateTime> dates = new List<DateTime>();
            foreach (ImportedModel dt in db.Importeds)
            {
                dates.Add(dt.ImportedDate);
            }
            if (!dates.Contains(DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture)))
            {
                string goalsFile = "http://www.ftbl.com/filestorage/xmlimport/goals_" + DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("dd_MM_yyyy") + ".xml";
                string playersFile = "http://www.ftbl.com/filestorage/xmlimport/players.xml";
                string teamsFile = "http://www.ftbl.com/filestorage/xmlimport/teams.xml";
                List<GoalModel> goals = GoalModel.GetGoals(goalsFile);
                List<PlayerModel> players = PlayerModel.GetPlayers(playersFile);
                List<TeamModel> teams = TeamModel.GetTeams(teamsFile);
                List<int> oldPlayersId = new List<int>();
                foreach (var pl in db.Players)
                {
                    oldPlayersId.Add(pl.PlayerId);
                }
                List<int> oldTeamsId = new List<int>();
                foreach (var tm in db.Teams)
                {
                    oldTeamsId.Add(tm.TeamId);
                }
                foreach (GoalModel gl in goals)
                {
                    db.Goals.Add(gl);
                    PlayerModel player = players.Find(p => p.PlayerId == gl.PlayerId);
                    if (!oldPlayersId.Contains(player.PlayerId))
                        db.Players.Add(player);
                    TeamModel team = teams.Find(t => t.TeamId == gl.TeamId);
                    if (!oldTeamsId.Contains(team.TeamId))
                        db.Teams.Add(team);
                }
                ImportedModel imported = new ImportedModel();
                imported.ImportedDate = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                db.Importeds.Add(imported);
                db.SaveChanges();
            }
            return View();
        }

        [HttpGet]
        public ActionResult CreateComment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateComment(int id, CommentModel newComment)
        {
            newComment.GoalModelId = id;
            db.Comments.Add(newComment);
            db.SaveChanges();

            return View();
        }

        public ActionResult GetComment(int id)
        {
            CommentModel model = db.Goals.Find(id).Comment.Last();
            return View(model);
        }

        public ActionResult GetComments(int id)
        {
            IEnumerable<CommentModel> model = db.Goals.Find(id).Comment.OrderByDescending(c => c.CommentId);
            return View(model);
        }
    }
}
