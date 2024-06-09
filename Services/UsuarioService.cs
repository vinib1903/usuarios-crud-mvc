using MvcUsers.Data;
using MvcUsers.Models;

namespace MvcUsers.Services
{
    public class UsuarioService
    {
        private readonly AppDbContext _context;
        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        public UsuarioModel CadastrarUsuario(UsuarioModel usuario)
        {
            usuario.Codigo = usuario.CPF.Substring(0, 4);

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            return usuario;
        }


        public List<UsuarioModel> ListarUsuarios()
        {
            return _context.Usuarios.ToList();
        }

        public UsuarioModel ListarPorId(int id)
        {
            return _context.Usuarios.FirstOrDefault(x => x.Id == id);
        }

        public UsuarioModel Editar(UsuarioModel usuario)
        {
            UsuarioModel usuarioDb = ListarPorId(usuario.Id);

            if (usuarioDb == null) throw new System.Exception("Não foi possível atualizar o usuário.");

            usuarioDb.Nome = usuario.Nome;
            //usuarioDb.Codigo = usuario.Codigo;
            usuarioDb.Endereco = usuario.Endereco;
            usuarioDb.Telefone = usuario.Telefone;

            _context.Usuarios.Update(usuarioDb);
            _context.SaveChanges();

            return usuarioDb;

        }

        public bool Deletar(int id)
        {
            UsuarioModel usuarioDb = ListarPorId(id);

            if (usuarioDb == null) throw new System.Exception("Não foi possível deletar o usuário.");


            _context.Usuarios.Remove(usuarioDb);
            _context.SaveChanges();

            return true;

        }
    }
}
