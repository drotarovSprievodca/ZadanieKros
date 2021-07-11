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
        public IHttpActionResult PostZamestnanci(zamestnanci novy) {
            if (!ModelState.IsValid)
                return BadRequest("Zle data.");
            using (firmaEntities f = new firmaEntities()) {
                f.zamestnanci.Add(new zamestnanci() {
                    titul = novy.titul,
                    meno = novy.meno,
                    priezvisko = novy.priezvisko,
                    telefon = novy.telefon,
                    email = novy.email
                });
                f.SaveChanges();
            }
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult PutZamestnanci(int id, zamestnanci upravovany) {
            if (!ModelState.IsValid)
                return BadRequest("Nie je validny model.");
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
                } else {
                    return NotFound();
                }
            }
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteZamestnanci(int id) {
            if (id <= 0)
                return BadRequest("Zle id");
            using (firmaEntities f = new firmaEntities()) {
                var naZmazanie = f.zamestnanci.FirstOrDefault(z => z.id == id);
                if (naZmazanie != null)
                {
                    foreach (var uz in f.hierarchia.Where(u => u.veduci == naZmazanie.id))
                    {
                        uz.veduci = null;
                    };
                    f.zamestnanci.Remove(naZmazanie);
                    f.SaveChanges();
                }
            }
            return Ok();
        }
    }
}
