-- Tarefa 1: Consulta SQL para encontrar colaboradores com maior salário por departamento
-- 
-- Descrição: Esta consulta retorna todos os colaboradores com o maior salário em cada departamento.
-- Em caso de empate de salários, todos os colaboradores com o salário máximo serão retornados.
-- Utiliza MAX() com subconsulta para identificar o maior salário por departamento.

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
ORDER BY d.Nome, p.Nome;
