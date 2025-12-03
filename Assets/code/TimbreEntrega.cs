using UnityEngine;
using System.Collections.Generic;

public class TimbreEntrega : MonoBehaviour
{
    public void Interactuar()
    {
        if (OrderManager.Instance == null || !OrderManager.Instance.hayOrdenActiva)
        {
            Debug.Log(" No hay orden activa.");
            return;
        }

        TipoIngrediente salsaMeta = OrderManager.Instance.salsaObjetivo;
        TipoIngrediente extraMeta = OrderManager.Instance.extraObjetivo;
        List<TipoIngrediente> plato = PlatoCentral.Instance.ingredientesActuales;


        bool tieneBase = plato.Contains(TipoIngrediente.Totopos) &&
                         plato.Contains(TipoIngrediente.Queso) &&
                         plato.Contains(TipoIngrediente.Cebolla) &&
                         plato.Contains(TipoIngrediente.Crema);

        if (!tieneBase)
        {
            Debug.Log("Orden Incompleta: Faltan ingredientes base.");
            EntregarFallo(false, false); 
            return;
        }

        bool salsaCorrecta = plato.Contains(salsaMeta);

        bool extraCorrecto = false;
        if (extraMeta == TipoIngrediente.Nada)
        {
            bool tieneCarne = plato.Contains(TipoIngrediente.Pollo) || plato.Contains(TipoIngrediente.Huevo);
            extraCorrecto = !tieneCarne;
        }
        else
        {
            extraCorrecto = plato.Contains(extraMeta);
        }

        if (salsaCorrecta && extraCorrecto)
        {
            EntregarConExito();
        }
        else
        {
            EntregarFallo(salsaCorrecta, extraCorrecto);
        }
    }

    void EntregarConExito()
    {
        Debug.Log("✅ ¡PEDIDO CORRECTO! $$$");
        
        OrderManager.Instance.ModificarDinero(33);

        OrderManager.Instance.CompletarOrden(); 
        PlatoCentral.Instance.LimpiarPlato();

        DespedirCliente();
    }

    void EntregarFallo(bool salsaBien, bool extraBien)
    {
        Debug.Log("❌ ¡ORDEN INCORRECTA! Multa aplicada.");
        
        OrderManager.Instance.ModificarDinero(-15);

        if (!salsaBien) Debug.Log("   - Error en la Salsa.");
        if (!extraBien) Debug.Log("   - Error en el Extra.");
        
        PlatoCentral.Instance.LimpiarPlato();
    }

    void DespedirCliente()
    {
        Debug.Log("[TIMBRE] Buscando cliente para despedir...");

        npc[] todosLosClientes = FindObjectsByType<npc>(FindObjectsSortMode.None);
        
        if (todosLosClientes.Length == 0) return;

        npc clienteMasCercano = null;
        float distanciaMinima = Mathf.Infinity;
        Vector3 miPosicion = transform.position;

        foreach (npc cliente in todosLosClientes)
        {
            if (cliente.seVa) continue; 

            float dist = Vector3.Distance(cliente.transform.position, miPosicion);
            if (dist < distanciaMinima)
            {
                distanciaMinima = dist;
                clienteMasCercano = cliente;
            }
        }

        if (clienteMasCercano != null && distanciaMinima < 25.0f)
        {
            clienteMasCercano.Retirarse();
        }
    }
}