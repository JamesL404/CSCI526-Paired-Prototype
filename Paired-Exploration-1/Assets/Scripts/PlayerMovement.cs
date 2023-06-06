using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] int lives = 3;
    private bool alive = true;
    public float speed = 50f;
    public Rigidbody rb;

    private float horizontalInput;
    [SerializeField] float horizontalMultiplier = 2;

    [SerializeField] float jumpForce = 1200f;
    [SerializeField] LayerMask groundMask;
<<<<<<< Updated upstream
    private float speedMax = 75f;
=======
    private float speedMax = 100f;
    
    [SerializeField] private TMP_Text livesCounter;
>>>>>>> Stashed changes

    void Start()
    {
        lives = 3;
        alive = true;
    }

    void FixedUpdate()
    {

        if (!alive)
        {
            return;
        }
        if (speed < speedMax)
        {
            speed += 1f;
        }
        Vector3 moveForward = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier;
        rb.MovePosition(rb.position + moveForward + horizontalMove);
        
    }


    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        livesCounter.text = "Lives: "  + lives.ToString();

    }

    void Jump()
    {
        //Check if the player is on the ground
        float height = GetComponent<Collider>().bounds.size.y;
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, (height/2)+0.1f, groundMask);
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce);
        }
    }

    public void ReduceLife()
    {
        lives -= 1;
        if (lives == 0)
        {
            Die();
        }

        speed = 50f;
        
    }
    private void Die()
    {
        alive = false;
        Invoke("Restart", 2);
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
