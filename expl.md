## 1976 - Number of Ways to Arrive at Destination


Hint
You are in a city that consists of n intersections numbered from 0 to n - 1 with bi-directional roads between some intersections. The inputs are generated such that you can reach any intersection from any other intersection and that there is at most one road between any two intersections.

You are given an integer n and a 2D integer array roads where roads[i] = [ui, vi, timei] means that there is a road between intersections ui and vi that takes timei minutes to travel. You want to know in how many ways you can travel from intersection 0 to intersection n - 1 in the shortest amount of time.

Return the number of ways you can arrive at your destination in the shortest amount of time. Since the answer may be large, return it modulo 109 + 7.

Example 1:

Input: n = 7, roads = [[0,6,7],[0,1,2],[1,2,3],[1,3,3],[6,3,3],[3,5,1],[6,5,1],[2,5,1],[0,4,5],[4,6,2]]
Output: 4
Explanation: The shortest amount of time it takes to go from intersection 0 to intersection 6 is 7 minutes.
The four ways to get there in 7 minutes are:

- 0 ➝ 6
- 0 ➝ 4 ➝ 6
- 0 ➝ 1 ➝ 2 ➝ 5 ➝ 6
- 0 ➝ 1 ➝ 3 ➝ 5 ➝ 6
  Example 2:

Input: n = 2, roads = [[1,0,10]]
Output: 1
Explanation: There is only one way to go from intersection 0 to intersection 1, and it takes 10 minutes.

Constraints:

1 <= n <= 200
n - 1 <= roads.length <= n \* (n - 1) / 2
roads[i].length == 3
0 <= ui, vi <= n - 1
1 <= timei <= 109
ui != vi
There is at most one road connecting any two intersections.
You can reach any intersection from any other intersection.



### JTiberK

# **Número de Caminos para Llegar al Destino**

## **Planteamiento del Problema**

Dado un conjunto de intersecciones numeradas de `0` a `n-1` y una lista de carreteras bidireccionales entre algunas de estas intersecciones, el objetivo es determinar cuántos caminos diferentes existen para viajar desde la intersección `0` hasta la intersección `n-1` en el tiempo más corto posible. Cada carretera tiene un tiempo asociado para viajar entre las intersecciones conectadas.

### **Ejemplo**

#### Entrada:

```csharp
int n = 7;
int[][] roads = new int[][] {
    new int[] {0, 6, 7},
    new int[] {0, 1, 2},
    new int[] {1, 2, 3},
    new int[] {1, 3, 3},
    new int[] {6, 3, 3},
    new int[] {3, 5, 1},
    new int[] {6, 5, 1},
    new int[] {2, 5, 1},
    new int[] {0, 4, 5},
    new int[] {4, 6, 2}
};
```

#### Salida:

```csharp
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
   - Utilizamos una lista de adyacencia (`List<(int node, long time)>[]`) para almacenar los nodos vecinos y los tiempos de viaje.

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

## **Implementación en C#**

```csharp
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
```

---

## **Explicación del Código**

1. **Construcción del Grafo**:

   - Usamos una lista de adyacencia para representar el grafo. Cada nodo tiene una lista de sus vecinos junto con el tiempo necesario para viajar.

2. **Inicialización**:

   - `distances`: Se inicializa con valores máximos, excepto para el nodo inicial (`0`), que se establece en `0`.
   - `ways`: Se inicializa con `1` para el nodo inicial, ya que hay una forma de estar en el nodo inicial sin moverse.

3. **Cola de Prioridad**:

   - Usamos una `SortedSet` para implementar la cola de prioridad. Esto nos permite extraer siempre el nodo con el menor tiempo.

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

```csharp
int n = 7;
int[][] roads = new int[][] {
    new int[] {0, 6, 7},
    new int[] {0, 1, 2},
    new int[] {1, 2, 3},
    new int[] {1, 3, 3},
    new int[] {6, 3, 3},
    new int[] {3, 5, 1},
    new int[] {6, 5, 1},
    new int[] {2, 5, 1},
    new int[] {0, 4, 5},
    new int[] {4, 6, 2}
};
```

#### Salida:

```csharp
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
