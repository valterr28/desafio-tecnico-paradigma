using System;
using System.Linq;

namespace Tarefa2
{
    /// <summary>
    /// Classe responsável por construir uma árvore binária a partir de um array
    /// seguindo as regras especificadas:
    /// - Raiz: maior valor do array
    /// - Galhos da esquerda: números à esquerda do valor raiz, em ordem decrescente
    /// - Galhos da direita: números à direita do valor raiz, em ordem decrescente
    /// </summary>
    public class ArvoreBuilder
    {
        /// <summary>
        /// Ordena um array em ordem decrescente (modifica o array original)
        /// </summary>
        private static void OrdenarDecrescente(int[] array)
        {
            if (array == null || array.Length <= 1)
            {
                return;
            }

            Array.Sort(array, (a, b) => b.CompareTo(a));
        }
        /// <summary>
        /// Valida se o array é válido para construção da árvore
        /// </summary>
        private static bool EhArrayValido(int[]? array)
        {
            return array != null && array.Length > 0;
        }

        /// <summary>
        /// Encontra o índice do maior valor no array em uma única passada
        /// </summary>
        private static int EncontrarIndiceMaximo(int[] array)
        {
            int indiceMaximo = 0;
            int valorMaximo = array[0];

            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] > valorMaximo)
                {
                    valorMaximo = array[i];
                    indiceMaximo = i;
                }
            }

            return indiceMaximo;
        }

        /// <summary>
        /// Constrói uma árvore binária a partir de um array de inteiros
        /// </summary>
        /// <param name="array">Array de inteiros sem duplicidade</param>
        /// <returns>Raiz da árvore construída, ou null se o array estiver vazio</returns>
        public Knot? ConstruirArvore(int[] array)
        {
            if (!EhArrayValido(array))
            {
                return null;
            }

            if (array.Length == 1)
            {
                return new Knot(array[0]);
            }

            // Encontra o índice do maior valor (raiz) em uma única passada
            int indiceRaiz = EncontrarIndiceMaximo(array);

            // Cria o nó raiz
            Knot raiz = new Knot(array[indiceRaiz]);

            // Divide o array em partes esquerda e direita
            int[] arrayEsquerda = array.Take(indiceRaiz).ToArray();
            int[] arrayDireita = array.Skip(indiceRaiz + 1).ToArray();

            // Ordena os arrays em ordem decrescente
            OrdenarDecrescente(arrayEsquerda);
            OrdenarDecrescente(arrayDireita);

            // Constrói recursivamente os galhos
            raiz.Esquerda = ConstruirGalho(arrayEsquerda);
            raiz.Direita = ConstruirGalho(arrayDireita);

            return raiz;
        }

        /// <summary>
        /// Constrói um galho da árvore a partir de um array já ordenado em ordem decrescente
        /// </summary>
        /// <param name="array">Array ordenado em ordem decrescente</param>
        /// <returns>Raiz do galho construído, ou null se o array estiver vazio</returns>
        private Knot? ConstruirGalho(int[] array)
        {
            if (!EhArrayValido(array))
            {
                return null;
            }

            if (array.Length == 1)
            {
                return new Knot(array[0]);
            }

            // O primeiro elemento é sempre o maior (já está em ordem decrescente)
            Knot no = new Knot(array[0]);

            // O restante do array continua em ordem decrescente
            int[] resto = array.Skip(1).ToArray();

            // O próximo maior valor vai para a direita (mantém ordem decrescente)
            no.Direita = ConstruirGalho(resto);

            return no;
        }

        /// <summary>
        /// Extrai os valores dos galhos da esquerda e direita da árvore em ordem decrescente
        /// </summary>
        /// <param name="raiz">Raiz da árvore</param>
        /// <returns>Tupla contendo os valores dos galhos da esquerda e direita (em ordem decrescente)</returns>
        public (int[] galhosEsquerda, int[] galhosDireita) ObterValoresDosGalhos(Knot? raiz)
        {
            if (raiz == null)
            {
                return (Array.Empty<int>(), Array.Empty<int>());
            }

            int[] galhosEsquerda = ExtrairValores(raiz.Esquerda);
            int[] galhosDireita = ExtrairValores(raiz.Direita);

            return (galhosEsquerda, galhosDireita);
        }

        /// <summary>
        /// Extrai os valores de um galho percorrendo-o da raiz para a direita (ordem decrescente)
        /// A estrutura do galho é linear (só tem filhos à direita), então percorremos da raiz até o fim
        /// </summary>
        private int[] ExtrairValores(Knot? no)
        {
            if (no == null)
            {
                return Array.Empty<int>();
            }

            var valores = new System.Collections.Generic.List<int>();
            Knot? atual = no;
            
            // Percorre a cadeia linear da direita coletando valores (já está em ordem decrescente)
            while (atual != null)
            {
                valores.Add(atual.Valor);
                atual = atual.Direita;
            }

            return valores.ToArray();
        }

        /// <summary>
        /// Imprime a árvore de forma visual no console
        /// </summary>
        /// <param name="raiz">Raiz da árvore a ser impressa</param>
        public void ImprimirArvore(Knot? raiz)
        {
            if (raiz == null)
            {
                Console.WriteLine("Árvore vazia");
                return;
            }

            Console.WriteLine("\nEstrutura da árvore:");
            ImprimirArvoreRecursivo(raiz, "", true);
        }

        /// <summary>
        /// Método auxiliar recursivo para imprimir a árvore
        /// </summary>
        private void ImprimirArvoreRecursivo(Knot? no, string prefixo, bool ehUltimo)
        {
            if (no == null)
            {
                return;
            }

            Console.Write(prefixo);
            Console.Write(ehUltimo ? "└── " : "├── ");
            Console.WriteLine(no.Valor);

            if (no.Esquerda == null && no.Direita == null)
            {
                return;
            }

            string novoPrefixo = prefixo + (ehUltimo ? "    " : "│   ");

            if (no.Esquerda != null && no.Direita != null)
            {
                // Imprime direita primeiro (acima) e esquerda depois (abaixo)
                // para que visualmente a direita apareça à direita na representação
                ImprimirArvoreRecursivo(no.Direita, novoPrefixo, false);
                ImprimirArvoreRecursivo(no.Esquerda, novoPrefixo, true);
            }
            else if (no.Esquerda != null)
            {
                ImprimirArvoreRecursivo(no.Esquerda, novoPrefixo, true);
            }
            else if (no.Direita != null)
            {
                ImprimirArvoreRecursivo(no.Direita, novoPrefixo, true);
            }
        }

    }
}
