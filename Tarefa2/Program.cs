using System;
using System.Linq;

namespace Tarefa2
{
    /// <summary>
    /// Programa principal para testar a construção de árvores binárias
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Desafio Técnico - Tarefa 2: Construção de Árvore Binária ===\n");

            ArvoreBuilder builder = new ArvoreBuilder();

            // Cenário 1: [3, 2, 1, 6, 0, 5]
            int[] array1 = { 3, 2, 1, 6, 0, 5 };
            ProcessarCenario("Cenário 1", array1, builder);

            Console.WriteLine("\n" + new string('-', 60) + "\n");

            // Cenário 2: [7, 5, 13, 9, 1, 6, 4]
            int[] array2 = { 7, 5, 13, 9, 1, 6, 4 };
            ProcessarCenario("Cenário 2", array2, builder);

            Console.WriteLine("\n" + new string('-', 60) + "\n");
            Console.WriteLine("Agora você pode testar com seus próprios arrays!\n");

            // Loop para permitir que o usuário teste arrays customizados
            while (true)
            {
                Console.Write("Digite os números do array separados por vírgula (ou 'sair' para encerrar): ");
                string? entrada = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(entrada) || entrada.ToLower() == "sair")
                {
                    break;
                }

                try
                {
                    int[] arrayCustom = entrada.Split(',')
                        .Select(s => s.Trim())
                        .Where(s => !string.IsNullOrEmpty(s))
                        .Select(int.Parse)
                        .ToArray();

                    if (arrayCustom.Length == 0)
                    {
                        Console.WriteLine("Array vazio! Tente novamente.\n");
                        continue;
                    }

                    // Verifica duplicidades
                    if (arrayCustom.Length != arrayCustom.Distinct().Count())
                    {
                        Console.WriteLine("Erro: O array não pode conter valores duplicados!\n");
                        continue;
                    }

                    ProcessarCenario("Array Customizado", arrayCustom, builder);
                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao processar array: {ex.Message}\n");
                }
            }

            Console.WriteLine("\nPrograma encerrado. Obrigado!");
        }

        static void ProcessarCenario(string nomeCenario, int[] array, ArvoreBuilder builder)
        {
            Console.WriteLine($"{nomeCenario}:");
            Console.WriteLine($"Array de entrada: [{string.Join(", ", array)}]");

            // Encontra o índice do maior valor (raiz)
            int indiceRaiz = Array.IndexOf(array, array.Max());
            int valorRaiz = array[indiceRaiz];

            // Calcula os galhos da esquerda e direita
            int[] galhosEsquerda = array.Take(indiceRaiz).ToArray();
            int[] galhosDireita = array.Skip(indiceRaiz + 1).ToArray();

            // Ordena em ordem decrescente para exibição
            Array.Sort(galhosEsquerda);
            Array.Reverse(galhosEsquerda);

            Array.Sort(galhosDireita);
            Array.Reverse(galhosDireita);

            // Constrói a árvore
            Knot? raiz = builder.ConstruirArvore(array);

            if (raiz != null)
            {
                Console.WriteLine($"Raiz: {raiz.Valor}");

                if (galhosEsquerda.Length > 0)
                {
                    Console.WriteLine($"Galhos da esquerda: {string.Join(", ", galhosEsquerda)}");
                }
                else
                {
                    Console.WriteLine("Galhos da esquerda: (nenhum)");
                }

                if (galhosDireita.Length > 0)
                {
                    Console.WriteLine($"Galhos da direita: {string.Join(", ", galhosDireita)}");
                }
                else
                {
                    Console.WriteLine("Galhos da direita: (nenhum)");
                }

                builder.ImprimirArvore(raiz);
            }
        }
    }
}
