using System;

namespace GrafosParque
{
    class Program
    {
        const int QuantidadeVertices = 8;

        const int Infinito = int.MaxValue;

        static int[,] matriz = new int[QuantidadeVertices, QuantidadeVertices];

        static void AdicionarAresta(int a, int b, int peso)
        {
            matriz[a, b] = peso;
            matriz[b, a] = peso;
        }
        static void CriarGrafo()
        {
            AdicionarAresta(0, 1, 4);
            AdicionarAresta(0, 2, 3);
            AdicionarAresta(1, 2, 1);
            AdicionarAresta(1, 3, 2);
            AdicionarAresta(2, 3, 4);
            AdicionarAresta(3, 4, 2);
            AdicionarAresta(3, 5, 5);
            AdicionarAresta(4, 5, 1);
            AdicionarAresta(4, 6, 6);
            AdicionarAresta(5, 6, 3);
            AdicionarAresta(5, 7, 7);
            AdicionarAresta(6, 7, 2);
        }
        static void ImprimirGrafo()
        {
            Console.WriteLine("===== ESTRUTURA DO GRAFO =====");
            for (int i = 0; i < QuantidadeVertices; i++)
            {
                Console.Write("V" + i + " -> ");
                for (int j = 0; j < QuantidadeVertices; j++)
                {
                    if (matriz[i, j] != 0)
                    {
                        Console.Write("V" + j + "(peso " + matriz[i, j] + ")  ");
                    }
                }
                Console.WriteLine();
            }
        }
        static void ImprimirCaminho(int[] anterior, int vertice)
        {
            if (anterior[vertice] != -1)
            {
                ImprimirCaminho(anterior, anterior[vertice]);
                Console.Write(" -> ");
            }
            Console.Write("V" + vertice);
        }

        static void Dijkstra(int origem)
        {
            int[] distancia = new int[QuantidadeVertices];
            int[] anterior = new int[QuantidadeVertices];
            bool[] visitado = new bool[QuantidadeVertices];

            for (int i = 0; i < QuantidadeVertices; i++)
            {
                distancia[i] = Infinito;
                anterior[i] = -1;
                visitado[i] = false;
            }

            distancia[origem] = 0;

            for (int contador = 0; contador < QuantidadeVertices; contador++)
            {
                int u = -1;
                for (int i = 0; i < QuantidadeVertices; i++)
                {
                    if (!visitado[i] && (u == -1 || distancia[i] < distancia[u]))
                    {
                        u = i;
                    }
                }

                if (distancia[u] == Infinito)
                {
                    break;
                }

                visitado[u] = true;

                for (int v = 0; v < QuantidadeVertices; v++)
                {
                    if (matriz[u, v] != 0 && !visitado[v])
                    {
                        int novaDistancia = distancia[u] + matriz[u, v];
                        if (novaDistancia < distancia[v])
                        {
                            distancia[v] = novaDistancia;
                            anterior[v] = u;
                        }
                    }
                }
            }

            Console.WriteLine("\n===== DIJKSTRA (origem V" + origem + ") =====");
            for (int i = 0; i < QuantidadeVertices; i++)
            {
                Console.Write("V" + origem + " -> V" + i + " | custo: " + distancia[i] + " | caminho: ");
                ImprimirCaminho(anterior, i);
                Console.WriteLine();
            }
        }

        static void Prim()
        {
            int[] chave = new int[QuantidadeVertices];
            int[] pai = new int[QuantidadeVertices];
            bool[] naArvore = new bool[QuantidadeVertices];

            for (int i = 0; i < QuantidadeVertices; i++)
            {
                chave[i] = Infinito;
                pai[i] = -1;
                naArvore[i] = false;
            }

            chave[0] = 0;

            int somaTotal = 0;

            Console.WriteLine("\n===== ÁRVORE GERADORA MÍNIMA (PRIM) =====");

            for (int contador = 0; contador < QuantidadeVertices; contador++)
            {
                int u = -1;
                for (int i = 0; i < QuantidadeVertices; i++)
                {
                    if (!naArvore[i] && (u == -1 || chave[i] < chave[u]))
                    {
                        u = i;
                    }
                }

                naArvore[u] = true;

                if (pai[u] != -1)
                {
                    Console.WriteLine("V" + pai[u] + " - V" + u + " (peso " + matriz[pai[u], u] + ")");
                    somaTotal += matriz[pai[u], u];
                }

                for (int v = 0; v < QuantidadeVertices; v++)
                {
                    if (matriz[u, v] != 0 && !naArvore[v] && matriz[u, v] < chave[v])
                    {
                        chave[v] = matriz[u, v];
                        pai[v] = u;
                    }
                }
            }

            Console.WriteLine("Soma total dos pesos: " + somaTotal);
        }


        static void Main(string[] args)
        {
            CriarGrafo();
            ImprimirGrafo();

            Console.Write("\nInforme o vértice de origem para o Dijkstra (0 a 7): ");
            int origem = int.Parse(Console.ReadLine());

            Dijkstra(origem);
            Prim();
        }
    }
}