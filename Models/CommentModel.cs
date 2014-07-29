using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace inln_test.Models
{
    public interface IModel { }
    public class CommentModel : IModel
    {
        [Key]
        public int CommentId { get; set; }
        [Required]
        public string Text { get; set; }
        public int GoalModelId { get; set; }
        public virtual GoalModel Goal { get; set; }
    }

    public static class ModelExt
    {
        public static bool Save<T>(this T model) where T : class,IModel
        {
            using (ContextDb db = new ContextDb())
            {
                db.Set<T>().Add(model);
                return true;
            }
        }
    }
}