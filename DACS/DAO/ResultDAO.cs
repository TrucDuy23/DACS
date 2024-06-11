using DACS.Models;
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
    }
}