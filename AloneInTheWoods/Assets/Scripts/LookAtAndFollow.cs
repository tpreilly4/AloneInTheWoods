/* LookAtAndFollow.cs
 * 
 * When attached to a GameObject, allows GameObject to follow a target while its z
 * axis remains fixed on public Transform target, "mTarget". 
 * Object stops at EPSILON to avoid undesirable collision with HMD alias.
 * Moves in world space rather than local space, to prevent object from floating.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtAndFollow : MonoBehaviour {

    public Transform followsTarget;

    public float followSpeed = 1.5f;

    public float keepsDistance = 1.5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //get our forward in world space
        Vector3 my_forward = transform.forward;

        //clear the y so we dont move up or down
        my_forward.y = 0;

        //normalize it, so we have a unit vector even after chopping out the y bit
        my_forward = my_forward.normalized;

        //scale it by the amount we want to move
        my_forward *= followSpeed * Time.deltaTime;

        //forces gameobject to face target
        transform.LookAt(followsTarget.position);

        //do the translation in world space, as long as object is not too close
     
        if ((transform.position - followsTarget.position).magnitude > keepsDistance)
        {
            transform.Translate(my_forward, Space.World);
        }
        else
        {
            transform.Translate(0.0f, 0.0f, 0.0f);
        }
    }
}
