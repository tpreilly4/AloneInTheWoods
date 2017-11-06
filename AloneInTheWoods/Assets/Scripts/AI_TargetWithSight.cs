using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A "smart" AI script that gives a gameobject "sight".
 * Creates a Transform that 
 */

public class AI_TargetWithSight : MonoBehaviour {

    //will turn from objects at this distance
    float obstacleRange = 1.0f;

    //will see player at this distance within spherecast
    public float sightRange = 15.0f;

    public float moveSpeed = 1.5f;

    //stays this far away from player
    public float keepsDistance = 1.5f;

    //player (or other GameObject to be chased/targeted)
    public GameObject player;

    void Update()
    {

        // move this GameObject forward
        Vector3 my_forward = transform.forward * moveSpeed * Time.deltaTime;
      
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        //Make sure enemy is not within keepsDistance
        if ((transform.position - player.transform.position).magnitude > keepsDistance)
        {
            transform.Translate(my_forward, Space.World);

            //make sure we can spherecast
            if (Physics.SphereCast(ray, 1.75f, out hit))
            {
                //the object that gets hit with spherecast
                GameObject hitObject = hit.transform.gameObject;

                // if gameobj can see player, follow player's position. Only rotates on y axis.
                if (hitObject.CompareTag("Player") && hit.distance < sightRange)
                {
                    Vector3 targetPosition = new Vector3(hitObject.transform.position.x,
                                             this.transform.position.y,
                                             hitObject.transform.position.z);
                    this.transform.LookAt(targetPosition);
                    Debug.Log("Can see player.");
                }
                // if object is going to collide with another object that isn't the player, rotate it semi-randomly.
                if (hit.distance < obstacleRange && !hitObject.CompareTag("Player"))
                {
                    Debug.Log("About to collide with:\n" + hitObject.ToString() + "\nNow Rotating!");
                    float angle = Random.Range(-110, 110);
                    transform.Rotate(0, angle, 0);
                }
            }
        }
        else
        {
            transform.Translate(0.0f, 0.0f, 0.0f);
        }

        
    }
}

