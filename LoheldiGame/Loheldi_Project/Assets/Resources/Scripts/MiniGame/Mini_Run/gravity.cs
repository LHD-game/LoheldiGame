using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravity : MonoBehaviour
{
    public GameObject block1;
    public GameObject block2;
    public GameObject block3;
    public GameObject block4;
    float forceGravity = 2000f;
    Rigidbody Prigid;

    private bool gravityTF = false;
    // Start is called before the first frame update

    private void Start()
    {
        Prigid =GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (gravityTF)
        {
            gravityControll();
        }
    }
    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject == block1)
        {
            gravityTF =true;
        }

        if (other.gameObject == block2)
        {
            gravityTF = true;
        }

        if (other.gameObject == block3)
        {
            gravityTF = true;
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == block1)
        {
            gravityTF = false;
            Prigid.AddForce(0, 1, 0);
        }

        if (other.gameObject == block2)
        {
            gravityTF = false;
            Prigid.AddForce(0, 1, 0);
        }

        if (other.gameObject == block3)
        {
            gravityTF = false;
            Prigid.AddForce(0, 1, 0);
        }
    }
    public void gravityControll()
    {
        Prigid.AddForce(Vector3.down * forceGravity);
    }
 }



