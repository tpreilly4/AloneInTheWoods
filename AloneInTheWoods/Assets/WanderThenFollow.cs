using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * CURRENTLY NON FUNCTIONAL
 */

public class WanderThenFollow : MonoBehaviour {

    //public Transform followsTarget;
    public float moveSpeed = 3.0f;
    public float obstacleRange = 5.0f;
    //public float targetRange = 50.f;

    void Update()
    {
        //moves object at moveSpeed
        transform.Translate(0, 0, moveSpeed * Time.deltaTime);

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.SphereCast(ray, 0.75f, out hit))
        {
            if (hit.transform.tag == "Player")
            {
                Debug.Log("Can see player");
            }
            // if object is going to collide with anohter object, rotate it.
            if (hit.distance < obstacleRange)
            {
                float angle = Random.Range(-110, 110);
                transform.Rotate(0, angle, 0);
            }
        }
    }
}

