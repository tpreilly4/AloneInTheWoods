using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class ParentFixedJoint : MonoBehaviour {

    SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device device;

    public Rigidbody rigidBodyAttachPoint;
    public Transform sphere;

    FixedJoint fixedJoint;

    void Awake () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }
	
	void FixedUpdate () {
        device = SteamVR_Controller.Input((int)trackedObj.index);

        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Debug.Log("You activated 'PressUp' on the Touchpad!");
            sphere.transform.position = new Vector3(0, 0, 0);
            sphere.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            sphere.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
        }
    }

    void OnTriggerStay(Collider col)
    {
        Debug.Log("You have collided with " + col.name + " and activated OnTriggerStay");
        if (fixedJoint == null && device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            fixedJoint = col.gameObject.AddComponent<FixedJoint>();
            fixedJoint.connectedBody = rigidBodyAttachPoint;
        }
        else if (fixedJoint !=null && device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            GameObject go = fixedJoint.gameObject;
            Rigidbody rigidBody = go.GetComponent<Rigidbody>();
            GameObject.Destroy(fixedJoint);
            fixedJoint = null;      //Need to be able to reset so that we can meet above condiiton in the future.
            tossObject(rigidBody);
        }
    }

    void tossObject(Rigidbody rigidBody)
    {
        Transform origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent; //short hand if-else. > condition ? if true : if false
        if (origin != null)
        {
            rigidBody.velocity = origin.TransformVector(device.velocity);
            rigidBody.angularVelocity = origin.TransformVector(device.angularVelocity);
        }
        else
        {
            rigidBody.velocity = device.velocity;
            rigidBody.angularVelocity = device.angularVelocity;
        }
    }
}
