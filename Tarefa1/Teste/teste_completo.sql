IF OBJECT_ID('Departamento', 'U') IS NOT NULL
    DROP TABLE Departamento;

CREATE TABLE Departamento (
    Id INT PRIMARY KEY,
    Nome VARCHAR(50) NOT NULL
);

IF OBJECT_ID('Pessoa', 'U') IS NOT NULL
    DROP TABLE Pessoa;

CREATE TABLE Pessoa (
    Id INT PRIMARY KEY,
    Nome VARCHAR(50) NOT NULL,
    Salario DECIMAL(10, 2) NOT NULL,
    DeptId INT NOT NULL,
    FOREIGN KEY (DeptId) REFERENCES Departamento(Id)
);

INSERT INTO Departamento (Id, Nome) VALUES
    (1, 'TI'),
    (2, 'Vendas');

INSERT INTO Pessoa (Id, Nome, Salario, DeptId) VALUES
    (1, 'Joe', 70000, 1),
    (2, 'Henry', 80000, 2),
    (3, 'Sam', 60000, 2),
    (4, 'Max', 90000, 1);

PRINT '=== Dados da tabela Departamento ===';
SELECT * FROM Departamento;

PRINT '=== Dados da tabela Pessoa ===';
SELECT * FROM Pessoa;

PRINT '';
PRINT '=== SOLUÇÃO 1: Usando ROW_NUMBER() (Recomendada) ===';
SELECT 
    d.Nome AS Departamento,
    p.Nome AS Pessoa,
    p.Salario
FROM (
    SELECT 
        Id,
        Nome,
        Salario,
        DeptId,
        ROW_NUMBER() OVER (PARTITION BY DeptId ORDER BY Salario DESC) AS rn
    FROM Pessoa
) p
INNER JOIN Departamento d ON p.DeptId = d.Id
WHERE p.rn = 1
ORDER BY d.Nome;

PRINT '';
PRINT '=== SOLUÇÃO 2: Usando MAX() com subconsulta ===';
SELECT 
    d.Nome AS Departamento,
    p.Nome AS Pessoa,
    p.Salario
FROM Pessoa p
INNER JOIN Departamento d ON p.DeptId = d.Id
INNER JOIN (
    SELECT 
        DeptId,
        MAX(Salario) AS MaxSalario
    FROM Pessoa
    GROUP BY DeptId
) maxSal ON p.DeptId = maxSal.DeptId AND p.Salario = maxSal.MaxSalario
ORDER BY d.Nome;

PRINT '';
PRINT '=== SOLUÇÃO 3: Usando RANK() ===';
SELECT 
    d.Nome AS Departamento,
    p.Nome AS Pessoa,
    p.Salario
FROM (
    SELECT 
        Id,
        Nome,
        Salario,
        DeptId,
        RANK() OVER (PARTITION BY DeptId ORDER BY Salario DESC) AS rk
    FROM Pessoa
) p
INNER JOIN Departamento d ON p.DeptId = d.Id
WHERE p.rk = 1
ORDER BY d.Nome;

PRINT '';
PRINT '=== TESTE ADICIONAL: Inserindo pessoa com salário igual ao máximo ===';

INSERT INTO Pessoa (Id, Nome, Salario, DeptId) VALUES
    (5, 'Ana', 90000, 1);

PRINT '=== Resultado com ROW_NUMBER() (retorna apenas 1 por dept) ===';
SELECT 
    d.Nome AS Departamento,
    p.Nome AS Pessoa,
    p.Salario
FROM (
    SELECT 
        Id,
        Nome,
        Salario,
        DeptId,
        ROW_NUMBER() OVER (PARTITION BY DeptId ORDER BY Salario DESC) AS rn
    FROM Pessoa
) p
INNER JOIN Departamento d ON p.DeptId = d.Id
WHERE p.rn = 1
ORDER BY d.Nome;

PRINT '=== Resultado com RANK() (retorna todos os empates) ===';
SELECT 
    d.Nome AS Departamento,
    p.Nome AS Pessoa,
    p.Salario
FROM (
    SELECT 
        Id,
        Nome,
        Salario,
        DeptId,
        RANK() OVER (PARTITION BY DeptId ORDER BY Salario DESC) AS rk
    FROM Pessoa
) p
INNER JOIN Departamento d ON p.DeptId = d.Id
WHERE p.rk = 1
ORDER BY d.Nome;
