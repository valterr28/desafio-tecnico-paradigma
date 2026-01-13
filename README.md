# Desafio Técnico - Paradigma

Este repositório contém as soluções para o desafio técnico da Paradigma, implementando duas tarefas:

1. **Tarefa 1**: Consulta SQL para encontrar colaboradores com maior salário por departamento
2. **Tarefa 2**: Algoritmo em C# para construção de árvore binária a partir de um array

## 📋 Estrutura do Projeto

```
desafio/
├── Tarefa1/
│   └── consulta_salarios.sql    # Solução SQL
├── Tarefa2/
│   ├── No.cs                     # Classe do nó da árvore
│   ├── ArvoreBuilder.cs          # Classe principal do algoritmo
│   ├── Program.cs                # Programa principal com testes
│   └── Tarefa2.csproj            # Arquivo de projeto C#
├── README.md                      # Este arquivo
└── .gitignore                     # Arquivos ignorados pelo Git
```

## 🔍 Tarefa 1: Consulta SQL

### Descrição
Escrever uma consulta SQL para encontrar os colaboradores que têm o salário mais alto em cada departamento.

### Estrutura das Tabelas

**Tabela Pessoa:**
- `Id` (int)
- `Nome` (varchar)
- `Salario` (decimal)
- `DeptId` (int)

**Tabela Departamento:**
- `Id` (int)
- `Nome` (varchar)

### Solução

O arquivo `Tarefa1/consulta_salarios.sql` contém três abordagens diferentes:

1. **Solução 1 (Recomendada)**: Usando `ROW_NUMBER()` com `PARTITION BY`
   - Mais eficiente e clara
   - Garante um único resultado por departamento, mesmo em caso de empate

2. **Solução 2**: Usando `MAX()` com subconsulta
   - Alternativa clássica usando agregação

3. **Solução 3**: Usando `RANK()`
   - Retorna todos os colaboradores em caso de empate de salários

### Como Executar

Execute o script SQL em um banco de dados SQL Server que contenha as tabelas `Pessoa` e `Departamento` populadas com os dados de exemplo.

### Resultado Esperado

| Departamento | Pessoa | Salario |
|--------------|--------|---------|
| TI           | Max    | 90000   |
| Vendas       | Henry  | 80000   |

## 🌳 Tarefa 2: Algoritmo de Árvore Binária

### Descrição
Construir um algoritmo que cria uma árvore binária a partir de um array de inteiros sem duplicidade, seguindo as regras:

- **Raiz**: O maior valor do array
- **Galhos da esquerda**: Números à esquerda do valor raiz, em ordem decrescente
- **Galhos da direita**: Números à direita do valor raiz, em ordem decrescente

### Algoritmo

O algoritmo implementado em C# segue os seguintes passos:

1. Encontra o índice do maior valor no array (este será a raiz)
2. Divide o array em duas partes: elementos à esquerda e à direita da raiz
3. Ordena cada parte em ordem decrescente
4. Constrói recursivamente a árvore, onde cada nó tem:
   - O maior valor disponível como valor do nó
   - O restante dos valores como subárvore da direita (mantendo ordem decrescente)

### Como Executar

#### Pré-requisitos
- .NET 6.0 SDK ou superior instalado

#### Passos

1. Navegue até a pasta do projeto:
```bash
cd Tarefa2
```

2. Restaure as dependências (se necessário):
```bash
dotnet restore
```

3. Execute o programa:
```bash
dotnet run
```

### Exemplos de Execução

#### Cenário 1
**Array de entrada:** `[3, 2, 1, 6, 0, 5]`

**Resultado:**
```
Raiz: 6
Galhos da esquerda: 3, 2, 1
Galhos da direita: 5, 0

Estrutura da árvore:
└── 6
    ├── 3
    │   └── 2
    │       └── 1
    └── 5
        └── 0
```

#### Cenário 2
**Array de entrada:** `[7, 5, 13, 9, 1, 6, 4]`

**Resultado:**
```
Raiz: 13
Galhos da esquerda: 7, 5
Galhos da direita: 9, 6, 4, 1

Estrutura da árvore:
└── 13
    ├── 7
    │   └── 5
    └── 9
        └── 6
            └── 4
                └── 1
```

### Classes Implementadas

#### `No`
Representa um nó da árvore binária com:
- `Valor`: Valor inteiro armazenado no nó
- `Esquerda`: Referência para o nó filho da esquerda
- `Direita`: Referência para o nó filho da direita

#### `ArvoreBuilder`
Classe principal que contém:
- `ConstruirArvore(int[] array)`: Método principal que constrói a árvore
- `ImprimirArvore(No? raiz)`: Método para visualizar a árvore no console
- `ImprimirEmOrdem(No? raiz)`: Método para imprimir a árvore em ordem (in-order)

### Complexidade

- **Tempo**: O(n log n) devido à ordenação dos subarrays
- **Espaço**: O(n) para armazenar a árvore

## 🧪 Testes

O programa inclui testes para:
- ✅ Cenário 1: `[3, 2, 1, 6, 0, 5]`
- ✅ Cenário 2: `[7, 5, 13, 9, 1, 6, 4]`
- ✅ Casos especiais: array com um elemento, arrays ordenados, raiz no início/fim

## 📝 Observações

- O algoritmo assume que o array não contém valores duplicados
- Arrays vazios retornam `null`
- A árvore é construída de forma recursiva
- A visualização da árvore usa caracteres Unicode para melhor apresentação

## 👨‍💻 Autor

Desenvolvido como parte do processo seletivo para desenvolvedor na Paradigma.

## 📄 Licença

Este projeto foi desenvolvido exclusivamente para fins de avaliação técnica.
