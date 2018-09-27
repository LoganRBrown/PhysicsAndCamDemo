using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalCamera : MonoBehaviour {

    /// <summary>
    /// Controls the pitch of the camera.
    /// </summary>
    float pitch = 0;

    /// <summary>
    /// Controls the yaw of the camera.
    /// </summary>
    float yaw = 0;

    public float pitchPercent
    {
        get
        {
            return (pitch - pitchMin) / (pitchMax - pitchMin);
        }
    }

    public float pitchMin = -80;
    public float pitchMax = 80;

    public float pitchSensitivity = 1;
    public float yawSensitivity = 5;

    public bool pitchInvert = true;
    public bool yawInvert = false;

    Transform cam;

    /// <summary>
    /// The target localPosition value for child camera object.
    /// </summary>
    Vector3 dollyPosition;

    public float dollyEaseMultiplier = 10;
    public float dollyMaxDistance = 20;
    public float dollyMinDistance = 10;

	// Use this for initialization
	void Start () {
        cam = GetComponentInChildren<Camera>().transform;
        dollyPosition = cam.localPosition;
	}

    void Update()
    {
        LookAround();

        dollyPosition += new Vector3(0, 0, Input.mouseScrollDelta.y);
        dollyPosition.z = Mathf.Clamp(dollyPosition.z, -dollyMaxDistance, -dollyMinDistance);

        cam.localPosition = Vector3.Lerp(cam.localPosition, dollyPosition, Time.deltaTime * dollyEaseMultiplier);


    }
	
	// Update is called once per frame
	void LookAround()
    {

        float lookX = Input.GetAxis("Mouse X");
        float lookY = Input.GetAxis("Mouse Y");

        pitch += lookY * pitchSensitivity * (pitchInvert ? -1 : 1);
        yaw += lookX * yawSensitivity * (yawInvert ? -1 : 1);

        pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);

        transform.eulerAngles = new Vector3(pitch, yaw, 0);
	}

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, Vector3.one);
    }
}
