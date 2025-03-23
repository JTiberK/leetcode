using System;
using System.Collections.Generic;

public class Solution {
    public int CountPaths(int n, int[][] roads) {
        const int MOD = 1_000_000_007;

        // Paso 1: Construir el grafo usando una lista de adyacencia
        List<(int node, long time)>[] graph = new List<(int, long)>[n];
        for (int i = 0; i < n; i++) {
            graph[i] = new List<(int, long)>();
        }
        foreach (var road in roads) {
            int u = road[0], v = road[1], time = road[2];
            graph[u].Add((v, time));
            graph[v].Add((u, time));
        }

        // Paso 2: Inicializar variables
        long[] distances = new long[n];
        int[] ways = new int[n];
        Array.Fill(distances, long.MaxValue);
        distances[0] = 0;
        ways[0] = 1;

        // Cola de prioridad (min-heap)
        var pq = new SortedSet<(long time, int node)>();
        pq.Add((0, 0)); // (tiempo, nodo)

        // Paso 3: Algoritmo de Dijkstra modificado
        while (pq.Count > 0) {
            var (currentTime, currentNode) = pq.Min;
            pq.Remove(pq.Min);

            // Iterar sobre los vecinos del nodo actual
            foreach (var (neighbor, travelTime) in graph[currentNode]) {
                long newTime = currentTime + travelTime;

                // Si encontramos un camino más corto
                if (newTime < distances[neighbor]) {
                    distances[neighbor] = newTime;
                    ways[neighbor] = ways[currentNode]; // Reiniciar el conteo de formas
                    pq.Add((newTime, neighbor));
                }
                // Si encontramos otro camino con el mismo tiempo mínimo
                else if (newTime == distances[neighbor]) {
                    ways[neighbor] = (ways[neighbor] + ways[currentNode]) % MOD;
                }
            }
        }

        // Paso 4: Devolver el resultado
        return ways[n - 1];
    }
}
