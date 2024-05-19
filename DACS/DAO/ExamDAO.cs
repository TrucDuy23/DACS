using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DACS.Models;
using PagedList;

namespace DACS.DAO
{
    public class ExamDAO
    {
        DBModel db = null;
        public ExamDAO()
        {
            db = new DBModel();
        }
        public long Insert(Exam entity)
        {
            db.Exams.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public bool Delete(int id)
        {
            try
            {
                var exam = db.Exams.Find(id);
                db.Exams.Remove(exam);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public IEnumerable<Exam> ListAllPaging(string searchString, int page, int pagesize)
        {
            IQueryable<Exam> model = db.Exams;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.ProductID.ToString().Contains(searchString) || x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.ID).ToPagedList(page, pagesize);
        }
        public Exam ViewDetail(int id)
        {

            return db.Exams.Find(id);
        }


        public bool Update(Exam entity)
        {
            try
            {
                var exam = db.Exams.Find(entity.ID);
                exam.Name = entity.Name;
                exam.Code = entity.Code;
                exam.MetaTitle = entity.MetaTitle;
                exam.QuestionList = entity.QuestionList;
                exam.AnswerList = entity.AnswerList;
                exam.ProductID = entity.ProductID;
                exam.StartDate = entity.StartDate;
                exam.EndDate = entity.EndDate;
                exam.TotalScore = entity.TotalScore;
                exam.Time = entity.Time;
                exam.TotalQuestion = entity.TotalQuestion;
                exam.QuestionEssay = entity.QuestionEssay;
                exam.UserList = entity.UserList;
                exam.ScoreList = entity.ScoreList;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }
    }
}