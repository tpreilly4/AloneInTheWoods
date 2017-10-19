/* This functionality could probably be written as an extension to VRTK_SDKManager and VRTK_SDKSetup,
 * but both of those classes are sealed and I do not want to awaken any demons which may result of
 * unsealing those classes. - Luke 
 *
 * Courtesy of Luke Tomkus */


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

/// <summary>
/// ApplyHMDScripts makes the defined GameObject HMDScriptContainer a child of VRTK_SDKSetup's actualHeadset object.
/// You should add this after the VRTK_SDKSetup in the script execution order.
/// </summary>
public class ApplyHMDScripts : MonoBehaviour {

    [Tooltip("This is the GameObject containing the scripts you would like to attach to this particular SDKSetup's HMD.")]
    public GameObject HMDScriptContainer;

    private GameObject ActualHeadset;

	void Start()
    {
        if (ActualHeadset == null)
        {
            ActualHeadset = this.gameObject.GetComponent<VRTK_SDKSetup>().actualHeadset;
        }
        SetupHeadset();
    }

    private void SetupHeadset()
    {
        //this code is ripped straight from VRTK_Setup. All credit there.
        Action<GameObject, GameObject> setParent = (scriptAliasGameObject, actualGameObject) =>
        {
            if (scriptAliasGameObject == null)
            {
                return;
            }

            Transform scriptAliasTransform = scriptAliasGameObject.transform;
            Transform actualTransform = actualGameObject.transform;

            if (scriptAliasTransform.parent != actualTransform)
            {
                Vector3 previousScale = scriptAliasTransform.localScale;
                scriptAliasTransform.SetParent(actualTransform);
                scriptAliasTransform.localScale = previousScale;
            }

            scriptAliasTransform.localPosition = Vector3.zero;
            scriptAliasTransform.localRotation = Quaternion.identity;
        };

        if (ActualHeadset != null)
        {
            setParent(HMDScriptContainer, ActualHeadset);
        }
    }
}
