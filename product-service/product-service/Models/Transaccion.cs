using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace product_service.Models
{
    [Table("Transacciones", Schema = "dbo")]
    public class Transaccion
    {
        [Key]
        public int TransaccionId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Fecha { get; set; }

        [Required]
        [MaxLength(10)]
        public TipoTransaccion TipoTransaccion { get; set; }

        [Required]
        public int ProductoId { get; set; }

        [ForeignKey(nameof(ProductoId))]
        public Producto Producto { get; set; } = null!;

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0.")]
        public int Cantidad { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "El precio unitario no puede ser negativo.")]
        public decimal PrecioUnitario { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal PrecioTotal { get; private set; }

        [MaxLength(255)]
        public string? Detalle { get; set; }
    }

    public enum TipoTransaccion
    {
        COMPRA,
        VENTA
    }
}
