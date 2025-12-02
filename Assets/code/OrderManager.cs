using UnityEngine;
using TMPro;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance;

    [Header("Pantalla PC")]
    public TextMeshProUGUI textoMonitor; 

    [Header("Orden Actual")]
    public bool hayOrdenActiva = false;
    public TipoIngrediente salsaObjetivo; 
    public TipoIngrediente extraObjetivo; 

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void GenerarOrden()
    {
        if (hayOrdenActiva) return;

        salsaObjetivo = (Random.value > 0.5f) ? TipoIngrediente.SalsaVerde : TipoIngrediente.SalsaRoja;

        float r = Random.value;
        if (r < 0.33f) extraObjetivo = TipoIngrediente.Nada;
        else if (r < 0.66f) extraObjetivo = TipoIngrediente.Pollo;
        else extraObjetivo = TipoIngrediente.Huevo;

        hayOrdenActiva = true;
        ActualizarMonitor();
    }

    void ActualizarMonitor()
    {
        string colorSalsa = (salsaObjetivo == TipoIngrediente.SalsaVerde) ? "<color=green>VERDES</color>" : "<color=red>ROJOS</color>";
        string textoExtra = (extraObjetivo == TipoIngrediente.Nada) ? "Sencillos" : "con " + extraObjetivo.ToString();

        textoMonitor.text = $"CLIENTE:\nChilaquiles {colorSalsa}\n{textoExtra}";
    }

    public void CompletarOrden()
    {
        hayOrdenActiva = false;
        textoMonitor.text = "¡Orden Entregada!\nEsperando...";
        textoMonitor.color = Color.white;
    }
}