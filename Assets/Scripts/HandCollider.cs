using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Box")
        {
            other.transform.Find("PickupText").GetComponent<TextMesh>().color = Color.green;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Box")
        {
            other.transform.Find("PickupText").GetComponent<TextMesh>().color = Color.red;
        }
    }

}
