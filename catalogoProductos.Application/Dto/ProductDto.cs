namespace catalogoProductos.Application.Dto
{
    // DTO mínimo para productos (entrada/salida)
    public class ProductDto
    {
        public int Id { get; set; }                 // id (0 si es nuevo)
        public string ProductName { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public int Stock { get; set; }
        public double Price { get; set; }
    }
}