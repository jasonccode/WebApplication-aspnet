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

-- =============================================
-- 3. CREACIÓN DE LA TABLA REQUEST Y PROCEDIMIENTOS ALMACENADOS
-- =============================================

CREATE TABLE Request (
    Id_request INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(100) NOT NULL,
    Description NVARCHAR(100) NOT NULL,
    Status NVARCHAR(150) NOT NULL,
    Dcreated_at DATETIME NOT NULL DEFAULT GETDATE(), -- Fecha automática si no se envía
);
GO

-- SP: Listar Request
CREATE PROCEDURE sp_get_all_requests
AS
BEGIN
    SELECT * FROM Request;
END
GO

-- SP: Insertar Request
CREATE PROCEDURE sp_add_request
    @Title NVARCHAR(100),
    @Description NVARCHAR(100),
    @Status NVARCHAR(150)
AS
BEGIN
    INSERT INTO Request (Title, Description, Status)
    VALUES (@Title, @Description, @Status);
END
GO

-- SP: Actualizar Request
CREATE PROCEDURE sp_update_request 
    @Id_request INT,
    @Title NVARCHAR(100),
    @Description NVARCHAR(100),
    @Status NVARCHAR(150)
AS
BEGIN
    UPDATE Request
    SET Title = @Title, Description = @Description, Status = @Status
    WHERE Id_request = @Id_request;
END
GO

-- SP: Eliminar Request
CREATE PROCEDURE sp_delete_request
    @Id_request INT
AS
BEGIN
    DELETE FROM Request WHERE Id_request = @Id_request;
END
GO

--SP: Obtener Request por ID
CREATE PROCEDURE sp_get_request_by_id
    @Id_request INT
AS
BEGIN
    SELECT * FROM Request WHERE Id_request = @Id_request;
END
GO


-- =============================================
-- 4. PROCEDIMIENTOS ALMACENADOS PARA LA TABLA USERS
-- =============================================

-- SP: Listar User
CREATE PROCEDURE sp_get_all_users
AS
BEGIN
    SELECT *
    FROM Users;
END
GO

-- SP: Insertar User
CREATE PROCEDURE sp_add_user
    @Sname NVARCHAR(100),
    @Slast_name NVARCHAR(100),
    @Semail NVARCHAR(150),
    @Saddress NVARCHAR(100),
    @Nphone BIGINT
AS
BEGIN
    INSERT INTO Users
        (Sname, Slast_name, Semail, Saddress, Nphone, Dcreated_at)
    VALUES
        (@Sname, @Slast_name, @Semail, @Saddress, @Nphone, GETDATE());
END
GO

-- SP: Actualizar User
CREATE PROCEDURE sp_update_user
    @Nid_user INT,
    @Sname NVARCHAR(100),
    @Slast_name NVARCHAR(100),
    @Semail NVARCHAR(150),
    @Saddress NVARCHAR(100),
    @Nphone INT
AS
BEGIN
    UPDATE Users
    SET Sname =@Sname, Slast_name = @Slast_name, Semail = @Semail, Saddress = @Saddress, Nphone = @Nphone
    WHERE Nid_user = @Nid_user;
END
GO

-- SP: Eliminar User
CREATE PROCEDURE sp_delete_user
    @Nid_user INT
AS
BEGIN
    DELETE FROM Users WHERE Nid_user = @Nid_user;
END
GO

--SP: Obtener User por ID
CREATE PROCEDURE sp_get_user_by_id
    @Nid_user INT
AS
BEGIN
    SELECT *
    FROM Users
    WHERE Nid_user = @Nid_user;
END
GO

-- =============================================
-- 5. EJEMPLO DE RELACIONES ENTRE TABLAS
-- =============================================

CREATE TABLE Employees (
    EmployeeID INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) UNIQUE,        -- UNIQUE: No permite correos repetidos
    HireDate DATE NOT NULL,
    IsActive BIT DEFAULT 1,            -- BIT es el boolean de SQL (1=True, 0=False)
    
    -- La Relación:
    DepartmentID INT,                  -- Debe ser del mismo tipo que la PK de Departments
    
    -- CONSTRAINT (La Regla):
    CONSTRAINT FK_Employees_Departments 
    FOREIGN KEY (DepartmentID) 
    REFERENCES Departments(DepartmentID)
    ON DELETE NO ACTION -- Evita borrar un departamento si tiene empleados
);
GO