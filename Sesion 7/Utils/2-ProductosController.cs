// 📁 Controllers/ProductosController.cs
using Microsoft.AspNetCore.Mvc;
using MiAPI.Services;
using MiAPI.DTOs;
using MiAPI.Models;

namespace MiAPI.Controllers
{
    [ApiController]  // 👈 Indica que es una API
    [Route("api/[controller]")]  // 👈 Ruta: /api/productos
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _productoService;
        
        // Inyección de dependencias (el servicio llega por constructor)
        public ProductosController(IProductoService productoService)
        {
            _productoService = productoService;
        }
        
        [HttpGet]  // GET /api/productos
        public IActionResult ObtenerTodos()
        {
            var productos = _productoService.ObtenerTodos();
            // Convertir a DTO antes de enviar
            var productosDTO = productos.Select(p => new ProductoDTO
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Precio = p.Precio,
                Categoria = p.Categoria,
                Disponible = p.Disponible,
                Stock = p.Stock
            });
            
            return Ok(productosDTO);  // 200 OK
        }
        
        [HttpGet("{id}")]  // GET /api/productos/5
        public IActionResult ObtenerPorId(int id)
        {
            var producto = _productoService.ObtenerPorId(id);
            if (producto == null)
                return NotFound($"Producto {id} no encontrado");  // 404
                
            return Ok(producto);  // 200 OK
        }
    }
}