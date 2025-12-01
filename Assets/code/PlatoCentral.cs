using UnityEngine;
using System.Collections.Generic;

public class PlatoCentral : MonoBehaviour
{
    public static PlatoCentral Instance;

    [Header("Referencias a Ingredientes (Hijos de este Objeto)")]
    // Referencias a los GameObjects hijos. ¡Arrastra los objetos de Unity aquí!
    public GameObject gTotopos;
    public GameObject gSalsaVerde, gSalsaRoja;
    public GameObject gQueso;
    public GameObject gCebolla;
    public GameObject gCrema;
    public GameObject gPollo, gHuevo;

    // Lista de lo que lleva el plato
    public List<TipoIngrediente> ingredientesActuales = new List<TipoIngrediente>();
    
    // Lista para almacenar las referencias a los GameObjects que hemos activado, 
    // útil para la función LimpiarPlato.
    private List<GameObject> objetosVisualesActivos = new List<GameObject>();

    void Awake() 
    { 
        if (Instance == null) Instance = this; 
        
        // 🚨 Inicializamos todos los ingredientes (excepto el plato padre) desactivados 🚨
        InicializarEstadoVisual();
    }

    /// <summary>
    /// Asegura que todos los GameObjects de ingredientes estén inicialmente desactivados.
    /// </summary>
    void InicializarEstadoVisual()
    {
        // El objeto padre 'chilaquiles1' (este script) debe estar activo
        // El plato (si es un hijo) debe ignorarse o manejarse por separado, 
        // pero por la lista de arriba, solo controlamos los ingredientes.
        
        // Desactivamos todas las referencias al inicio.
        gTotopos.SetActive(false);
        gSalsaVerde.SetActive(false);
        gSalsaRoja.SetActive(false);
        gQueso.SetActive(false);
        gCebolla.SetActive(false);
        gCrema.SetActive(false);
        gPollo.SetActive(false);
        gHuevo.SetActive(false);

        // Limpiamos la lista lógica también.
        ingredientesActuales.Clear();
        objetosVisualesActivos.Clear();
    }


    public void AgregarIngrediente(TipoIngrediente ing)
    {
        // 1. Validar Orden Estricto
        if (!EsElIngredienteCorrecto(ing)) 
        {
            Debug.Log($"❌ Ingrediente incorrecto. Toca poner el siguiente de la lista.");
            return;
        }

        // 2. Obtener el GameObject y activarlo
        GameObject modelo = ObtenerGameObject(ing);
        if (modelo != null)
        {
            // Activamos el GameObject, ya no instanciamos
            modelo.SetActive(true);
            
            // Guardar para la lógica
            ingredientesActuales.Add(ing);
            // Guardamos la referencia para poder limpiarla después
            objetosVisualesActivos.Add(modelo); 
            
            Debug.Log($"✅ Agregado y Visible: {ing}");
        }
    }
    
    // ⚠ Se mantiene la lógica de validación de orden estricto ⚠
    bool EsElIngredienteCorrecto(TipoIngrediente ing)
    {
        int pasoActual = ingredientesActuales.Count;

        // PASO 0: Base de Totopos
        if (pasoActual == 0) return ing == TipoIngrediente.Totopos;

        // PASO 1: Salsa (Verde o Roja)
        if (pasoActual == 1) return (ing == TipoIngrediente.SalsaVerde || ing == TipoIngrediente.SalsaRoja);

        // PASO 2: Queso
        if (pasoActual == 2) return ing == TipoIngrediente.Queso;

        // PASO 3: Cebolla
        if (pasoActual == 3) return ing == TipoIngrediente.Cebolla;

        // PASO 4: Crema
        if (pasoActual == 4) return ing == TipoIngrediente.Crema;

        // PASO 5: Extras (Pollo o Huevo) - Opcional
        if (pasoActual == 5) return (ing == TipoIngrediente.Pollo || ing == TipoIngrediente.Huevo);

        return false; // Ya tiene todo o intentó poner algo extra raro
    }

    // ✨ Función modificada para devolver el GameObject a activar ✨
    GameObject ObtenerGameObject(TipoIngrediente ing)
    {
        switch (ing)
        {
            case TipoIngrediente.Totopos: return gTotopos;
            // Manejo especial para la salsa, ya que solo puede ir una
            case TipoIngrediente.SalsaVerde: 
                // Asegurar que la salsa opuesta esté desactivada si se agregó alguna vez
                gSalsaRoja.SetActive(false); 
                return gSalsaVerde;
            case TipoIngrediente.SalsaRoja: 
                // Asegurar que la salsa opuesta esté desactivada si se agregó alguna vez
                gSalsaVerde.SetActive(false); 
                return gSalsaRoja;
            case TipoIngrediente.Queso: return gQueso;
            case TipoIngrediente.Cebolla: return gCebolla;
            case TipoIngrediente.Crema: return gCrema;
            // Manejo especial para Pollo/Huevo, solo puede ir uno.
            case TipoIngrediente.Pollo: 
                gHuevo.SetActive(false); 
                return gPollo;
            case TipoIngrediente.Huevo: 
                gPollo.SetActive(false);
                return gHuevo;
            default: return null;
        }
    }

    /// <summary>
    /// Desactiva todos los ingredientes y limpia la lógica del plato.
    /// </summary>
    public void LimpiarPlato()
    {
        // Desactivamos cada GameObject que previamente activamos.
        foreach (GameObject obj in objetosVisualesActivos) 
        {
            if (obj != null) obj.SetActive(false);
        }
        
        // Limpiamos las listas lógicas.
        objetosVisualesActivos.Clear();
        ingredientesActuales.Clear();
        
        Debug.Log("🍽 Plato Limpio y Componentes Desactivados.");
    }
}