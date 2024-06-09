using Microsoft.AspNetCore.Mvc;
using MvcUsers.Models;
using MvcUsers.Services;

namespace MvcUsers.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioService _service;
        public UsuarioController(UsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult ListarUsuarios()
        {
            var usuarios = _service.ListarUsuarios();
            return Ok(usuarios);
        }

        [HttpDelete("{id}")]
        public IActionResult ApagarUsuario(int id)
        {
            try
            {
                bool apagado = _service.Deletar(id);
                if (apagado)
                {
                    return Ok(new { Message = "Usuário apagado com sucesso!" });
                }
                return NotFound(new { Message = "Usuário não encontrado." });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { Message = $"Erro ao apagar usuário: {ex.Message}" });
            }
        }


        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("User") != "Authenticated")
            {
                return RedirectToAction("Login", "Home");
            }

            List<UsuarioModel> contatos = _service.ListarUsuarios();
            return View(contatos);
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(UsuarioModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _service.CadastrarUsuario(usuario);
                    TempData["MensagemSucesso"] = $"Pessoa cadastrada com sucesso, código {usuario.Codigo}";
                    return RedirectToAction("Index");
                }

                return View(usuario);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não foi possível cadastrar o usuário. Detalhes: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Editar(int id)
        {
            UsuarioModel usuario = _service.ListarPorId(id);
            return View(usuario);
        }

        [HttpPost]
        public IActionResult Alterar(UsuarioModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _service.Editar(usuario);
                    TempData["MensagemSucesso"] = $"Usuario alterado com sucesso.";
                    return RedirectToAction("Index");
                }

                return View("Editar", usuario);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não foi possível alterar o usuário. Detalhes: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _service.Deletar(id);

                if (apagado)
                {
                    TempData["MensagemSucesso"] = $"Usuario apagado com sucesso.";
                }
                else
                {
                    TempData["MensagemErro"] = $"Ops, não foi possível apagar o usuário.";
                }

                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não foi possível apagar o usuário. Detalhes: {erro.Message}";
                return RedirectToAction("Index");
            }
        }




        [HttpPost("api/cadastrar")]
        public IActionResult CadastrarApi([FromBody] UsuarioModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _service.CadastrarUsuario(usuario);
                    var response = new
                    {
                        Message = $"Pessoa cadastrada com sucesso, código {usuario.Codigo}",
                        Codigo = usuario.Codigo
                    };
                    return Ok(response);
                }
                return BadRequest(ModelState);
            }
            catch (Exception erro)
            {
                return StatusCode(500, new { Message = $"Ops, não foi possível cadastrar o usuário. Detalhes: {erro.Message}" });
            }
        }
    }
}

