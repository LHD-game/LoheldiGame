using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcLookAt : MonoBehaviour
{


    public GameObject target;

    private Vector3 targetPosition;



    private void OnTriggerStay(Collider other)

    {

        // other.gameObject == target.gameObject

        if (other.tag == "Player")

        {

            targetPosition = new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z);

            transform.LookAt(targetPosition);

        }

    }

}
