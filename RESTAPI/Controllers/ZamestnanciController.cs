using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FirmaModel;

namespace RESTAPI.Controllers
{
    public class ZamestnanciController : ApiController
    {
        public IEnumerable<zamestnanci> GetZamestnanci()
        {
            using (firmaEntities f = new firmaEntities()) {
                f.Configuration.ProxyCreationEnabled = false;
                return f.zamestnanci.ToList();
            }
        }
    }
}
