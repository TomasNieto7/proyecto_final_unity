using UnityEngine;

public class npc : MonoBehaviour
{
    private Animator animator;
    
    public Transform objetivoDestino; 

    public float velocidad = 2.0f;
    public float distanciaParaParar = 1.5f;

    // Ya no necesitamos inicializarla aquí con valor, se calcula en cada frame
    private bool detenerse; 

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 1. REINICIAR ESTADO:
        // Asumimos que en este frame PODEMOS caminar, a menos que el raycast diga lo contrario.
        // Esto es vital para que vuelvan a avanzar si la fila se mueve.
        detenerse = false; 

        // 2. DETECTAR OBSTÁCULO
        Ray rayo = new Ray(transform.position + Vector3.up * 0.1f, transform.forward); 
        RaycastHit informacionDelGolpe;

        Debug.DrawRay(rayo.origin, rayo.direction * distanciaParaParar, Color.red);
        
        // 

        if (Physics.Raycast(rayo, out informacionDelGolpe, distanciaParaParar))
        {
            // AQUI ESTA EL CAMBIO:
            // Usamos el operador "||" (O) para checar si es el Mostrador O si es otro npc
            if (informacionDelGolpe.collider.CompareTag("Mostrador") || informacionDelGolpe.collider.CompareTag("npc"))
            {
                detenerse = true;
            }
        }

        // 3. MOVERSE O PARAR
        if (!detenerse)
        {
            if (objetivoDestino != null)
            {
                Vector3 posicionPlana = new Vector3(objetivoDestino.position.x, transform.position.y, objetivoDestino.position.z);
                transform.LookAt(posicionPlana);
            }

            transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
            animator.SetBool("isWalking", true);
        }
        else
        {
            // Si detenerse es true (porque hay alguien enfrente), entramos aquí
            animator.SetBool("isWalking", false);
        }
    }
}