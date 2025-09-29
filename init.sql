IF DB_ID('NetbyDB') IS NULL
BEGIN
    CREATE DATABASE NetbyDB;
END

USE NetbyDB;

-- Tabla Productos
IF OBJECT_ID('dbo.Productos', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Productos (
        ProductoId INT IDENTITY(1,1) PRIMARY KEY,
        Nombre NVARCHAR(100) NOT NULL,
        Descripcion NVARCHAR(255),
        Categoria NVARCHAR(100),
        Imagen NVARCHAR(255),
        Precio DECIMAL(10,2) NOT NULL CHECK (Precio >= 0),
        Stock INT NOT NULL DEFAULT 0 CHECK (Stock >= 0),
        FechaCreacion DATETIME DEFAULT GETDATE(),
        FechaActualizacion DATETIME DEFAULT GETDATE()
    );
END

-- Datos de ejemplo para la tabla Productos
IF NOT EXISTS (SELECT 1 FROM dbo.Productos)
BEGIN
    INSERT INTO dbo.Productos (Nombre, Descripcion, Categoria, Imagen, Precio, Stock)
    VALUES
    ('Laptop', 'Laptop de 15 pulgadas con 16GB RAM y 512GB SSD', 'Electrónica', 'laptop.png', 999.99, 10),
    ('Mouse', 'Mouse inalámbrico ergonómico', 'Accesorios', 'mouse.png', 25.50, 100),
    ('Teclado', 'Teclado mecánico retroiluminado', 'Accesorios', 'keyboard.png', 50.00, 50),
    ('Monitor', 'Monitor 24 pulgadas Full HD', 'Electrónica', 'monitor.png', 199.99, 20),
    ('Auriculares', 'Auriculares con cancelación de ruido', 'Accesorios', 'headphones.png', 75.00, 40);
END

-- Tabla Transacciones
IF OBJECT_ID('dbo.Transacciones', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Transacciones (
        TransaccionId INT IDENTITY(1,1) PRIMARY KEY,
        Fecha DATETIME NOT NULL DEFAULT GETDATE(),
        TipoTransaccion VARCHAR(10) NOT NULL CHECK (TipoTransaccion IN ('COMPRA', 'VENTA')),
        ProductoId INT NOT NULL,
        Cantidad INT NOT NULL CHECK (Cantidad > 0),
        PrecioUnitario DECIMAL(10,2) NOT NULL CHECK (PrecioUnitario >= 0),
        PrecioTotal AS (Cantidad * PrecioUnitario) PERSISTED,
        Detalle NVARCHAR(255)
        -- 🔽 No coma aquí
        CONSTRAINT FK_Transacciones_Productos FOREIGN KEY (ProductoId)
            REFERENCES dbo.Productos (ProductoId)
            ON DELETE CASCADE
    );
END