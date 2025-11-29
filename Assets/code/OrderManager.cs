using UnityEngine;
using TMPro;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance;

    [Header("Pantalla PC")]
    public TextMeshProUGUI textoMonitor; // Arrastra el texto del Canvas de la PC

    [Header("Orden Actual")]
    public bool hayOrdenActiva = false;
    public TipoIngrediente salsaObjetivo; // ¿Verde o Roja?
    public TipoIngrediente extraObjetivo; // ¿Pollo, Huevo o Nada?

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void GenerarOrden()
    {
        if (hayOrdenActiva) return;

        // 1. Elegir Salsa (50/50)
        salsaObjetivo = (Random.value > 0.5f) ? TipoIngrediente.SalsaVerde : TipoIngrediente.SalsaRoja;

        // 2. Elegir Extra (33% cada uno)
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