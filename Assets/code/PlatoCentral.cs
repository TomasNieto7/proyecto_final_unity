using UnityEngine;
using System.Collections.Generic;

public class PlatoCentral : MonoBehaviour
{
    public static PlatoCentral Instance;

    [Header("Prefabs Visuales")]
    public GameObject pTotopos;
    public GameObject pSalsaVerde, pSalsaRoja;
    public GameObject pQueso;
    public GameObject pCebolla;
    public GameObject pCrema;
    public GameObject pPollo, pHuevo;

    [Header("Configuración")]
    public Transform spawnPoint;
    public float alturaPorCapa = 0.03f; // Ajusta esto para que se vea bien apilado
    
    // Lista de lo que lleva el plato
    public List<TipoIngrediente> ingredientesActuales = new List<TipoIngrediente>();
    private List<GameObject> objetosVisuales = new List<GameObject>();

    void Awake() { if (Instance == null) Instance = this; }

    public void AgregarIngrediente(TipoIngrediente ing)
    {
        // 1. Validar Orden Estricto según tu dibujo
        if (!EsElIngredienteCorrecto(ing)) 
        {
            Debug.Log($"❌ Ingrediente incorrecto. Toca poner el siguiente de la lista.");
            return;
        }

        // 2. Instanciar visualmente
        GameObject modelo = ObtenerModelo(ing);
        if (modelo != null)
        {
            float alturaY = ingredientesActuales.Count * alturaPorCapa;
            GameObject nuevoObj = Instantiate(modelo, spawnPoint);
            
            // Ajustes de posición
            nuevoObj.transform.localPosition = new Vector3(0, alturaY, 0);
            nuevoObj.transform.localRotation = Quaternion.identity;

            // Guardar
            ingredientesActuales.Add(ing);
            objetosVisuales.Add(nuevoObj);
            
            Debug.Log($"✅ Agregado: {ing}");
        }
    }

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
        // Nota: Si el jugador quiere entregarlos sencillos, no pondrá esto e irá directo al timbre.
        if (pasoActual == 5) return (ing == TipoIngrediente.Pollo || ing == TipoIngrediente.Huevo);

        return false; // Ya tiene todo o intentó poner algo extra raro
    }

    GameObject ObtenerModelo(TipoIngrediente ing)
    {
        switch (ing)
        {
            case TipoIngrediente.Totopos: return pTotopos;
            case TipoIngrediente.SalsaVerde: return pSalsaVerde;
            case TipoIngrediente.SalsaRoja: return pSalsaRoja;
            case TipoIngrediente.Queso: return pQueso;
            case TipoIngrediente.Cebolla: return pCebolla;
            case TipoIngrediente.Crema: return pCrema;
            case TipoIngrediente.Pollo: return pPollo;
            case TipoIngrediente.Huevo: return pHuevo;
            default: return null;
        }
    }

    public void LimpiarPlato()
    {
        foreach (GameObject obj in objetosVisuales) Destroy(obj);
        objetosVisuales.Clear();
        ingredientesActuales.Clear();
    }
}