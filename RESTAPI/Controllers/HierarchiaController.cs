using System;
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
        public void PostHierarchia(hierarchia uzol) {
            using (firmaEntities f = new firmaEntities()) {
                int level = 1;
                int? smernikNaOtca = uzol.patriDo; //na zistenie urovne pridavaneho uzla v hierarchii 
                while (smernikNaOtca != null) {
                    smernikNaOtca = f.hierarchia.FirstOrDefault(u => u.id == smernikNaOtca).patriDo;
                    level++;
                }
                if (level <= 4 && level > 1) {
                    f.hierarchia.Add(new hierarchia() {
                        id = uzol.id,
                        nazov = uzol.nazov,
                        patriDo = uzol.patriDo,
                        //ak veduci na ktoreho sa uzol odkazuje nie je v tabulke zamestnanci, veduci bude null
                        veduci = f.zamestnanci.FirstOrDefault(z => z.id == uzol.veduci)?.id 
                    });
                    f.SaveChanges();
                }
            }
        }
        
        [HttpPut]
        public void PutHierarchia(int id, hierarchia noveNastavenia) {
            using (firmaEntities f = new firmaEntities()) {
                var najdeny = f.hierarchia.FirstOrDefault(u => u.id == id);
                if (najdeny != null) {
                    najdeny.nazov = noveNastavenia.nazov;
                    //patriDo = uzol.patriDo,
                    if (najdeny.veduci != noveNastavenia.veduci) {
                        najdeny.veduci = f.zamestnanci.FirstOrDefault(z => z.id == noveNastavenia.veduci)?.id;
                    }
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