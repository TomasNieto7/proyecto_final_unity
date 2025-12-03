using UnityEngine;

public class ComputerTrigger : MonoBehaviour
{
    public void Interactuar()
    {
        OrderManager.Instance.GenerarOrden();
    }
}