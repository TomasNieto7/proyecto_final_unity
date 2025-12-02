using UnityEngine;
using TMPro;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance;

    [Header("Pantalla PC")]
    public TextMeshProUGUI textoMonitor;

    [Header("HUD Jugador")]
    public TextMeshProUGUI textoDinero; 

    [Header("Datos del Juego")]
    public int dineroActual = 0;
    public bool hayOrdenActiva = false;
    
    public TipoIngrediente salsaObjetivo;
    public TipoIngrediente extraObjetivo;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        ActualizarTextoDinero();
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

        if (textoMonitor != null)
        {
            textoMonitor.text = $"CLIENTE:\nChilaquiles {colorSalsa}\n{textoExtra}";
            textoMonitor.color = Color.white;
        }
    }


    public void ModificarDinero(int cantidad)
    {
        dineroActual += cantidad;
        
        if (textoDinero != null)
        {
            if (dineroActual < 0) textoDinero.color = Color.red;
            else textoDinero.color = Color.green;
        }

        ActualizarTextoDinero();
    }

    void ActualizarTextoDinero()
    {
        if (textoDinero != null)
        {
            textoDinero.text = "$" + dineroActual.ToString();
        }
    }

    public void CompletarOrden()
    {
        hayOrdenActiva = false;
        
        if (textoMonitor != null)
        {
            textoMonitor.text = "¡ENTREGADO!\nEsperando cliente...";
            textoMonitor.color = Color.yellow;
        }
    }
}