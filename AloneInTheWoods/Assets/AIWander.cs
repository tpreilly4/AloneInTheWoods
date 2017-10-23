/* An AI script that forces a GameObject to wander an open area.
 * The object is constantly moving forward at rare moveSpeed * TdT.
 * Uses raycasting to detect collisions, and avoids them by rotating semi-randomly.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWander : MonoBehaviour {

    public float moveSpeed = 3.0f;
    public float obstacleRange = 5.0f;

	// Update is called once per frame
	void Update () {

        //moves object at moveSpeed
        transform.Translate(0, 0, moveSpeed * Time.deltaTime);

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.SphereCast(ray, 0.75f, out hit))
        {
            // if object is going to collide with anohter object, rotate it.
            if(hit.distance < obstacleRange)
            {
                float angle = Random.Range(-110, 110);
                transform.Rotate(0, angle, 0);
            }
        }
	}
}
