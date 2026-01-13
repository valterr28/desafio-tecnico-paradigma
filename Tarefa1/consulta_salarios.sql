-- Tarefa 1: Consulta SQL para encontrar colaboradores com maior salário por departamento
-- 
-- Descrição: Esta consulta retorna o colaborador com o maior salário em cada departamento.
-- Utiliza ROW_NUMBER() com PARTITION BY para identificar o maior salário por departamento.

-- Solução 1: Usando ROW_NUMBER() (Recomendada)
-- Esta abordagem é eficiente e clara, identificando o primeiro registro (maior salário) de cada departamento
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

-- Solução 2: Usando MAX() com subconsulta (Alternativa)
-- Esta abordagem usa uma subconsulta para encontrar o maior salário por departamento
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

-- Solução 3: Usando RANK() (Alternativa - pode retornar múltiplos resultados em caso de empate)
-- Esta solução retorna todos os colaboradores que têm o maior salário (em caso de empate)
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

-- Nota: A Solução 1 (ROW_NUMBER()) é recomendada pois garante um único resultado por departamento,
-- mesmo em caso de empate de salários. A Solução 3 (RANK()) retornaria todos os empates.
