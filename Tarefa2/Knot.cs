namespace Tarefa2
{
    /// <summary>
    /// Classe que representa um nó de uma árvore binária
    /// </summary>
    public class Knot
    {
        /// <summary>
        /// Valor armazenado no nó
        /// </summary>
        public int Valor { get; set; }

        /// <summary>
        /// Referência para o nó filho da esquerda
        /// </summary>
        public Knot? Esquerda { get; set; }

        /// <summary>
        /// Referência para o nó filho da direita
        /// </summary>
        public Knot? Direita { get; set; }

        /// <summary>
        /// Construtor do nó
        /// </summary>
        /// <param name="valor">Valor a ser armazenado no nó</param>
        public Knot(int valor)
        {
            Valor = valor;
            Esquerda = null;
            Direita = null;
        }
    }
}
