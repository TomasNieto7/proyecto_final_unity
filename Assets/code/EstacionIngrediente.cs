using UnityEngine;
using UnityEngine.UI;

public class EstacionIngrediente : MonoBehaviour
{
    [Header("Configuración")]
    public TipoIngrediente ingredienteQueDoy;
    public Sprite icono; // Imagen del ingrediente
    public GameObject prefabUI; // El Canvas flotante
    public Transform puntoFlotante; // Objeto vacío encima de la estación

    void Start()
    {
        if (prefabUI != null && puntoFlotante != null)
        {
            GameObject ui = Instantiate(prefabUI, puntoFlotante.position, Quaternion.identity);
            ui.transform.SetParent(puntoFlotante);
            ui.GetComponentInChildren<Image>().sprite = icono;
        }
    }

    public void Interactuar()
    {
        // Enviar ingrediente al plato central
        Debug.Log("Estación de ingrediente: " + ingredienteQueDoy.ToString() + " ha sido interactuada.");
        if (PlatoCentral.Instance != null)
        {
            Debug.Log("Agregando ingrediente al plato central: " + ingredienteQueDoy.ToString());
            PlatoCentral.Instance.AgregarIngrediente(ingredienteQueDoy);
        }
    }
}