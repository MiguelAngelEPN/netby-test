using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using transaccion_service.Models;

namespace transaccion_service.Dto
{
    public class TransaccionDto
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Fecha { get; set; }

        [Required]
        public TipoTransaccion TipoTransaccion { get; set; }

        [Required]
        public int ProductoId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0.")]
        public int Cantidad { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "El precio unitario no puede ser negativo.")]
        public decimal PrecioUnitario { get; set; }

        [MaxLength(255)]
        public string? Detalle { get; set; }
    }
}