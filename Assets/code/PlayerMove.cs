using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float runSpeed = 5;
    public float rotationSpeed = 250;

    public Animator animator;

    private float x, y;

    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        transform.Rotate(0, x * rotationSpeed * Time.deltaTime, 0);
        transform.Translate(0, 0, y * runSpeed * Time.deltaTime * runSpeed);
    }
}
