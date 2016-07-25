using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;

namespace AttandenceApp.Areas.Common.Controllers
{
    public class CommonBaseController : Controller
    {
        public CommonBs obj;
        //
        // GET: /Common/BaseAdmin/
        public CommonBaseController()
        {
            obj = new CommonBs();
        }
    }
}