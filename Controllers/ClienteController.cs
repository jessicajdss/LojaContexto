using System.Collections.Generic;
using System.Linq;
using LojaWebEF.Dados;
using LojaWebEF.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LojaWebEF.Controllers {
    [Route ("api/[controller]")]
    public class ClienteController : Controller {
        Cliente cliente = new Cliente ();

        readonly LojaContexto contexto;

        public ClienteController (LojaContexto contexto) {
            this.contexto = contexto;
        }

        [HttpGet]
        // public JsonResult Listar () {
        //     var cliente = contexto.Cliente.ToList();
        //     return Json(cliente) ;
        // }

        public IEnumerable<Cliente> Listar(){
            return contexto.Cliente.ToList();            
        }

        [HttpGet ("{id}")]
        public Cliente Listar (int id) {
            return contexto.Cliente.Where (x => x.IdCliente == id).FirstOrDefault ();
        }

        [HttpPost]
        public IActionResult Cadastrar ([FromBody] Cliente cliente) {

            if (!ModelState.IsValid) {
                return BadRequest (ModelState);
            }
            contexto.Cliente.Add (cliente);
            int x = contexto.SaveChanges ();
            if (x > 0)
                return Ok ();
            else
                return BadRequest ();
        }

    }
}