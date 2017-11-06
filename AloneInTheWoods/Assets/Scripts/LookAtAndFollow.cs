/* LookAtAndFollow.cs
 * 
 * When attached to a GameObject, allows GameObject to follow a target while its z
 * axis remains fixed on public Transform target, "mTarget". 
 * Object stops at keepsDistance to avoid undesirable collision with object being followed.
 * Moves in world space rather than local space, to prevent object from floating.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtAndFollow : MonoBehaviour {

    public Transform followsTarget;
    public float followSpeed = 1.5f;
    public float keepsDistance = 1.5f;
	
	void Update () {

        //turns forward in worldspace into a nicely named vector, my_forward
        Vector3 my_forward = transform.forward;

        //clears the y from my_forward to make it a 2-dimensional array, so object doesnt move up or down
        my_forward.y = 0;

        //normalize it, so we have a unit vector even after chopping out the y bit
        my_forward = my_forward.normalized;

        //scale it by followSpeed to determine the rate the object moves at
        my_forward *= followSpeed * Time.deltaTime;

        //forces gameobject to face target
        //transform.LookAt(followsTarget.position);
        transform.Rotate(followsTarget.position.x, 0, followsTarget.position.z);

        //make the translation in world space, as long as object is keepsDistance away.
        //keepsDistacne avoids undesirable 
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
