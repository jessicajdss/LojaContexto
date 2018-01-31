using System.Collections.Generic;
using System.Linq;
using LojaWebEF.Dados;
using LojaWebEF.Models;
using Microsoft.AspNetCore.Mvc;

namespace LojaWebEF.Controllers
{
    [Route("api/[controller]")]
    public class PedidoController:Controller
    {
        
        Pedido pedido = new Pedido();

        readonly LojaContexto contexto;

        public PedidoController(LojaContexto contexto){
            this.contexto = contexto;
        }

        [HttpGet]
        public IEnumerable<Pedido> Listar(){
            return contexto.Pedido.ToList();            
        }

        [HttpGet("{id}")]
        public Pedido Listar(int id){
            return contexto.Pedido.Where(x=>x.IdPedido==id).FirstOrDefault();
        }

        [HttpPost]
        public IActionResult Cadastrar ([FromBody] Pedido pedido) {

            if (!ModelState.IsValid) {
                return BadRequest (ModelState);
            }
            contexto.Pedido.Add(pedido);
            int x = contexto.SaveChanges ();
            if (x > 0)
                return Ok ();
            else
                return BadRequest ();
        }
    }
}