using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public Transform cam;

    Rigidbody body;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody>();
	}

    void Update()
    {
        MoveAround();

        if (Input.GetButtonDown("Jump"))
        {
            body.AddForce(Vector3.up * 5, ForceMode.Impulse);
        }
    }
	
	// Update is called once per frame
	void MoveAround ()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 ballForward = cam.forward;
        ballForward.y = 0;
        ballForward.Normalize();

        Vector3 ballRight = new Vector3(ballForward.z, 0, -ballForward.x);

        Vector3 move = ballForward * v + ballRight * h ;

        Vector3 torque = Vector3.Cross(Vector3.up, move);

        //body.AddForce(move * Time.deltaTime * 1000);
        body.AddTorque(torque * Time.deltaTime * 1000);
	}
}
