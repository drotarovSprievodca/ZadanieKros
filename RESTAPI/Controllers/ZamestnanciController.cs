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
        //GET api/zamestnanci
        [HttpGet]
        public IEnumerable<zamestnanci> GetZamestnanci() {
            using (firmaEntities f = new firmaEntities()) {
                f.Configuration.ProxyCreationEnabled = false;
                return f.zamestnanci.ToList();
            }
        }
        /*POST api/zamestnanci
        {
            "titul": null,
            "meno": "Jan",
            "priezvisko": "Tomas"
            "telefon": "1597846328"
            "email": "jan98@gmail.com"
        }*/
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
        /*PUT api/zamestnanci/1
        {
            "titul": "Ing.",
            "meno": "Dusan",
            "priezvisko": "Ruzbarsky",
            "telefon": "9899999991",
            "email": "ruzbarsky87@gmail.com"
        }*/
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
        //DELETE api/zamestnanci/12
        [HttpDelete]
        public IHttpActionResult DeleteZamestnanci(int id) {
            if (id <= 0)
                return BadRequest("Zle id");
            using (firmaEntities f = new firmaEntities()) {
                var naZmazanie = f.zamestnanci.FirstOrDefault(z => z.id == id);
                if (naZmazanie != null) {
                    //v uzle, v ktorom bol zamestnanec veduci sa za veduceho nastavi null
                    foreach (var uz in f.hierarchia.Where(u => u.veduci == naZmazanie.id)) {
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
