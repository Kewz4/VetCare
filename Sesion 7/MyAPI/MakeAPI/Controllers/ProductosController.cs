using MakeAPI.DTOs;
using MakeAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MakeAPI.Controllers
{
    /// <summary>
    /// Controlador para gestionar todas las operaciones relacionadas con productos.
    /// Proporciona endpoints RESTful para CRUD (Create, Read, Update, Delete).
    /// </summary>
    [ApiController]
    [Route("api/[controller]")] // Ruta base: /api/productos
    public class ProductosController : ControllerBase
    {
        // Se inyecta el servicio de productos
        private readonly IProductoService _productoService;

        /// <summary>
        /// Constructor que recibe la inyección de dependencias del servicio.
        /// </summary>
        /// <param name="productoService">Servicio de gestión de productos</param>
        public ProductosController(IProductoService productoService)
        {
            _productoService = productoService ?? throw new ArgumentNullException(nameof(productoService));
        }

        /// <summary>
        /// Obtiene la lista de todos los productos.
        /// GET /api/productos
        /// </summary>
        /// <returns>
        /// 200 OK: Lista de todos los productos.
        /// </returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoDTO>>> ObtenerTodos()
        {
            try
            {
                // Se obtienen todos los productos del servicio
                var productos = await _productoService.ObtenerTodosAsync();

                // Se convierten a DTOs
                var productosDTO = productos.Select(p => new ProductoDTO
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Precio = p.Precio,
                    Categoria = p.Categoria,
                    Stock = p.Stock,
                    Disponible = p.Disponible
                }).ToList();

                return Ok(productosDTO);
            }
            catch (Exception ex)
            {
                // Registrar el error (en un proyecto real)
                return StatusCode(500, new { mensaje = "Error al obtener los productos", detalle = ex.Message });
            }
        }

        /// <summary>
        /// Obtiene un producto específico por su ID.
        /// GET /api/productos/{id}
        /// </summary>
        /// <param name="id">Identificador del producto</param>
        /// <returns>
        /// 200 OK: El producto solicitado.
        /// 404 Not Found: Si el producto no existe.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDTO>> ObtenerPorId(int id)
        {
            try
            {
                // Validación básica
                if (id <= 0)
                    return BadRequest(new { mensaje = "El ID debe ser un número positivo" });

                // Se busca el producto
                var producto = await _productoService.ObtenerPorIdAsync(id);

                if (producto == null)
                    return NotFound(new { mensaje = $"No se encontró un producto con ID {id}" });

                // Se convierte a DTO
                var productoDTO = new ProductoDTO
                {
                    Id = producto.Id,
                    Nombre = producto.Nombre,
                    Precio = producto.Precio,
                    Categoria = producto.Categoria,
                    Stock = producto.Stock,
                    Disponible = producto.Disponible
                };

                return Ok(productoDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al obtener el producto", detalle = ex.Message });
            }
        }

        /// <summary>
        /// Crea un nuevo producto.
        /// POST /api/productos
        /// </summary>
        /// <param name="crearDTO">Datos del producto a crear</param>
        /// <returns>
        /// 201 Created: El producto creado con su ID asignado.
        /// 400 Bad Request: Si los datos no son válidos.
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<ProductoDTO>> Crear([FromBody] CrearProductoDTO crearDTO)
        {
            try
            {
                // Validaciones del DTO
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (string.IsNullOrWhiteSpace(crearDTO.Nombre))
                    return BadRequest(new { mensaje = "El nombre del producto es requerido" });

                if (crearDTO.Precio <= 0)
                    return BadRequest(new { mensaje = "El precio debe ser mayor a 0" });

                if (crearDTO.Stock < 0)
                    return BadRequest(new { mensaje = "El stock no puede ser negativo" });

                // Se crea el producto en el servicio
                var productoCreado = await _productoService.CrearAsync(
                    crearDTO.Nombre,
                    crearDTO.Precio,
                    crearDTO.Categoria ?? string.Empty,
                    crearDTO.Stock
                );

                // Se convierte a DTO
                var productoDTO = new ProductoDTO
                {
                    Id = productoCreado.Id,
                    Nombre = productoCreado.Nombre,
                    Precio = productoCreado.Precio,
                    Categoria = productoCreado.Categoria,
                    Stock = productoCreado.Stock,
                    Disponible = productoCreado.Disponible
                };

                // Se retorna 201 Created con la ruta al nuevo recurso
                return CreatedAtAction(nameof(ObtenerPorId), new { id = productoDTO.Id }, productoDTO);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al crear el producto", detalle = ex.Message });
            }
        }

        /// <summary>
        /// Actualiza un producto existente.
        /// PUT /api/productos/{id}
        /// </summary>
        /// <param name="id">Identificador del producto a actualizar</param>
        /// <param name="actualizarDTO">Nuevos datos del producto</param>
        /// <returns>
        /// 200 OK: El producto actualizado.
        /// 400 Bad Request: Si los datos no son válidos.
        /// 404 Not Found: Si el producto no existe.
        /// </returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductoDTO>> Actualizar(int id, [FromBody] ActualizarProductoDTO actualizarDTO)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new { mensaje = "El ID debe ser un número positivo" });

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (string.IsNullOrWhiteSpace(actualizarDTO.Nombre))
                    return BadRequest(new { mensaje = "El nombre del producto es requerido" });

                if (actualizarDTO.Precio <= 0)
                    return BadRequest(new { mensaje = "El precio debe ser mayor a 0" });

                // Se actualiza el producto
                var productoActualizado = await _productoService.ActualizarAsync(
                    id,
                    actualizarDTO.Nombre,
                    actualizarDTO.Precio,
                    actualizarDTO.Categoria ?? string.Empty
                );

                if (productoActualizado == null)
                    return NotFound(new { mensaje = $"No se encontró un producto con ID {id}" });

                // Se convierte a DTO
                var productoDTO = new ProductoDTO
                {
                    Id = productoActualizado.Id,
                    Nombre = productoActualizado.Nombre,
                    Precio = productoActualizado.Precio,
                    Categoria = productoActualizado.Categoria,
                    Stock = productoActualizado.Stock,
                    Disponible = productoActualizado.Disponible
                };

                return Ok(productoDTO);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al actualizar el producto", detalle = ex.Message });
            }
        }

        /// <summary>
        /// Elimina un producto del sistema.
        /// DELETE /api/productos/{id}
        /// </summary>
        /// <param name="id">Identificador del producto a eliminar</param>
        /// <returns>
        /// 204 No Content: Si se eliminó exitosamente.
        /// 404 Not Found: Si el producto no existe.
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new { mensaje = "El ID debe ser un número positivo" });

                // Se elimina el producto
                var eliminado = await _productoService.EliminarAsync(id);

                if (!eliminado)
                    return NotFound(new { mensaje = $"No se encontró un producto con ID {id}" });

                // Se retorna 204 No Content (éxito sin contenido)
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al eliminar el producto", detalle = ex.Message });
            }
        }

        /// <summary>
        /// Actualiza el stock de un producto (suma o resta).
        /// PATCH /api/productos/{id}/stock
        /// </summary>
        /// <param name="id">Identificador del producto</param>
        /// <param name="cantidad">
        /// Cantidad a sumar (positivos) o restar (negativos) del stock.
        /// Ejemplo: 10 para agregar 10 unidades, -5 para restar 5 unidades.
        /// </param>
        /// <returns>
        /// 200 OK: El producto con el stock actualizado.
        /// 400 Bad Request: Si la operación no es válida.
        /// 404 Not Found: Si el producto no existe.
        /// </returns>
        [HttpPatch("{id}/stock")]
        public async Task<ActionResult<ProductoDTO>> ActualizarStock(int id, [FromQuery] int cantidad)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new { mensaje = "El ID debe ser un número positivo" });

                if (cantidad == 0)
                    return BadRequest(new { mensaje = "La cantidad debe ser diferente de 0" });

                // Se actualiza el stock
                var productoActualizado = await _productoService.ActualizarStockAsync(id, cantidad);

                if (productoActualizado == null)
                    return NotFound(new { mensaje = $"No se encontró un producto con ID {id}" });

                // Se convierte a DTO
                var productoDTO = new ProductoDTO
                {
                    Id = productoActualizado.Id,
                    Nombre = productoActualizado.Nombre,
                    Precio = productoActualizado.Precio,
                    Categoria = productoActualizado.Categoria,
                    Stock = productoActualizado.Stock,
                    Disponible = productoActualizado.Disponible
                };

                return Ok(productoDTO);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al actualizar el stock", detalle = ex.Message });
            }
        }
    }
}
