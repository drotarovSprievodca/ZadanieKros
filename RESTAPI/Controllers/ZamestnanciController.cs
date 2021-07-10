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
        [HttpGet]
        public IEnumerable<zamestnanci> GetZamestnanci()
        {
            using (firmaEntities f = new firmaEntities()) {
                f.Configuration.ProxyCreationEnabled = false;
                return f.zamestnanci.ToList();
            }
        }
        [HttpPost]
        public void PostZamestnanci(zamestnanci zamestnanec)
        {
            using (firmaEntities f = new firmaEntities())
            {
                f.zamestnanci.Add(new zamestnanci() {
                    id = zamestnanec.id,
                    titul = zamestnanec.titul,
                    meno = zamestnanec.meno,
                    priezvisko = zamestnanec.priezvisko,
                    telefon = zamestnanec.telefon,
                    email = zamestnanec.email
                });
                f.SaveChanges();
            }
        }
        [HttpPut]
        public void PutZamestnanci(int id, zamestnanci upravovany)
        {
            using (firmaEntities f = new firmaEntities())
            {
                var najdeny = f.zamestnanci.FirstOrDefault(z => z.id == id);
                if (najdeny != null)
                {
                    najdeny.titul = upravovany.titul;
                    najdeny.meno = upravovany.meno;
                    najdeny.priezvisko = upravovany.priezvisko;
                    najdeny.telefon = upravovany.telefon;
                    najdeny.email = upravovany.email;

                    f.SaveChanges();
                }
            }
        }
        [HttpDelete]
        public void DeleteZamestnanci(int id)
        {
            using (firmaEntities f = new firmaEntities())
            {
                var mazany = f.zamestnanci.FirstOrDefault(z => z.id == id);
                if (mazany != null)
                {
                    f.zamestnanci.Remove(mazany);
                    f.SaveChanges();
                }
            }
        }
    }
}
