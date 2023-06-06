using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicObstaclesController : MonoBehaviour
{
    public float xRange = 20.0f;

    public float obstacleSpeed = 40.0f;

    private bool moveLeft = false;
    // Start is called before the first frame update
    void Start()
    {
        moveLeft = true;
    }

    // Update is called once per frame
    void Update()
    {
        //1. Direction to move
        if (moveLeft)
        {
            transform.Translate(Vector3.left * Time.deltaTime * obstacleSpeed);
        }
        else if (!moveLeft)
        {
            transform.Translate(Vector3.right * Time.deltaTime * obstacleSpeed);
        }
        
        //2. Check condition if direction should change
        if (transform.position.x >= xRange)
        {
            moveLeft = true;
        }
        else if (transform.position.x <= -xRange)
        {
            moveLeft = false;
        }
    }
}
