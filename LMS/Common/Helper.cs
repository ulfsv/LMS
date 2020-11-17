using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS.Data;
using LMS.Models;
using LMS.Common;

namespace LMS.Common
{
    public class Helper
    {
        private readonly ApplicationDbContext db;

        public Helper(ApplicationDbContext context)
        {
            db = context;
        }
        public void AddDocument(string name, string description, string storageUrl, string userId, Constants.Level level, int levelId)
        {
            // "level" is either course, module or activity
            // "levelId" is either a courseID, a moduleID or an activityID, depending on "level"

            var document = new Document
            {
                Name = name,
                Description = description,
                UploadTimeStamp = DateTime.Now,
                Storage = storageUrl,
                ApplicationUserId = userId
            };

            switch (level)
            {
                case Constants.Level.Course:
                    document.CourseId = levelId;
                    break;
                case Constants.Level.Module:
                    document.ModuleId = levelId;
                    break;
                case Constants.Level.Activity:
                    document.ActivityId = levelId;
                    break;
            }

            db.Documents.Add(document);
            db.SaveChanges();

            // TODO return something useful -
            // for example check if the create worked and then return an LmsResult
        }

        public LmsResult Succeeded(string message)
        {
            return new LmsResult
            {
                Succeeded = true,
                Message = message
            };
        }
        public LmsResult Failed(string message)
        {
            return new LmsResult
            {
                Succeeded = false,
                Message = message
            };
        }
    }
}
