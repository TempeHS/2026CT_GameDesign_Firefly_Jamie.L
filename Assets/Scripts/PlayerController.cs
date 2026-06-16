using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    public static string targetSpawn = "";

    private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (targetSpawn != "")
        {
            GameObject spawn = GameObject.Find(targetSpawn);
            if (spawn != null)
            {
                transform.position = spawn.transform.position;
            }
            targetSpawn = "";
        }
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = moveInput * moveSpeed;

    }

    public void move(InputAction.CallbackContext context)
    {
        animator.SetBool("isWalking", true);
        if (context.canceled)
        {
            animator.SetBool("isWalking", false);
            animator.SetFloat("LastInputX", moveInput.x);
            animator.SetFloat("LastInputY", moveInput.y);
        }
        moveInput = context.ReadValue<Vector2>();
        animator.SetFloat("InputX", moveInput.x);
        animator.SetFloat("InputY", moveInput.y);

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "bath-bed")
        {
            targetSpawn = "BedroomDoorSpawn";
            SceneManager.LoadScene("Main Bedroom");
        }
        if (other.tag == "bed-bath")
        {
            targetSpawn = "BathroomDoorSpawn";
            SceneManager.LoadScene("Bathroom");
        }

    }
}