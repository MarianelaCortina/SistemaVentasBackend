using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.API.Utilidad;
using SistemaVenta.BLL.Servicios;
using Microsoft.AspNetCore.Authorization;
using SistemaVenta.Model;

namespace SistemaVenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IJwtService  _jwtService;

        public UsuarioController(IUsuarioService usuarioService, IJwtService jwtService)
        {
            _usuarioService = usuarioService;
            _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
        }

        
        [HttpGet("Lista")]
        public async Task<IActionResult> Lista()
        {
            var rsp = new Response<List<UsuarioDTO>>();

            try
            {
                rsp.status = true;
                rsp.value = await _usuarioService.Lista();
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }
            return Ok(rsp);
        }
        
        [AllowAnonymous]
        [HttpPost("IniciarSesion")]
        public async Task<IActionResult> IniciarSesion([FromBody] LoginDTO login)
        {
            try
            {
                // Validar credenciales
                var sesionUsuario = await _usuarioService.ValidarCredenciales(login.Correo, login.Clave);

                if (sesionUsuario == null)
                {
                    // Retornar una respuesta válida que Angular pueda manejar
                    return Ok(new { status = false, msg = "Credenciales incorrectas" });
                }

                // Generar token JWT
                var token = _jwtService.GenerarToken(sesionUsuario);

                // Retornar la respuesta en formato JSON
                return Ok(new
                {
                    status = true,
                    value = new
                    {
                        token = token,
                        idUsuario = sesionUsuario.IdUsuario,
                        correo = sesionUsuario.Correo,
                        rol = sesionUsuario.RolDescripcion
                        
                    }
                });
            }
            catch (TaskCanceledException ex) // Excepción lanzada en ValidarCredenciales
            {
                return Unauthorized(new { status = false, mensaje = ex.Message });
            }
            catch (Exception ex) // Cualquier otro error inesperado
            {
                return StatusCode(500, new { status = false, mensaje = "Error interno en el servidor", detalle = ex.Message });
            }
        }



        [HttpPost]
        [Route("Guardar")]

        public async Task<IActionResult> Guardar([FromBody] UsuarioDTO usuario)
        {
            var rsp = new Response<UsuarioDTO>();

            try
            {
                rsp.status = true;
                rsp.value = await _usuarioService.Crear(usuario);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }
            return Ok(rsp);
        }

        [HttpPut]
        [Route("Editar")]

        public async Task<IActionResult> Editar([FromBody] UsuarioDTO usuario)
        {
            var rsp = new Response<bool>();

            try
            {
                rsp.status = true;
                rsp.value = await _usuarioService.Editar(usuario);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }
            return Ok(rsp);
        }
        

        [HttpDelete]
        [Route("Eliminar/{id:int}")]

        public async Task<IActionResult> Eliminar(int id)
        {
            var rsp = new Response<bool>();
                                                                                         
            try
            {
                rsp.status = true;
                rsp.value = await _usuarioService.Eliminar(id);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }
            return Ok(rsp);
        }

        

    }



}
