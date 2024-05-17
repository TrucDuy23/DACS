/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DACS.Models;
using PagedList;

namespace DACS.DAO
{
    public class QuestionDAO
    {
        DBModel db = null;
        public QuestionDAO()
        {
            db = new DBModel();
        }
    }
    public long Insert(Question entity)
    {
        db.Questions.Add(entity);
        db.SaveChanges();
        return entity.ID;
    }
    public bool Delete(int id)
    {
        try
        {
            var question = db.Questions.Find(id);
            db.Questions.Remove(question);
            db.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    public IEnumerable<Question> ListAllPaging(string searchString, int page, int pagesize)
    {
        IQueryable<Question> model = db.Questions;
        if (!string.IsNullOrEmpty(searchString))
        {
            model = model.Where(x => x.Content.Contains(searchString) || x.Name.Contains(searchString));
        }
        return model.OrderByDescending(x => x.ID).ToPagedList(page, pagesize);
    }
    public Question ViewDetail(int id)
    {

        return db.Questions.Find(id);
    }
    public bool Update(Question entity)
    {
        try
        {
            var question = db.Questions.Find(entity.ID);
            question.Name = entity.Name;
            question.Content = entity.Content;
            question.Answer = entity.Answer;
            question.ProductID = entity.ProductID;
            db.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;

        }
    }
}*/