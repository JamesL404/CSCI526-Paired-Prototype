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

    [SerializeField] float jumpForce = 4000f;
    [SerializeField] LayerMask groundMask;
    private float speedMax = 75f;
    [SerializeField] private TMP_Text livesCounter;
    
    //Pause button
    private bool isPaused = false;
    
    //Stalker Teleport - Each player start game with 1 teleport ability
    private int teleportCount = 1;
    private float minDistForTeleport = 0.0f;
    private float maxDistForTeleport = 50.0f;
    private float teleportDist = 40.0f;

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
        if ((rb.position + horizontalMove).x < -20)
        {
            horizontalMove.x = -20 - rb.position.x;
        }
        else if ((rb.position + horizontalMove).x > 20)
        {
            horizontalMove.x = 20 - rb.position.x; 
        }
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
        
        //Pause game by pressing 'P'
        if (Input.GetKeyDown(KeyCode.P) && !isPaused)
        {
            Time.timeScale = 0;
            isPaused = true;
        }
        else if (Input.GetKeyDown(KeyCode.P) && isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
        }
        
        //Stalker Teleport
        if (Input.GetKeyDown((KeyCode.T)) /*&& teleportCount > 0*/)
        {
            teleportCount--;
            Teleport();
        }

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

    void Teleport()
    {
        //Shoot ray along z-axis
        Ray zRay = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(zRay, out hit))
        {
            //Get current distance to nearest obstacle
            float dist = hit.distance;
            if (dist > minDistForTeleport && dist < maxDistForTeleport)
            {
                //Stalker Teleport!
                transform.Translate( new Vector3(0,0,teleportDist));
            }
        }
    }
}
