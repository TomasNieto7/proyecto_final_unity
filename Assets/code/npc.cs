using UnityEngine;

public class npc : MonoBehaviour
{
    private Animator animator;
    
    public float velocidad = 2.0f;
    public float distanciaParaParar = 1.5f; // Distancia a la que se detendrá del mostrador

    private bool detenerse = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 1. DETECTAR OBSTÁCULO (El "Láser")
        // Creamos un rayo desde el centro del personaje hacia adelante
        Ray rayo = new Ray(transform.position + Vector3.up * 0.1f, transform.forward); 
        RaycastHit informacionDelGolpe;

        // Dibujamos el rayo en la escena (solo se ve en la pestaña Scene de Unity, linea roja)
        Debug.DrawRay(rayo.origin, rayo.direction * distanciaParaParar, Color.red);

        // Verificamos si el rayo golpea algo a la distancia especificada
        if (Physics.Raycast(rayo, out informacionDelGolpe, distanciaParaParar))
        {
            // Si lo que golpeamos tiene la etiqueta "Mostrador"
            if (informacionDelGolpe.collider.CompareTag("Mostrador"))
            {
                detenerse = true;
            }
        }

        // 2. MOVERSE O PARAR
        if (!detenerse)
        {
            // Avanzar
            transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
            animator.SetBool("isWalking", true);
        }
        else
        {
            // Parar (Idle)
            animator.SetBool("isWalking", false);
        }
    }
}
