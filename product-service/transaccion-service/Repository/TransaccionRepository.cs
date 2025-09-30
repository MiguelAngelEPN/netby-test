using transaccion_service.Dto;
using transaccion_service.Models;
using transaccion_service.Service;
using transaccion_service.Data;
using Microsoft.EntityFrameworkCore;

namespace transaccion_service.Repository
{
    public class TransaccionRepository : ITransaccionService
    {
        private readonly AppDbContext _appDbContext;
        public TransaccionRepository(AppDbContext context)
        {
            _appDbContext = context;
        }
        public async Task<AnswerModel> GetListTransaccion(int page, int amount)
        {
            try
            {
                if (page <= 0) page = 1;
                if (amount <= 0) amount = 10;

                var total = await _appDbContext.Transaccion.CountAsync();

                var transacciones = await _appDbContext.Transaccion
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
                        registrosPagina = transacciones.Count, // cuántos vinieron en esta página
                        registros = transacciones
                    }
                };

            }
            catch (Exception ex)
            {
                return new AnswerModel
                {
                    Message = $"Error al obtener la lista de transaccione: {ex.Message}",
                    Status = "Error",
                    Code = 500
                };
            }
        }

        public async Task<AnswerModel> RegisterTransaccion(TransaccionDto dto)
        {
            try
            {
                var producto = await _appDbContext.Producto.FindAsync(dto.ProductoId);
                if (producto == null)
                {
                    return new AnswerModel
                    {
                        Message = "Producto no encontrado",
                        Status = "error",
                        Code = 404
                    };
                }

                // Crear entidad Transaccion
                var transaccion = new Transaccion
                {
                    Fecha = DateTime.Now,
                    TipoTransaccion = dto.TipoTransaccion,
                    ProductoId = dto.ProductoId,
                    Cantidad = dto.Cantidad,
                    PrecioUnitario = dto.PrecioUnitario,
                    Detalle = dto.Detalle
                };

                // Ajustar stock según tipo de transacción
                if (dto.TipoTransaccion == TipoTransaccion.COMPRA)
                {
                    producto.Stock += dto.Cantidad;
                }
                else if (dto.TipoTransaccion == TipoTransaccion.VENTA)
                {
                    if (producto.Stock < dto.Cantidad)
                    {
                        return new AnswerModel
                        {
                            Message = "Stock insuficiente para realizar la venta",
                            Status = "error",
                            Code = 400
                        };
                    }
                    producto.Stock -= dto.Cantidad;
                }

                _appDbContext.Transaccion.Add(transaccion);
                await _appDbContext.SaveChangesAsync();

                return new AnswerModel
                {
                    Message = "Transacción registrada correctamente",
                    Status = "success",
                    Code = 201,
                    Data = transaccion
                };
            }
            catch (Exception ex)
            {
                return new AnswerModel
                {
                    Message = $"Error al registrar la transacción: {ex.Message}",
                    Status = "error",
                    Code = 500
                };
            }
        }

        public async Task<AnswerModel> UpdateTransaccion(Transaccion transaccion)
        {
            try
            {
                var existing = await _appDbContext.Transaccion.FindAsync(transaccion.TransaccionId);
                if (existing == null)
                {
                    return new AnswerModel
                    {
                        Message = "Transacción no encontrada",
                        Status = "error",
                        Code = 404
                    };
                }

                existing.Cantidad = transaccion.Cantidad;
                existing.PrecioUnitario = transaccion.PrecioUnitario;
                existing.Detalle = transaccion.Detalle;

                _appDbContext.Transaccion.Update(existing);
                await _appDbContext.SaveChangesAsync();

                return new AnswerModel
                {
                    Message = "Transacción actualizada correctamente",
                    Status = "success",
                    Code = 200,
                    Data = existing
                };
            }
            catch (Exception ex)
            {
                return new AnswerModel
                {
                    Message = $"Error al actualizar la transacción: {ex.Message}",
                    Status = "error",
                    Code = 500
                };
            }

        }

        public async Task<AnswerModel> DeleteTransaccion(int id)
        {
            try
            {
                var transaccion = await _appDbContext.Transaccion.FindAsync(id);
                if (transaccion == null)
                {
                    return new AnswerModel
                    {
                        Message = "Transacción no encontrada",
                        Status = "error",
                        Code = 404
                    };
                }

                _appDbContext.Transaccion.Remove(transaccion);
                await _appDbContext.SaveChangesAsync();

                return new AnswerModel
                {
                    Message = "Transacción eliminada correctamente",
                    Status = "success",
                    Code = 200
                };
            }
            catch (Exception ex)
            {
                return new AnswerModel
                {
                    Message = $"Error al eliminar la transacción: {ex.Message}",
                    Status = "error",
                    Code = 500
                };
            }
        }

    }
}
