﻿using System;
using System.Collections.Generic;
using System.Text;
using Voat.Configuration;
using Voat.Data;
using Voat.Data.Models;
using Voat.Logging;

namespace Voat.Data
{
    public class DatabaseLogger : BaseLogger
    {
        public DatabaseLogger() { }
        public DatabaseLogger(LogType logLevel)
        {
            this.LogLevel = logLevel;
        }

        protected override void ProtectedLog(ILogInformation info)
        {
            //Logging to EventLog
            using (var repo = new Repository())
            {
                repo.Log(Map(info));
            }
        }
        private EventLog Map(ILogInformation info)
        {
            EventLog e = null;
            if (info != null)
            {
                e = new EventLog();

                e.Message = info.Message;
                e.Origin = info.Origin;
                e.Type = info.Type.ToString();
                e.Category = info.Category;
                e.ActivityID = info.ActivityID?.ToString();
                e.Exception = Newtonsoft.Json.JsonConvert.SerializeObject(info.Exception, JsonSettings.FriendlySerializationSettings);
                e.Data = Newtonsoft.Json.JsonConvert.SerializeObject(info.Data, JsonSettings.FriendlySerializationSettings);
                e.CreationDate = Repository.CurrentDate;
                e.UserName = info.UserName;
            }

         

            return e;
        }
    }
}
