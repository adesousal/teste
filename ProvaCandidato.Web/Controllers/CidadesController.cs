using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProvaCandidato.Data;
using ProvaCandidato.Data.Entidade;
using ProvaCandidato.Helper;

namespace ProvaCandidato.Controllers
{
    public class CidadesController : GenericaController
    {
        private ContextoPrincipal db = new ContextoPrincipal();

        // GET: Cidades
        public ActionResult Index()
        {
            return View(db.Cidades.ToList());
        }

        // GET: Cidades
        public ActionResult GetAjax()
        {
            var cidades = db.Cidades.Select(x => new { codigo = x.Codigo, nome = x.Nome }).Distinct();
            return Json(cidades, JsonRequestBehavior.AllowGet);
        }

        // GET: Cidades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cidade cidade = db.Cidades.Find(id);
            if (cidade == null)
            {
                return HttpNotFound();
            }
            return View(cidade);
        }

        // GET: Cidades/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cidades/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,Nome")] Cidade cidade)
        {
            var registro = db.Cidades.AsNoTracking().Where(x => x.Nome == cidade.Nome).Count();

            if (registro > 0)
            {
                MessageHelper.DisplayErrorMessage(this, $"A cidade {cidade.Nome} já está cadastrada");
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                db.Cidades.Add(cidade);
                db.SaveChanges();
                MessageHelper.DisplaySuccessMessage(this, "Cidade incluída com sucesso");
                return RedirectToAction("Index");
            }

            return View(cidade);
        }

        // GET: Cidades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cidade cidade = db.Cidades.Find(id);
            if (cidade == null)
            {
                return HttpNotFound();
            }
            return View(cidade);
        }

        // POST: Cidades/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Nome")] Cidade cidade)
        {
            var registro = db.Cidades.AsNoTracking().Where(x => x.Nome == cidade.Nome).Count();

            if (registro > 0)
            {
                MessageHelper.DisplayErrorMessage(this, $"A cidade {cidade.Nome} já está cadastrada");
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                db.Entry(cidade).State = EntityState.Modified;
                db.SaveChanges();
                MessageHelper.DisplaySuccessMessage(this, "Cidade editada com sucesso");
                return RedirectToAction("Index");
            }
            return View(cidade);
        }

        // GET: Cidades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cidade cidade = db.Cidades.Find(id);
            if (cidade == null)
            {
                return HttpNotFound();
            }
            return View(cidade);
        }

        // POST: Cidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var cliente = db.Clientes.AsNoTracking().Where(x => x.CidadeId == id).Count();

            if (cliente > 0)
            {
                MessageHelper.DisplayErrorMessage(this, "Esta cidade não pode ser removida, pois está associada a algum cliente.");
                return RedirectToAction("Index");
            }
            Cidade cidade = db.Cidades.Find(id);
            db.Cidades.Remove(cidade);
            db.SaveChanges();
            MessageHelper.DisplaySuccessMessage(this, "Cidade removida com sucesso");
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
