/**   SOLUCION  ****************************************(❁´◡`❁)*****************************/


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

    // Cola de prioridad (min-heap) implementada manualmente
    let pq = [[0, 0]]; // [time, node]

    // Paso 3: Algoritmo de Dijkstra modificado
    while (pq.length > 0) {
        // Extraer el nodo con el menor tiempo usando un heap mínimo
        pq.sort((a, b) => a[0] - b[0]); // Ordenar por tiempo
        let [currentTime, currentNode] = pq.shift();

        // Iterar sobre los vecinos del nodo actual
        for (let [neighbor, travelTime] of graph[currentNode]) {
            let newTime = currentTime + travelTime;

            // Si encontramos un camino más corto
            if (newTime < distances[neighbor]) {
                distances[neighbor] = newTime;
                ways[neighbor] = ways[currentNode]; // Reiniciar el conteo de formas
                pq.push([newTime, neighbor]);
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
