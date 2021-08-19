using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ProvaCandidato.Data;
using ProvaCandidato.Data.Entidade;
using ProvaCandidato.Helper;

namespace ProvaCandidato.Controllers
{
    public class ClientesController : GenericaController
    {
        private ContextoPrincipal db = new ContextoPrincipal();

        // GET: Clientes
        public ActionResult Index(string pesquisaNome)
        {
            var clientes = db.Clientes.Include(c => c.Cidade);

            if (!String.IsNullOrEmpty(pesquisaNome))
            {
                clientes = clientes.Where(c => c.Nome.Contains(pesquisaNome));
            }
            return View(clientes.ToList());
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            //ViewBag.CidadeId = new SelectList(db.Cidades, "Codigo", "Nome");
            return View();
        }

        // POST: Clientes/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,Nome,DataNascimento,CidadeId,Ativo")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Clientes.Add(cliente);
                db.SaveChanges();
                MessageHelper.DisplaySuccessMessage(this, "Cliente incluído com sucesso");
                return RedirectToAction("Index");
            }            

            ViewBag.CidadeId = new SelectList(db.Cidades, "Codigo", "Nome", cliente.CidadeId);
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            ViewBag.CidadeId = new SelectList(db.Cidades, "Codigo", "Nome", cliente.CidadeId);
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Nome,DataNascimento,CidadeId,Ativo")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                MessageHelper.DisplaySuccessMessage(this, "Cliente editado com sucesso");
                return RedirectToAction("Index");
            }
            ViewBag.CidadeId = new SelectList(db.Cidades, "Codigo", "Nome", cliente.CidadeId);
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Clientes.Find(id);

            var observacao = db.ClienteObservacao.ToList().Where(x => x.Cliente == cliente);

            if(observacao.Count() > 0)
            {
                MessageHelper.DisplayErrorMessage(this, "Cliente não pode ser removido até que suas observações sejam removidas.");
                return RedirectToAction("Index");
            }
            
            db.Clientes.Remove(cliente);
            db.SaveChanges();
            MessageHelper.DisplaySuccessMessage(this, "Cliente removido com sucesso");
            return RedirectToAction("Index");
        }

        public ActionResult Observation(int id)
        {
            return RedirectToAction("Index", "ClienteObservacao", new { id = id });
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
