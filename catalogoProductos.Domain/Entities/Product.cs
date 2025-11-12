namespace catalogoProductos.Domain.Entities;

// Entidad Producto mínima requerida por HU3.
public class Product
{
    public int Id { get; set; }                    // PK
    public string ProductName { get; set; } = string.Empty; // nombre del producto
    public string Code { get; set; } = string.Empty;        // código identificador
    public int Stock { get; set; }                 // cantidad en inventario
    public double Price { get; set; }              // precio
}

// Por qué?
// Se necesita CRUD de productos; estos campos son
// suficientes para las operaciones básicas.