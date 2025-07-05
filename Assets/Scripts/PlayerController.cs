using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rbody;

    public float moveSpeed = 5.0f;
    public float jumpHeight = 2.0f;
    public float groundChekcDistance = 0.1f;

    bool isGrounded;

    float axisH;
    float axisV;
    Vector3 move;

    bool isJump;

    public LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        axisH = Input.GetAxisRaw("Horizontal");
        axisV = Input.GetAxisRaw("Vertical");
        move = new Vector3(axisH,0,axisV) * moveSpeed;

        // 向き変更
        if (move != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(move);
        }

        if (Input.GetButtonDown("Jump"))
        { 
            if(isGrounded)
            isJump = true;
        }
    }

    private void FixedUpdate()
    {
        // 地面判定（カプセルの底から下へRayを飛ばす）
        isGrounded = Physics.Raycast(
            transform.position - new Vector3(0,-0.5f,0),
            Vector3.down,
            1.0f,
            groundLayer
        );

        if (isGrounded)
            Debug.Log("地面");

        rbody.velocity = new Vector3(move.x,rbody.velocity.y,move.z);

        if(isJump)
        { 
            rbody.AddForce((new Vector3(0, jumpHeight, 0)), ForceMode.Impulse);
            isJump = false;
        }
    }
}
