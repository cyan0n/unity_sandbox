using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandRaycast : MonoBehaviour
{
    public GameObject raycastTrigger;
    public GameObject hand;
    private bool holdBox = false;
    public float sphereRadius = 2f;
    public LayerMask layerMask;
    private RaycastHit raycastHit;


    private float maxDistance = 1.5f;
    private float currentHitDistance; 

    //raycast debug
    private Vector3 origin;
    private Vector3 direction;

    private void OnEnable()
    {
        ThirdPersonController.Grab += grabObject;
    }

    private void OnDisable()
    {
        ThirdPersonController.Grab -= grabObject;
    }

    private void grabObject()
    {
        origin = raycastTrigger.transform.position;
        direction = raycastTrigger.transform.forward;
        RaycastHit hit;

        if (Physics.SphereCast(origin, sphereRadius, direction, out hit, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal) && !holdBox)
        {
            raycastHit = hit;
            currentHitDistance = raycastHit.distance;
            Debug.Log("miaoo");
            holdBox = true;
            raycastHit.collider.gameObject.layer = 7;
        }
        else
        {
            if(holdBox)
                raycastHit.collider.gameObject.layer = 6;

            currentHitDistance = maxDistance;
            holdBox = false;
        }
        Debug.Log(holdBox);
    }


    // Update is called once per frame
    void Update()
    {
        if (holdBox)
        {
            raycastHit.transform.position = hand.transform.position + hand.transform.forward * 1.5f + new Vector3(0, 1.17f, 0);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(origin, origin + direction * currentHitDistance);
        Gizmos.DrawWireSphere(origin + direction * currentHitDistance, sphereRadius);

    }
}
