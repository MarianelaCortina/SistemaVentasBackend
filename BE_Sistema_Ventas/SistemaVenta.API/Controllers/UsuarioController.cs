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

        [HttpPost("IniciarSesion")]
        public async Task<IActionResult> IniciarSesion([FromBody] LoginDTO login)
        {
            try
            {
                // Validar credenciales
                var sesionUsuario = await _usuarioService.ValidarCredenciales(login.Correo, login.Clave);

                // Generar token JWT
                var token = _jwtService.GenerarToken(sesionUsuario);

                // Retornar la respuesta en formato JSON
                return Ok(new
                {
                    Status = true,
                    Value = new
                    {
                        Token = token,
                        Usuario = new
                        {
                            idUsuario = sesionUsuario.IdUsuario,
                            correo = sesionUsuario.Correo,
                            Rol = sesionUsuario.RolDescripcion
                        }
                    }
                });
            }
            catch (TaskCanceledException ex) // Excepción lanzada en ValidarCredenciales
            {
                return Unauthorized(new { mensaje = ex.Message });
            }
            catch (Exception ex) // Cualquier otro error inesperado
            {
                return StatusCode(500, new { mensaje = "Error interno en el servidor", detalle = ex.Message });
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

        //[HttpPost]
        //[Route("ActualizarContrasenas")]
        //public async Task<IActionResult> ActualizarContrasenas()
        //{
        //    var rsp = new Response<string>();
        //    try
        //    {
        //        var usuarios = await _usuarioService.Lista(); // Obtén todos los usuarios
        //        foreach (var usuario in usuarios)
        //        {
        //            // Genera un hash para cada contraseña
        //            usuario.Clave = BCrypt.Net.BCrypt.HashPassword("123");
        //            //usuario.Clave = BCrypt.Net.BCrypt.HashPassword("NuevaContrasena123");
        //            await _usuarioService.Editar(usuario); // Actualiza el usuario
        //        }
        //        rsp.status = true;
        //        rsp.value = "Contraseñas actualizadas correctamente";
        //    }
        //    catch (Exception ex)
        //    {
        //        rsp.status = false;
        //        rsp.msg = ex.Message;
        //    }
        //    return Ok(rsp);
        //}

    }



}
