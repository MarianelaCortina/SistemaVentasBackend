using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DAL.Repository.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.Model;

namespace SistemaVenta.BLL.Servicios
{
    public class MenuService : IMenuService
    {
        private readonly IGenericRepository<Usuario> _usuarioRepository;
        private readonly IGenericRepository<MenuRol> _menuRolRepository;
        private readonly IGenericRepository<Menu> _menuRepository;
        private readonly IMapper _mapper;

        public MenuService(IGenericRepository<Usuario> usuarioRepository, IGenericRepository<MenuRol> menuRolRepository, IGenericRepository<Menu> menuRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _menuRolRepository = menuRolRepository;
            _menuRepository = menuRepository;
            _mapper = mapper;
        }

        public async Task<List<MenuDTO>> Lista(int idUsuario)
        {
            IQueryable<Usuario> tablaUsuario = await _usuarioRepository.Consult(u => u.IdUsuario == idUsuario);
            IQueryable<MenuRol> tablaMenuRol = await _menuRolRepository.Consult();
            IQueryable <Menu> tablaMenu = await _menuRepository.Consult();

            try
            {
                IQueryable<Menu> tablaResultado = (from u in tablaUsuario
                                                   join mr in tablaMenuRol on u.IdRol equals mr.IdRol
                                                   join m in tablaMenu on mr.IdMenu equals m.IdMenu
                                                   select m).AsQueryable();

                var listaMenus = tablaResultado.ToList();

                return _mapper.Map<List<MenuDTO>>(listaMenus);
                    
            }
            catch
            {
                throw;
            }
        }
    }
}
