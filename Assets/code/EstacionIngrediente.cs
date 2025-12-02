using UnityEngine;
using UnityEngine.UI;

public class EstacionIngrediente : MonoBehaviour
{
    [Header("Configuración")]
    public TipoIngrediente ingredienteQueDoy;
    public Sprite icono;
    public GameObject prefabUI; 
    public Transform puntoFlotante; 

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
        if (PlatoCentral.Instance != null)
        {
            PlatoCentral.Instance.AgregarIngrediente(ingredienteQueDoy);
        }
    }
}