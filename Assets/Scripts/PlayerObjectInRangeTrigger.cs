using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectInRangeTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Box")
        {
            other.transform.Find("PickupText").GetComponent<MeshRenderer>().enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.Find("PickupText").GetComponent<MeshRenderer>().enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
