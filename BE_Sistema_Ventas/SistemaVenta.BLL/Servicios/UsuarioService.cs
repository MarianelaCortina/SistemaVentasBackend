using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DAL.Repository.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.Model;
using BCrypt.Net;


namespace SistemaVenta.BLL.Servicios
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IGenericRepository<Usuario> _usuarioRepository;
        private readonly IMapper _mapper;

        public UsuarioService(IGenericRepository<Usuario> usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<List<UsuarioDTO>> Lista()
        {
            try
            {
                var queryUsuario = await _usuarioRepository.Consult();
                var listaUsuarios = queryUsuario.Include(rol => rol.IdRolNavigation).ToList();

                return _mapper.Map<List<UsuarioDTO>>(listaUsuarios);
            }
            catch 
            {
                throw;
            }
        }
      

        public async Task<SesionDTO> ValidarCredenciales(string correo, string clave)
        {
            try
            {
                // Busca el usuario por correo y que esté activo
                var queryUsuario = await _usuarioRepository.Consult(u => u.Correo == correo && u.EsActivo == true);

                var usuario = queryUsuario.Include(rol => rol.IdRolNavigation).FirstOrDefault();

                if (usuario == null)
                    throw new TaskCanceledException("El usuario no existe");

                // Verifica la contraseña
                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(clave, usuario.Clave);

                if (!isPasswordValid)
                    throw new TaskCanceledException("La contraseña es incorrecta");


                // Si pasa la validación, devuelve el usuario
                return _mapper.Map<SesionDTO>(usuario);
            }
            catch
            {
                throw;
            }

        }

        public async Task<UsuarioDTO> Crear(UsuarioDTO model)
        {
            try
            {
                var existeUsuario = await _usuarioRepository.Get(u => u.Correo == model.Correo);

                if (existeUsuario != null)
                    throw new InvalidOperationException("El correo ya está en uso");

                // Hash de la contraseña antes de guardarla
                model.Clave = BCrypt.Net.BCrypt.HashPassword(model.Clave);


                var usuarioCreado = await _usuarioRepository.Create(_mapper.Map<Usuario>(model));

                if (usuarioCreado.IdUsuario == 0)
                    throw new TaskCanceledException("No se pudo crear el usuario");

                var query = await _usuarioRepository.Consult(u => u.IdUsuario == usuarioCreado.IdUsuario);

                usuarioCreado = query.Include(rol => rol.IdRolNavigation).First();

                return _mapper.Map<UsuarioDTO>(usuarioCreado);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Editar(UsuarioDTO model)
        {
            try
            {
                var usuarioModelo = _mapper.Map<Usuario>(model);

                var usuarioEncontrado = await _usuarioRepository.Get(u => u.IdUsuario == usuarioModelo.IdUsuario);

                if (usuarioEncontrado == null)
                    throw new TaskCanceledException("El usuario no existe");

                usuarioEncontrado.NombreCompleto = usuarioModelo.NombreCompleto;
                usuarioEncontrado.Correo = usuarioModelo.Correo;
                usuarioEncontrado.IdRol = usuarioModelo.IdRol;
                usuarioEncontrado.Clave = usuarioModelo.Clave;
                usuarioEncontrado.EsActivo = usuarioModelo.EsActivo;

                bool respuesta = await _usuarioRepository.Edit(usuarioEncontrado);

                if (!respuesta)
                    throw new TaskCanceledException("No se pudo editar");

                return respuesta;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var usuarioEncontrado = await _usuarioRepository.Get(u => u.IdUsuario == id);

                if (usuarioEncontrado == null)
                    throw new TaskCanceledException("El usuario no existe");

                bool respuesta = await _usuarioRepository.Delete(usuarioEncontrado);

                if (!respuesta)
                    throw new TaskCanceledException("No se pudo eliminar");

                return respuesta;

            }
            catch
            {
                throw;
            }
        }

        
    }
}
