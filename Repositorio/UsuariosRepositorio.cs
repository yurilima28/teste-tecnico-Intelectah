﻿using Intelectah.Dapper;
using Intelectah.Models;


namespace Intelectah.Repositorio
{
    public class UsuariosRepositorio : IUsuariosRepositorio
    {
        private readonly BancoContext _bancoContext;

        public UsuariosRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public UsuariosModel BuscarPorLogin(string login)
        {
            return _bancoContext.Usuarios
                .FirstOrDefault(u => u.Login == login && !u.IsDeleted);
        }

        public UsuariosModel ListarPorId(int id, bool incluirExcluidos = false)
        {
            IQueryable<UsuariosModel> query = _bancoContext.Usuarios;

            if (!incluirExcluidos)
            {
                query = query.Where(u => !u.IsDeleted);
            }

            return query.FirstOrDefault(u => u.UsuarioID == id);
        }

        public List<UsuariosModel> ObterTodosUsuarios(bool incluirExcluidos = false)
        {
            IQueryable<UsuariosModel> query = _bancoContext.Usuarios;

            if (!incluirExcluidos)
            {
                query = query.Where(u => !u.IsDeleted);
            }

            return query.ToList();
        }

        public UsuariosModel AdicionarUsuario(UsuariosModel usuario)
        {
            _bancoContext.Usuarios.Add(usuario);
            _bancoContext.SaveChanges();
            return usuario;
        }

        public UsuariosModel AtualizarUsuario(UsuariosModel usuario)
        {
            _bancoContext.Usuarios.Update(usuario);
            _bancoContext.SaveChanges();
            return usuario;
        }

        public bool ApagarUsuario(int usuarioId)
        {
            var usuario = ListarPorId(usuarioId);
            if (usuario != null)
            {
                usuario.IsDeleted = true;
                _bancoContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool RestaurarUsuario(int usuarioId)
        {
            var usuario = ListarPorId(usuarioId, incluirExcluidos: true);
            if (usuario != null && usuario.IsDeleted)
            {
                usuario.IsDeleted = false;
                _bancoContext.SaveChanges();
                return true;
            }
            return false;
        }

        public UsuariosModel ObterUsuarioPorId(int usuarioId)
        {
            return ListarPorId(usuarioId);
        }

        public bool UsuarioExiste(string nomeUsuario)
        {
            return _bancoContext.Usuarios.Any(u => u.NomeUsuario == nomeUsuario && !u.IsDeleted);
        }

        public bool VerificarNomeUsuarioUnico(string nomeUsuario, int? usuarioId = null)
        {
            return !_bancoContext.Usuarios.Any(u => u.NomeUsuario == nomeUsuario && u.UsuarioID != usuarioId && !u.IsDeleted);
        }

        public bool EmailExiste(string email, int? usuarioId = null)
        {
            return _bancoContext.Usuarios.Any(u => u.Email == email && u.UsuarioID != usuarioId);
        }

        public bool LoginExiste(string login, int? usuarioId = null)
        {
            return _bancoContext.Usuarios.Any(u => u.Login == login && u.UsuarioID != usuarioId);
        }
    }
}
