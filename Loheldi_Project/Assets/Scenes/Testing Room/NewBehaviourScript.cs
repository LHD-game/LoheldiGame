using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public ParticleSystem ps;

    void Start()
    {
       
        ps.Play();
        ps.transform.SetParent(this.transform);
        ps.transform.position = this.transform.position;
    }


    void Update()
    {
        
        
        
    }
}
