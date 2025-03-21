using System;
using System.Collections.Generic;

public class Solution {
    public IList<string> FindAllRecipes(string[] recipes, IList<IList<string>> ingredients, string[] supplies) {
        // Mapa de recetas a sus ingredientes
        Dictionary<string, IList<string>> recipeMap = new Dictionary<string, IList<string>>();
        // Conjunto de suministros iniciales
        HashSet<string> supplySet = new HashSet<string>(supplies);
        // Conjunto para almacenar las recetas que se pueden crear
        HashSet<string> canMake = new HashSet<string>();
        // Estado de las recetas: 0 = no visitada, 1 = visitando, 2 = visitada
        Dictionary<string, int> state = new Dictionary<string, int>();

        // Llenar el mapa de recetas
        for (int i = 0; i < recipes.Length; i++) {
            recipeMap[recipes[i]] = ingredients[i];
            state[recipes[i]] = 0; // Inicialmente, todas las recetas están no visitadas
        }

        // Función para verificar si se puede hacer una receta
        bool CanMakeRecipe(string recipe) {
            if (state[recipe] == 1) return false; // Ciclo detectado
            if (state[recipe] == 2) return true; // Ya se puede hacer

            state[recipe] = 1; // Marcar como visitando

            // Obtener los ingredientes necesarios
            foreach (var ingredient in recipeMap[recipe]) {
                // Si el ingrediente es un suministro, continuar
                if (supplySet.Contains(ingredient)) {
                    continue;
                }
                // Si el ingrediente es una receta, verificar si se puede hacer
                if (!recipeMap.ContainsKey(ingredient) || !CanMakeRecipe(ingredient)) {
                    state[recipe] = 0; // Marcar como no visitada
                    return false; // No se puede hacer esta receta
                }
            }

            // Si se pueden hacer todos los ingredientes, se puede hacer la receta
            state[recipe] = 2; // Marcar como visitada
            canMake.Add(recipe);
            return true;
        }

        // Verificar cada receta
        foreach (var recipe in recipes) {
            CanMakeRecipe(recipe);
        }

        // Convertir el conjunto de recetas que se pueden hacer a una lista
        return new List<string>(canMake);
    }
}
