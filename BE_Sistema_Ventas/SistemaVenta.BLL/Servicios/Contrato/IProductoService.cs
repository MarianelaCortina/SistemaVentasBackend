using SistemaVenta.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.BLL.Servicios.Contrato
{
    public interface IProductoService
    {
        Task<List<ProductoDTO>> Lista();
       
        Task<ProductoDTO> Crear(ProductoDTO model);
        Task<bool> Editar(ProductoDTO model);

        Task<bool> Eliminar(int id);
    }
}
