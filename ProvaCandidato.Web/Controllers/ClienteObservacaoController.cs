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
    public class ClienteObservacaoController : GenericaController
    {
        private ContextoPrincipal db = new ContextoPrincipal();

        // GET: ClienteObservacaos
        public ActionResult Index(int? id)
        {
            Cliente cliente = db.Clientes.Find(id);
            return View(db.ClienteObservacao.ToList().Where(x => x.Cliente == cliente));
        }

        // GET: ClienteObservacaos/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: ClienteObservacaos/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string observacao, int id)
        {
            Cliente cliente = db.Clientes.Find(id);

            var obs = new ClienteObservacao
            {
                Observacao = observacao,
                Cliente = cliente
            };

            if (ModelState.IsValid)
            {
                db.ClienteObservacao.Add(obs);
                db.SaveChanges();
                MessageHelper.DisplaySuccessMessage(this, "Observação incluída com sucesso");
                return RedirectToAction("Index", "Clientes");
            }

            return View(obs);
        }

        // GET: ClienteObservacaos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClienteObservacao clienteObservacao = db.ClienteObservacao.Find(id);
            if (clienteObservacao == null)
            {
                return HttpNotFound();
            }
            return View(clienteObservacao);
        }

        // POST: ClienteObservacaos/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Observacao")] ClienteObservacao clienteObservacao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clienteObservacao).State = EntityState.Modified;
                db.SaveChanges();
                MessageHelper.DisplaySuccessMessage(this, "Observação editada com sucesso");
                return RedirectToAction("Index", "Clientes");
            }
            return View(clienteObservacao);
        }

        // GET: ClienteObservacaos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClienteObservacao clienteObservacao = db.ClienteObservacao.Find(id);
            if (clienteObservacao == null)
            {
                return HttpNotFound();
            }
            return View(clienteObservacao);
        }

        // POST: ClienteObservacaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClienteObservacao clienteObservacao = db.ClienteObservacao.Find(id);
            db.ClienteObservacao.Remove(clienteObservacao);
            db.SaveChanges();
            MessageHelper.DisplaySuccessMessage(this, "Observação excluída com sucesso");
            return RedirectToAction("Index", "Clientes");
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
