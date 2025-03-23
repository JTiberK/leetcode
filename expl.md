# **Número de Caminos para Llegar al Destino**

## **Planteamiento del Problema**
Dado un conjunto de intersecciones numeradas de `0` a `n-1` y una lista de carreteras bidireccionales entre algunas de estas intersecciones, el objetivo es determinar cuántos caminos diferentes existen para viajar desde la intersección `0` hasta la intersección `n-1` en el tiempo más corto posible. Cada carretera tiene un tiempo asociado para viajar entre las intersecciones conectadas.

### **Ejemplo**
#### Entrada:
```javascript
let n = 7;
let roads = [
    [0, 6, 7],
    [0, 1, 2],
    [1, 2, 3],
    [1, 3, 3],
    [6, 3, 3],
    [3, 5, 1],
    [6, 5, 1],
    [2, 5, 1],
    [0, 4, 5],
    [4, 6, 2]
];
```
#### Salida:
```javascript
4
```

**Explicación**: El tiempo mínimo para llegar de la intersección `0` a la intersección `6` es `7` minutos. Hay 4 caminos diferentes que permiten lograrlo:
1. `0 ➝ 6`
2. `0 ➝ 4 ➝ 6`
3. `0 ➝ 1 ➝ 2 ➝ 5 ➝ 6`
4. `0 ➝ 1 ➝ 3 ➝ 5 ➝ 6`

---

## **Enfoque**

Para resolver este problema, utilizaremos una variante del **algoritmo de Dijkstra**, que es ideal para encontrar los caminos más cortos en grafos con pesos no negativos. Además, modificaremos el algoritmo para contar el número de formas de llegar a cada nodo en el tiempo mínimo.

### **Pasos del Enfoque**
1. **Representación del Grafo**:
   - Representamos las intersecciones como nodos y las carreteras como aristas ponderadas.
   - Utilizamos una lista de adyacencia (`Map`) para almacenar los nodos vecinos y los tiempos de viaje.

2. **Algoritmo de Dijkstra Modificado**:
   - Usamos una cola de prioridad (min-heap) para procesar los nodos en orden creciente de tiempo.
   - Mantenemos dos arreglos:
     - `distances`: Almacena el tiempo mínimo para llegar a cada nodo.
     - `ways`: Cuenta el número de formas de llegar a cada nodo en el tiempo mínimo.

3. **Actualización de Estados**:
   - Si encontramos un camino más corto hacia un nodo, actualizamos su distancia mínima y reiniciamos el contador de formas (`ways[node] = ways[currentNode]`).
   - Si encontramos otro camino con el mismo tiempo mínimo, incrementamos el contador de formas (`ways[node] += ways[currentNode]`).

4. **Módulo**:
   - Como el resultado puede ser muy grande, devolvemos el resultado módulo $10^9 + 7$.

---

## **Implementación en JavaScript**

```javascript
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
```

---

## **Explicación del Código**

1. **Construcción del Grafo**:
   - Usamos una lista de adyacencia (`Array` de listas) para representar el grafo. Cada nodo tiene una lista de sus vecinos junto con el tiempo necesario para viajar.

2. **Inicialización**:
   - `distances`: Se inicializa con valores máximos (`Infinity`), excepto para el nodo inicial (`0`), que se establece en `0`.
   - `ways`: Se inicializa con `1` para el nodo inicial, ya que hay una forma de estar en el nodo inicial sin moverse.

3. **Cola de Prioridad**:
   - Usamos la clase `MinPriorityQueue` de la biblioteca `@datastructures-js/priority-queue` para implementar la cola de prioridad. Esto nos permite extraer siempre el nodo con el menor tiempo.

4. **Procesamiento de Nodos**:
   - Para cada nodo, exploramos sus vecinos. Si encontramos un camino más corto o un camino con el mismo tiempo mínimo, actualizamos las distancias y los contadores de formas.

5. **Resultado**:
   - Finalmente, devolvemos el valor de `ways[n-1]`, que representa el número de formas de llegar al nodo destino en el tiempo mínimo.

---

## **Complejidad**

### **Tiempo**
1. **Construcción del Grafo**: $O(E)$, donde $E$ es el número de carreteras.
2. **Procesamiento de Dijkstra**: $O((V + E) \log V)$, donde $V$ es el número de nodos.
3. **Total**: $O((V + E) \log V)$.

### **Espacio**
1. **Grafo**: $O(V + E)$.
2. **Arreglos `distances` y `ways`**: $O(V)$.
3. **Cola de Prioridad**: $O(V)$.
4. **Total**: $O(V + E)$.

---

## **Ejemplo de Ejecución**

#### Entrada:
```javascript
let n = 7;
let roads = [
    [0, 6, 7],
    [0, 1, 2],
    [1, 2, 3],
    [1, 3, 3],
    [6, 3, 3],
    [3, 5, 1],
    [6, 5, 1],
    [2, 5, 1],
    [0, 4, 5],
    [4, 6, 2]
];
```

#### Salida:
```javascript
4
```

**Explicación**: El tiempo mínimo para llegar de la intersección `0` a la intersección `6` es `7` minutos. Hay 4 caminos diferentes que permiten lograrlo:
1. `0 ➝ 6`
2. `0 ➝ 4 ➝ 6`
3. `0 ➝ 1 ➝ 2 ➝ 5 ➝ 6`
4. `0 ➝ 1 ➝ 3 ➝ 5 ➝ 6`

---

### **Respuesta Final**
$$
\boxed{4}
$$
