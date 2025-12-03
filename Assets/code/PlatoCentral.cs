using UnityEngine;
using System.Collections.Generic;

public class PlatoCentral : MonoBehaviour
{
    public static PlatoCentral Instance;

    [Header("Referencias a Ingredientes (Hijos de este Objeto)")]
    public GameObject gTotopos;
    public GameObject gSalsaVerde, gSalsaRoja;
    public GameObject gQueso;
    public GameObject gCebolla;
    public GameObject gCrema;
    public GameObject gPollo, gHuevo;

    public List<TipoIngrediente> ingredientesActuales = new List<TipoIngrediente>();
  
    private List<GameObject> objetosVisualesActivos = new List<GameObject>();

    void Awake() 
    { 
        if (Instance == null) Instance = this; 
        
        InicializarEstadoVisual();
    }

    void InicializarEstadoVisual()
    {
  
        gTotopos.SetActive(false);
        gSalsaVerde.SetActive(false);
        gSalsaRoja.SetActive(false);
        gQueso.SetActive(false);
        gCebolla.SetActive(false);
        gCrema.SetActive(false);
        gPollo.SetActive(false);
        gHuevo.SetActive(false);

        ingredientesActuales.Clear();
        objetosVisualesActivos.Clear();
    }


    public void AgregarIngrediente(TipoIngrediente ing)
    {
        if (!EsElIngredienteCorrecto(ing)) 
        {
            Debug.Log($"❌ Ingrediente incorrecto. Toca poner el siguiente de la lista.");
            return;
        }

        GameObject modelo = ObtenerGameObject(ing);
        if (modelo != null)
        {
            modelo.SetActive(true);
            
            ingredientesActuales.Add(ing);
            objetosVisualesActivos.Add(modelo); 
            
            Debug.Log($"✅ Agregado y Visible: {ing}");
        }
    }
    
    bool EsElIngredienteCorrecto(TipoIngrediente ing)
    {
        int pasoActual = ingredientesActuales.Count;

        if (pasoActual == 0) return ing == TipoIngrediente.Totopos;

        if (pasoActual == 1) return (ing == TipoIngrediente.SalsaVerde || ing == TipoIngrediente.SalsaRoja);

        if (pasoActual == 2) return ing == TipoIngrediente.Queso;

        if (pasoActual == 3) return ing == TipoIngrediente.Cebolla;

        if (pasoActual == 4) return ing == TipoIngrediente.Crema;

        if (pasoActual == 5) return (ing == TipoIngrediente.Pollo || ing == TipoIngrediente.Huevo);

        return false; 
    }

    GameObject ObtenerGameObject(TipoIngrediente ing)
    {
        switch (ing)
        {
            case TipoIngrediente.Totopos: return gTotopos;
            case TipoIngrediente.SalsaVerde: 
                gSalsaRoja.SetActive(false); 
                return gSalsaVerde;
            case TipoIngrediente.SalsaRoja: 
                gSalsaVerde.SetActive(false); 
                return gSalsaRoja;
            case TipoIngrediente.Queso: return gQueso;
            case TipoIngrediente.Cebolla: return gCebolla;
            case TipoIngrediente.Crema: return gCrema;
            case TipoIngrediente.Pollo: 
                gHuevo.SetActive(false); 
                return gPollo;
            case TipoIngrediente.Huevo: 
                gPollo.SetActive(false);
                return gHuevo;
            default: return null;
        }
    }

    public void LimpiarPlato()
    {
        foreach (GameObject obj in objetosVisualesActivos) 
        {
            if (obj != null) obj.SetActive(false);
        }
        
        objetosVisualesActivos.Clear();
        ingredientesActuales.Clear();
        
        Debug.Log("Plato Limpio y Componentes Desactivados.");
    }
}