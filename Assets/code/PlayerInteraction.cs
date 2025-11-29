using UnityEngine;
using TMPro; // Necesario para controlar el texto

public class PlayerInteraction : MonoBehaviour
{
    [Header("Configuración")]
    public float distancia = 4f;
    public LayerMask capaInteractuable;

    [Header("Referencias UI")]
    public GameObject mensajeUI; // Arrastra aquí tu objeto de texto "MensajeInteraccion"

    void Update()
    {
        // 1. Lanzamos el rayo CONSTANTEMENTE (no solo al presionar E)
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // Variable para saber si golpeamos algo válido
        bool haDetectadoAlgo = Physics.Raycast(ray, out hit, distancia, capaInteractuable);

        // 2. Lógica Visual (Mostrar u Ocultar Texto)
        if (haDetectadoAlgo)
        {
            // Si el mensaje está apagado, lo prendemos
            if (mensajeUI != null && !mensajeUI.activeSelf) 
                mensajeUI.SetActive(true);
        }
        else
        {
            // Si no miramos nada y el mensaje está prendido, lo apagamos
            if (mensajeUI != null && mensajeUI.activeSelf) 
                mensajeUI.SetActive(false);
        }

        // 3. Lógica de Acción (Presionar la tecla)
               if (haDetectadoAlgo && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log($"E pressed. Collider golpeado: {hit.collider.name} (gameObject: {hit.collider.gameObject.name})");

            // Buscar componente en collider, luego en padres y luego en hijo
            ComputerTrigger pc = hit.collider.GetComponent<ComputerTrigger>()
                                 ?? hit.collider.GetComponentInParent<ComputerTrigger>()
                                 ?? hit.collider.GetComponentInChildren<ComputerTrigger>();
            if (pc != null)
            {
                Debug.Log($"Interacting with Computer (found on: {pc.gameObject.name})");
                pc.Interactuar();
                return;
            }

            EstacionIngrediente est = hit.collider.GetComponent<EstacionIngrediente>()
                                 ?? hit.collider.GetComponentInParent<EstacionIngrediente>()
                                 ?? hit.collider.GetComponentInChildren<EstacionIngrediente>();
            if (est != null)
            {
                Debug.Log($"Interacting with EstacionIngrediente (found on: {est.gameObject.name}) ingrediente: {est.ingredienteQueDoy}");
                est.Interactuar();
                return;
            }

            TimbreEntrega timbre = hit.collider.GetComponent<TimbreEntrega>()
                                 ?? hit.collider.GetComponentInParent<TimbreEntrega>()
                                 ?? hit.collider.GetComponentInChildren<TimbreEntrega>();
            if (timbre != null)
            {
                Debug.Log($"Interacting with TimbreEntrega (found on: {timbre.gameObject.name})");
                timbre.Interactuar();
                return;
            }

        }
    }
}