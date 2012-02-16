using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SqlDatabase;

namespace MvcWebRole.Controllers
{
    public class BaseController : Controller
    {
        protected DatabaseInterface _database;

        /// <summary>
        /// Start a controller
        /// </summary>
        /// <param name="InitiateDatabase"></param>
        public BaseController(bool InitiateDatabase)
        {
            if (InitiateDatabase)
                _database = new DatabaseInterface();
        }
    }
}
