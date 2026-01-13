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
        /// Constrói uma árvore binária a partir de um array de inteiros
        /// </summary>
        /// <param name="array">Array de inteiros sem duplicidade</param>
        /// <returns>Raiz da árvore construída, ou null se o array estiver vazio</returns>
        public Knot? ConstruirArvore(int[] array)
        {
            if (array == null || array.Length == 0)
            {
                return null;
            }

            if (array.Length == 1)
            {
                return new Knot(array[0]);
            }

            // Encontra o índice do maior valor (raiz)
            int indiceRaiz = Array.IndexOf(array, array.Max());

            // Cria o nó raiz
            Knot raiz = new Knot(array[indiceRaiz]);

            // Divide o array em partes esquerda e direita
            int[] arrayEsquerda = array.Take(indiceRaiz).ToArray();
            int[] arrayDireita = array.Skip(indiceRaiz + 1).ToArray();

            // Ordena os arrays em ordem decrescente
            Array.Sort(arrayEsquerda);
            Array.Reverse(arrayEsquerda);

            Array.Sort(arrayDireita);
            Array.Reverse(arrayDireita);

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
            if (array == null || array.Length == 0)
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

        /// <summary>
        /// Imprime a árvore em ordem (percurso in-order)
        /// </summary>
        /// <param name="raiz">Raiz da árvore</param>
        public void ImprimirEmOrdem(Knot? raiz)
        {
            if (raiz == null)
            {
                return;
            }

            ImprimirEmOrdem(raiz.Esquerda);
            Console.Write(raiz.Valor + " ");
            ImprimirEmOrdem(raiz.Direita);
        }
    }
}
