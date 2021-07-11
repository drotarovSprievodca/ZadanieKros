﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FirmaModel;

namespace RESTAPI.Controllers 
{
    public class HierarchiaController : ApiController 
    {
        [HttpGet]
        public IEnumerable<hierarchia> GetHierarchia() {
            using (firmaEntities f = new firmaEntities()) {
                f.Configuration.ProxyCreationEnabled = false;
                return f.hierarchia.ToList();
            }
        }

        [HttpPost]
        public IHttpActionResult PostHierarchia(hierarchia uzol) {
            if (!ModelState.IsValid)
                return BadRequest("Zle data.");
            using (firmaEntities f = new firmaEntities()) {
                if (uzol.uroven <= 4 && uzol.uroven > 0) {
                    hierarchia otec = f.hierarchia.FirstOrDefault(u => u.id == uzol.patriDo);
                    int patriPOD = -1;
                    if (otec != null) {
                        if (uzol.uroven - otec.uroven == 1) {
                            patriPOD = uzol.patriDo;
                        }
                    }
                    if (uzol.uroven == 1) {
                        patriPOD = 0;
                    }
                    if (patriPOD != -1) {
                        f.hierarchia.Add(new hierarchia() {
                            nazov = uzol.nazov,
                            patriDo = patriPOD,
                            uroven = uzol.uroven,
                            //ak veduci na ktoreho sa uzol odkazuje nie je v tabulke zamestnanci, veduci bude null
                            veduci = f.zamestnanci.FirstOrDefault(z => z.id == uzol.veduci)?.id
                        });
                        f.SaveChanges();
                    }
                }
            }
            return Ok();
        }
        
        [HttpPut]
        public IHttpActionResult PutHierarchia(int id, hierarchia noveNastavenia) {
            if (!ModelState.IsValid)
                return BadRequest("Nie je validny model.");
            using (firmaEntities f = new firmaEntities()) {
                var najdeny = f.hierarchia.FirstOrDefault(u => u.id == id);
                if (najdeny != null) {
                    najdeny.nazov = noveNastavenia.nazov;
                    if (najdeny.uroven - f.hierarchia.FirstOrDefault(u => u.id == noveNastavenia.patriDo).uroven == 1)
                        najdeny.patriDo = noveNastavenia.patriDo;

                    if (najdeny.veduci != noveNastavenia.veduci) 
                        najdeny.veduci = f.zamestnanci.FirstOrDefault(z => z.id == noveNastavenia.veduci)?.id;
                    f.SaveChanges();
                } else {
                    return NotFound();
                }
            }
            return Ok();
        }
        
        [HttpDelete]
        public IHttpActionResult DeleteHierarchia(int id) {
            if (id <= 0)
                return BadRequest("Nie je spravne id.");
            using (firmaEntities f = new firmaEntities()) {
                var naZmazanie = f.hierarchia.FirstOrDefault(u => u.id == id);
                if (naZmazanie != null)
                {
                    vymazPodstrom(f, naZmazanie);
                    f.SaveChanges();
                }
            }
            return Ok();
        }

        //pomocna metoda na mazanie
        private void vymazPodstrom(firmaEntities f, hierarchia uzol) {
            foreach (var uz in f.hierarchia.Where(u => u.patriDo == uzol.id)) {
                vymazPodstrom(f, uz); 
            };
            f.hierarchia.Remove(uzol);
        }
    }
}