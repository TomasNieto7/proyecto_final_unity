using UnityEngine;
using System.Collections.Generic;

public class TimbreEntrega : MonoBehaviour
{
    public void Interactuar()
    {
        if (OrderManager.Instance == null || !OrderManager.Instance.hayOrdenActiva)
        {
            Debug.Log("No hay ninguna orden activa. Ve a la PC primero.");
            return;
        }

        TipoIngrediente salsaDeseada = OrderManager.Instance.salsaObjetivo;
        TipoIngrediente extraDeseado = OrderManager.Instance.extraObjetivo;
        List<TipoIngrediente> loQueCocinamos = PlatoCentral.Instance.ingredientesActuales;


        bool salsaCorrecta = loQueCocinamos.Contains(salsaDeseada);

        bool extraCorrecto = false;
        if (extraDeseado == TipoIngrediente.Nada)
        {
            bool tienePollo = loQueCocinamos.Contains(TipoIngrediente.Pollo);
            bool tieneHuevo = loQueCocinamos.Contains(TipoIngrediente.Huevo);
            extraCorrecto = (!tienePollo && !tieneHuevo);
        }
        else
        {
            extraCorrecto = loQueCocinamos.Contains(extraDeseado);
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
        Debug.Log("¡PEDIDO CORRECTO! $$$");
        
        OrderManager.Instance.CompletarOrden(); 
        
        PlatoCentral.Instance.LimpiarPlato();

        DespedirCliente();
    }

    void EntregarFallo(bool salsaBien, bool extraBien)
    {
        Debug.Log("❌ ¡ORDEN INCORRECTA!");
        if (!salsaBien) Debug.Log("   - Error en la Salsa.");
        if (!extraBien) Debug.Log("   - Error en el Extra.");
        
        PlatoCentral.Instance.LimpiarPlato();
        Debug.Log("   - Plato tirado a la basura.");
    }

    void DespedirCliente()
    {
        Debug.Log("[TIMBRE] Iniciando protocolo de despedida...");


        npc[] todosLosClientes = FindObjectsByType<npc>(FindObjectsSortMode.None);
        
        if (todosLosClientes.Length == 0)
        {
            Debug.Log("[TIMBRE] No hay NPCs en la escena para despedir.");
            return;
        }

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
            Debug.Log($"[TIMBRE] Cliente encontrado a {distanciaMinima}m. ¡Adiós!");
            clienteMasCercano.Retirarse();
        }
    }
}