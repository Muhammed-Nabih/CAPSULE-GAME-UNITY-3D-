using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody move;
    [SerializeField] float speedd = 6f;
    [SerializeField] float speeddJump = 5f;

    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;

    [SerializeField] AudioSource jumpSound;

    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        float horInput = Input.GetAxis("Horizontal");
        float verInput = Input.GetAxis("Vertical");

        move.velocity = new Vector3(horInput * speedd, move.velocity.y, verInput * speedd);

        if (Input.GetButtonDown("Jump") && IsGrounded() )
        {
            Jump();
        }
        
    }

    void Jump()
    {
        move.velocity = new Vector3(move.velocity.x, speeddJump, move.velocity.z);
        jumpSound.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy Head"))
        {
            Destroy(collision.transform.parent.gameObject);
            Jump();
        }
    }
   
    bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, .1f, ground);
    }
   
}
