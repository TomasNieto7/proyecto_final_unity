using UnityEngine;
using TMPro;

public class InfoPlato : MonoBehaviour
{
    public TextMeshProUGUI textoDisplay;
    
    void Update()
    {
        if (OrderManager.Instance == null || !OrderManager.Instance.hayOrdenActiva)
        {
            textoDisplay.text = "Esperando Orden...";
            return;
        }

        // Usamos <mspace> para que los caracteres tengan el mismo ancho y se alineen bien
        string lista = "<align=left><mspace=0.6em><b>PREPARACIÓN:</b>\n";
        var plato = PlatoCentral.Instance.ingredientesActuales;

        // 1. Totopos (Usamos [X] para listo y [ ] para pendiente)
        bool tieneTotopos = plato.Contains(TipoIngrediente.Totopos);
        lista += tieneTotopos ? "[X] Totopos\n" : "[ ] Totopos\n";

        // 2. Salsa
        TipoIngrediente salsaMeta = OrderManager.Instance.salsaObjetivo;
        string nombreSalsa = (salsaMeta == TipoIngrediente.SalsaVerde) ? "Salsa Verde" : "Salsa Roja";
        bool tieneSalsa = plato.Contains(salsaMeta);
        lista += tieneSalsa ? $"[X] {nombreSalsa}\n" : $"[ ] {nombreSalsa}\n";

        // 3. Queso
        lista += plato.Contains(TipoIngrediente.Queso) ? "[X] Queso\n" : "[ ] Queso\n";

        // 4. Cebolla
        lista += plato.Contains(TipoIngrediente.Cebolla) ? "[X] Cebolla\n" : "[ ] Cebolla\n";

        // 5. Crema
        lista += plato.Contains(TipoIngrediente.Crema) ? "[X] Crema\n" : "[ ] Crema\n";

        // 6. Extra
        TipoIngrediente extraMeta = OrderManager.Instance.extraObjetivo;
        if (extraMeta != TipoIngrediente.Nada)
        {
            bool tieneExtra = plato.Contains(extraMeta);
            lista += tieneExtra ? $"[X] {extraMeta}\n" : $"[ ] {extraMeta}\n";
        }
        else
        {
            lista += "<i>(Sencillos)</i>";
        }

        textoDisplay.text = lista;
    }
}