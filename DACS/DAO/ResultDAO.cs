﻿using DACS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DACS.DAO
{
    public class ResultDAO
    {
        DBModel db = null;
        public ResultDAO()
        {
            db = new DBModel();
        }
        public Result GetByUserExamID(long UserID, long ExamID)
        {
            return db.Results.SingleOrDefault(x => x.UserID == UserID && x.ExamID == ExamID);
        }

        public bool Insert(Result entity)
        {
            db.Results.Add(entity);
            db.SaveChanges();
            return true;
        }
        public bool Update(Result entity)
        {
            try
            {
                var result = GetByUserExamID(entity.UserID, entity.ExamID);
                result.ResultQuiz = entity.ResultQuiz;
                result.ResultEssay = entity.ResultEssay;
                result.FinishTimeEssay = entity.FinishTimeEssay;
                result.FinishTimeQuiz = entity.FinishTimeQuiz;

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