using UnityEngine;

public class npc : MonoBehaviour
{
    private Animator animator;
    
    [Header("Configuración de Movimiento")]
    public Transform objetivoDestino; 
    public Transform puntoSalida;    
    public float velocidad = 2.0f;
    public float distanciaParaParar = 1.5f;

    private bool detenerse;
    public bool seVa = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Retirarse()
    {
        Debug.Log($"[NPC {gameObject.name}] ¡Me retiro!");

        if (puntoSalida == null)
        {
            Debug.LogError($"[NPC ERROR] {gameObject.name} quiere irse pero NO TIENE 'Punto Salida' asignado.");
            return;
        }

        seVa = true;
        detenerse = false; 
        
        gameObject.tag = "Untagged"; 
    }

    void Update()
    {
        if (seVa)
        {
            if (puntoSalida != null)
            {
                Vector3 salidaPos = new Vector3(puntoSalida.position.x, transform.position.y, puntoSalida.position.z);
                transform.LookAt(salidaPos);
                transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
                
                if(animator != null) animator.SetBool("isWalking", true);

                if (Vector3.Distance(transform.position, salidaPos) < 1.0f)
                {
                    Debug.Log("[NPC] Llegué a la salida!");
                    Destroy(gameObject);
                }
            }
            return; 
        }

        
        detenerse = false; 

        Ray rayo = new Ray(transform.position + Vector3.up * 0.5f, transform.forward); 
        RaycastHit informacionDelGolpe;
        
        Debug.DrawRay(rayo.origin, rayo.direction * distanciaParaParar, Color.red);

        if (Physics.Raycast(rayo, out informacionDelGolpe, distanciaParaParar))
        {
            if (informacionDelGolpe.collider.CompareTag("Mostrador") || informacionDelGolpe.collider.CompareTag("npc"))
            {
                detenerse = true;
            }
        }

        if (!detenerse)
        {
            if (objetivoDestino != null)
            {
                Vector3 posicionPlana = new Vector3(objetivoDestino.position.x, transform.position.y, objetivoDestino.position.z);
                transform.LookAt(posicionPlana);
            }

            transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
            if(animator != null) animator.SetBool("isWalking", true);
        }
        else
        {
            if(animator != null) animator.SetBool("isWalking", false);
        }
    }
}