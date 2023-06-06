using UnityEngine;

public class Collision : MonoBehaviour
{
    PlayerMovement playerMovement;
    
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
    }

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            playerMovement.ReduceLife();
            float width = GetComponent<Collider>().bounds.size.z;
            Vector3 obsWidth = new Vector3(0, 0, width);
            playerMovement.rb.MovePosition(playerMovement.rb.position + obsWidth);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
