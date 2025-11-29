using UnityEngine;

public class TimbreEntrega : MonoBehaviour
{
    public void Interactuar()
    {
        if (!OrderManager.Instance.hayOrdenActiva) return;

        // Verificar Salsa
        bool salsaCorrecta = PlatoCentral.Instance.ingredientesActuales.Contains(OrderManager.Instance.salsaObjetivo);
        
        // Verificar Extra (Si pidió nada, verificamos que NO haya extras)
        bool extraCorrecto = false;
        if (OrderManager.Instance.extraObjetivo == TipoIngrediente.Nada)
        {
            extraCorrecto = !PlatoCentral.Instance.ingredientesActuales.Contains(TipoIngrediente.Pollo) &&
                            !PlatoCentral.Instance.ingredientesActuales.Contains(TipoIngrediente.Huevo);
        }
        else
        {
            extraCorrecto = PlatoCentral.Instance.ingredientesActuales.Contains(OrderManager.Instance.extraObjetivo);
        }

        if (salsaCorrecta && extraCorrecto)
        {
            Debug.Log("¡PEDIDO CORRECTO! $$$");
            OrderManager.Instance.CompletarOrden();
            PlatoCentral.Instance.LimpiarPlato();
        }
        else
        {
            Debug.Log("PEDIDO INCORRECTO (Se tira a la basura)");
            PlatoCentral.Instance.LimpiarPlato();
        }
    }
}