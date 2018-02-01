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

        // [HttpGet]
        // public IEnumerable<Cliente> Listar(){
        //     return contexto.Cliente.ToList();            
        // }

        [HttpGet ("{id}")]
        public Cliente Listar (int id) {
            return contexto.Cliente.Where (x => x.IdCliente == id).FirstOrDefault ();
        }

        //Testando Join
        [HttpGet]
        public JsonResult ListarJoin(){
            var consulta = from c in contexto.Cliente.Join(contexto.Pedido,
            c=>c.IdCliente,
            p=>p.IdCliente
            ,(c,p)=>new{Cliente = c.Nome,Pedido= p.IdPedido}) select c;
            return (Json(consulta.ToList()));
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