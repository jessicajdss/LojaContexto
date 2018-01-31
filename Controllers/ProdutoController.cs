using System.Collections.Generic;
using System.Linq;
using LojaWebEF.Dados;
using LojaWebEF.Models;
using Microsoft.AspNetCore.Mvc;

namespace LojaWebEF.Controllers
{
    [Route("api/[controller]")]
    public class ProdutoController: Controller
    {
        Produto produto = new Produto();

        readonly LojaContexto contexto;

        public ProdutoController(LojaContexto contexto){
            this.contexto = contexto;
        }

        [HttpGet]
        public IEnumerable<Produto> Listar(){
            return contexto.Produto.ToList();            
        }

        [HttpGet("{id}")]
        public Produto Listar(int id){
            return contexto.Produto.Where(x=>x.IdProduto==id).FirstOrDefault();
        }

        [HttpPost]
        public IActionResult Cadastrar ([FromBody] Produto produto) {

            if (!ModelState.IsValid) {
                return BadRequest (ModelState);
            }
            contexto.Produto.Add(produto);
            int x = contexto.SaveChanges ();
            if (x > 0)
                return Ok ();
            else
                return BadRequest ();
        }
    }
}