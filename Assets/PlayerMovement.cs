using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public Transform cam;

    public float health = 100;

    public float mana = 100;

    public float stamina = 50;

    #region UniqueAbility

    string playerAbility;

    /// <summary>
    /// This is the Unique ability variable that determines the percent to be applied to the specifc attributes determined by the rest of the ability.
    /// </summary>
    float[] abilityPercent = new float[] { 25, 50, 75, 100 };

    /// <summary>
    /// This is the Unique ability variable that determines what typing the ability will have.  
    /// </summary>
    string[] abilityType = new string[] { "Discount", "Invulnerability" };

    /// <summary>
    /// This is the Unique ability variable that determines where to apply the ability.
    /// </summary>
    string[] abilityAttribute = new string[] { "Mana", "Health", "Stamina" };

    /// <summary>
    /// This is the unique ability variable that determines how long the ability lasts for in seconds.
    /// </summary>
    float[] abilityDuration = new float[] { .25f, .5f, .75f, 1 };
#endregion


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

    void AssignPlayerAbility()
    {
        int abilPerc = Random.Range(-1, 5);

        int abilType = Random.Range(-1, 2);

        int abilAtt = Random.Range(-1, 3);

        int abilLength = Random.Range(-1, 5);

        float percent = abilityPercent[abilPerc];

        string type = abilityType[abilType];

        string attribute = abilityAttribute[abilAtt];

        float duration = abilityDuration[abilLength];

        playerAbility = type + percent + attribute + duration;
    }
}
