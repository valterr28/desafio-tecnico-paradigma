-- Tarefa 1: Script para criar as tabelas e inserir dados de exemplo
-- 
-- Descrição: Este script cria as tabelas Departamento e Pessoa,
-- e insere dados de exemplo para testar a consulta de salários.
-- Foi usado pra testar com o "https://sqliteonline.com/" então foi adicionado ao código para facilitar a execução.
--- Itens de acordo com o desafio:

-- Criando as tabelas
CREATE TABLE Departamento (
    Id INTEGER PRIMARY KEY,
    Nome TEXT
);

CREATE TABLE Pessoa (
    Id INTEGER PRIMARY KEY,
    Nome TEXT,
    Salario DECIMAL(10,2),
    DeptId INTEGER,
    FOREIGN KEY (DeptId) REFERENCES Departamento(Id)
);

-- Inserindo departamentos
INSERT INTO Departamento (Id, Nome) VALUES
(1, 'TI'),
(2, 'Vendas');

-- Inserindo pessoas
INSERT INTO Pessoa (Id, Nome, Salario, DeptId) VALUES
(1, 'Joe', 70000, 1),
(2, 'Henry', 80000, 2),
(3, 'Sam', 60000, 2),
(4, 'Max', 90000, 1);
