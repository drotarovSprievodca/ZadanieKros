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
        public IEnumerable<zamestnanci> GetZamestnanci() {
            using (firmaEntities f = new firmaEntities()) {
                f.Configuration.ProxyCreationEnabled = false;
                return f.zamestnanci.ToList();
            }
        }

        [HttpPost]
        public void PostZamestnanci(zamestnanci novy) {
            using (firmaEntities f = new firmaEntities()) {
                f.zamestnanci.Add(new zamestnanci() {
                    id = novy.id,
                    titul = novy.titul,
                    meno = novy.meno,
                    priezvisko = novy.priezvisko,
                    telefon = novy.telefon,
                    email = novy.email
                });
                f.SaveChanges();
            }
        }

        [HttpPut]
        public void PutZamestnanci(int id, zamestnanci upravovany) {
            using (firmaEntities f = new firmaEntities()) {
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
        public void DeleteZamestnanci(int id) {
            using (firmaEntities f = new firmaEntities()) {
                var naZmazanie = f.zamestnanci.FirstOrDefault(z => z.id == id);
                if (naZmazanie != null)
                {
                    f.zamestnanci.Remove(naZmazanie);
                    f.SaveChanges();
                }
            }
        }
    }
}
