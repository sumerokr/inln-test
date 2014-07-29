using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace inln_test.Models
{
    public class ContextDb : DbContext
    {
        public DbSet<GoalModel> Goals { get; set; }
        public DbSet<PlayerModel> Players { get; set; }
        public DbSet<TeamModel> Teams { get; set; }
        public DbSet<CommentModel> Comments { get; set; }
        public DbSet<ImportedModel> Importeds { get; set; }
    }
}