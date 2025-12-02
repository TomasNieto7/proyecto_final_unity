using UnityEngine;
using TMPro; 

public class PlayerInteraction : MonoBehaviour
{
    [Header("Configuración")]
    public float distancia = 4f;
    public LayerMask capaInteractuable;

    [Header("Referencias UI")]
    public GameObject mensajeUI;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        bool haDetectadoAlgo = Physics.Raycast(ray, out hit, distancia, capaInteractuable);

        if (haDetectadoAlgo)
        {
            if (mensajeUI != null && !mensajeUI.activeSelf) 
                mensajeUI.SetActive(true);
        }
        else
        {
            if (mensajeUI != null && mensajeUI.activeSelf) 
                mensajeUI.SetActive(false);
        }

               if (haDetectadoAlgo && Input.GetKeyDown(KeyCode.E))
        {
            ComputerTrigger pc = hit.collider.GetComponent<ComputerTrigger>()
                                 ?? hit.collider.GetComponentInParent<ComputerTrigger>()
                                 ?? hit.collider.GetComponentInChildren<ComputerTrigger>();
            if (pc != null)
            {
                pc.Interactuar();
                return;
            }

            EstacionIngrediente est = hit.collider.GetComponent<EstacionIngrediente>()
                                 ?? hit.collider.GetComponentInParent<EstacionIngrediente>()
                                 ?? hit.collider.GetComponentInChildren<EstacionIngrediente>();
            if (est != null)
            {
                est.Interactuar();
                return;
            }

            TimbreEntrega timbre = hit.collider.GetComponent<TimbreEntrega>()
                                 ?? hit.collider.GetComponentInParent<TimbreEntrega>()
                                 ?? hit.collider.GetComponentInChildren<TimbreEntrega>();
            if (timbre != null)
            {
                timbre.Interactuar();
                return;
            }

        }
    }
}