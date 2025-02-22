using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D body;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        body.linearVelocity = new Vector3(Input.GetAxis("Horizontal"), body.linearVelocity.y, 0);
    }
}
