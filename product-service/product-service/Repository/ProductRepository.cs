using Microsoft.EntityFrameworkCore;
using product_service.Data;
using product_service.Dto;
using product_service.Models;
using product_service.Service;

namespace product_service.Repository
{
    public class ProductRepository : IProductService
    {
        private readonly AppDbContext _appDbContext;

        public ProductRepository(AppDbContext context)
        {
            _appDbContext = context;
        }
        // Funciones
        public async Task<AnswerModel> GetListProduct(int page, int amount)
        {
            try
            {
                if (page <= 0) page = 1;
                if (amount <= 0) amount = 10;

                var total = await _appDbContext.Producto.CountAsync();

                var products = await _appDbContext.Producto
                    .Skip((page - 1) * amount)
                    .Take(amount)
                    .ToListAsync();

                var totalPages = (int)Math.Ceiling(total / (double)amount);

                return new AnswerModel
                {
                    Message = "Lista obtenida correctamente",
                    Status = "success",
                    Code = 200,
                    Data = new
                    {
                        totalRegistros = total,
                        paginaActual = page,
                        cantidadPaginas = totalPages,
                        registrosPagina = products.Count, // cuántos vinieron en esta página
                        registros = products
                    }
                };
            }
            catch (Exception ex)
            {
                return new AnswerModel
                {
                    Message = $"Error al obtener la lista de productos: {ex.Message}",
                    Status = "Error",
                    Code = 500
                };
            }
        }

        public async Task<AnswerModel> DeleteProduct(int productId)
        {
            try
            {
                var existing = await _appDbContext.Producto
                    .FirstOrDefaultAsync(p => p.ProductoId == productId);

                if (existing == null)
                {
                    return new AnswerModel
                    {
                        Message = "Producto no encontrado.",
                        Status = "Error",
                        Code = 404
                    };
                }

                _appDbContext.Producto.Remove(existing);
                await _appDbContext.SaveChangesAsync();

                return new AnswerModel
                {
                    Message = "Producto eliminado exitosamente.",
                    Status = "Success",
                    Code = 200
                };
            }
            catch (Exception ex)
            {
                return new AnswerModel
                {
                    Message = $"Error al obtener la lista de productos: {ex.Message}",
                    Status = "Error",
                    Code = 500
                };
            }
        }

        public async Task<AnswerModel> RegisterProduct(ProductoDto dto)
        {
            try
            {
                var producto = new Producto
                {
                    Nombre = dto.Nombre,
                    Descripcion = dto.Descripcion,
                    Categoria = dto.Categoria,
                    Imagen = dto.Imagen,
                    Precio = dto.Precio,
                    Stock = dto.Stock,
                };

                await _appDbContext.Producto.AddAsync(producto);
                await _appDbContext.SaveChangesAsync();

                return new AnswerModel
                {
                    Message = "Producto registrado exitosamente.",
                    Status = "Success",
                    Code = 201,
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new AnswerModel
                {
                    Message = $"Error al obtener la lista de productos: {ex.Message}",
                    Status = "Error",
                    Code = 500
                };
            }
        }

        public async Task<AnswerModel> UpsateProduct(Producto dto)
        {
            try
            {

                var existing = await _appDbContext.Producto
                .FirstOrDefaultAsync(p => p.ProductoId == dto.ProductoId);

                if (existing == null)
                {
                    return new AnswerModel
                    {
                        Message = "Producto no encontrado.",
                        Status = "Error",
                        Code = 404
                    };
                }

                existing.Nombre = dto.Nombre;
                existing.Descripcion = dto.Descripcion;
                existing.Categoria = dto.Categoria;
                existing.Imagen = dto.Imagen;
                existing.Precio = dto.Precio;
                existing.Stock = dto.Stock;
                existing.FechaActualizacion = DateTime.Now;

                _appDbContext.Producto.Update(existing);
                await _appDbContext.SaveChangesAsync();

                return new AnswerModel
                {
                    Message = "Producto actualizado exitosamente.",
                    Status = "Success",
                    Code = 200,
                    Data = null,
                };

            }
            catch (Exception ex)
            {
                return new AnswerModel
                {
                    Message = $"Error al obtener la lista de productos: {ex.Message}",
                    Status = "Error",
                    Code = 500
                };
            }
        }
    }
}
