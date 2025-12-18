-- =============================================
-- 1. CREACIÓN DE LA BASE DE DATOS Y LA TABLA
-- =============================================

-- Crear la base de datos si no existe
IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'UserDB')
BEGIN
    CREATE DATABASE UserDB;
END
GO

USE UserDB;
GO

-- Crear la tabla Users
-- Nota: Usamos IDENTITY(1,1) para que el Nid_user sea autoincremental (1, 2, 3...)
IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL
DROP TABLE dbo.Users
GO

CREATE TABLE Users (
    Nid_user INT IDENTITY(1,1) PRIMARY KEY,
    Sname NVARCHAR(100) NOT NULL,
    Slast_name NVARCHAR(100) NOT NULL,
    Semail NVARCHAR(150) NOT NULL,
    Dcreated_at DATETIME NOT NULL DEFAULT GETDATE(), -- Fecha automática si no se envía
    Saddress NVARCHAR(200) NOT NULL,
    Nphone INT NOT NULL -- OJO: INT soporta hasta 2 mil millones. Para celulares reales se prefiere BIGINT o VARCHAR.
);
GO

-- =============================================
-- 2. EJEMPLOS DE CONSULTAS (CRUD)
-- =============================================

-- A. CREATE (Insertar datos de prueba)
-- No incluimos 'Nid_user' porque es automático
INSERT INTO Users (Sname, Slast_name, Semail, Dcreated_at, Saddress, Nphone)
VALUES 
('Jeison', 'Camargo', 'jeison.dev@gmail.com', GETDATE(), 'Calle 140 # 11-20', 31012345),
('Laura', 'Martínez', 'laura.m@hotmail.com', GETDATE(), 'Carrera 7 # 100-20', 31598765),
('Carlos', 'López', 'carlos.lopez@outlook.com', GETDATE(), 'Av Suba # 120-10', 30055544),
('Ana', 'Rojas', 'ana.rojas@yahoo.com', GETDATE(), 'Calle 80 # 68-50', 32011122),
('Pedro', 'Perez', 'pedro.p@gmail.com', GETDATE(), 'Autopista Norte # 170', 31122233);
GO

-- B. READ (Consultar datos)
-- 1. Consultar todos los usuarios
SELECT * FROM Users;

-- 2. Consultar un usuario específico por ID
SELECT * FROM Users WHERE Nid_user = 1;

-- 3. Consultar usuarios creados hoy
SELECT * FROM Users WHERE CAST(Dcreated_at AS DATE) = CAST(GETDATE() AS DATE);

-- C. UPDATE (Actualizar datos)
-- Ejemplo: Cambiar la dirección y teléfono del usuario con ID 2
UPDATE Users
SET Saddress = 'Nueva Dirección 123', 
    Nphone = 30099999
WHERE Nid_user = 2;

-- Verificamos el cambio
SELECT * FROM Users WHERE Nid_user = 2;

-- D. DELETE (Eliminar datos)
-- Ejemplo: Eliminar al usuario con ID 5
DELETE FROM Users WHERE Nid_user = 5;

-- Verificamos que ya no exista
SELECT * FROM Users;
GO