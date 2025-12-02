using UnityEngine;

public class Camara : MonoBehaviour
{
    [SerializeField] public float sensibilidad = 100f;
    public Transform Player;

    public float RotacionHorizontal = 0f;
    public float RotacionVertical = 0f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float ValorX = Input.GetAxis("Mouse X")* 100 * Time.deltaTime;
        float ValorY = Input.GetAxis("Mouse Y")* 100 * Time.deltaTime;

        RotacionHorizontal += ValorX;
        RotacionVertical -= ValorY;

        RotacionVertical = Mathf.Clamp(RotacionVertical, -80, 80);


        transform.localRotation = Quaternion.Euler(RotacionVertical, 0f,0f);

        Player.Rotate(Vector3.up * ValorX); 
        
    }
}
