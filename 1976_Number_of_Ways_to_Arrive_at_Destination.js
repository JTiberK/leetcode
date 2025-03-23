/**
 * @param {number} n
 * @param {number[][]} roads
 * @return {number}
 */
var countPaths = function(n, roads) {
    const MOD = 1_000_000_007;

    // Paso 1: Construir el grafo usando una lista de adyacencia
    let graph = new Array(n).fill(null).map(() => []);
    for (let [u, v, time] of roads) {
        graph[u].push([v, time]);
        graph[v].push([u, time]);
    }

    // Paso 2: Inicializar variables
    let distances = new Array(n).fill(Infinity);
    let ways = new Array(n).fill(0);
    distances[0] = 0;
    ways[0] = 1;

    // Cola de prioridad (min-heap)
    let pq = new MinPriorityQueue({ priority: (x) => x.time });
    pq.enqueue({ node: 0, time: 0 });

    // Paso 3: Algoritmo de Dijkstra modificado
    while (!pq.isEmpty()) {
        let { element: { node: currentNode, time: currentTime } } = pq.dequeue();

        // Iterar sobre los vecinos del nodo actual
        for (let [neighbor, travelTime] of graph[currentNode]) {
            let newTime = currentTime + travelTime;

            // Si encontramos un camino más corto
            if (newTime < distances[neighbor]) {
                distances[neighbor] = newTime;
                ways[neighbor] = ways[currentNode]; // Reiniciar el conteo de formas
                pq.enqueue({ node: neighbor, time: newTime });
            }
            // Si encontramos otro camino con el mismo tiempo mínimo
            else if (newTime === distances[neighbor]) {
                ways[neighbor] = (ways[neighbor] + ways[currentNode]) % MOD;
            }
        }
    }

    // Paso 4: Devolver el resultado
    return ways[n - 1];
};
