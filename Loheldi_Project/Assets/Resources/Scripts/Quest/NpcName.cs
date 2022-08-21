using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcName : MonoBehaviour
{
    public GameObject Npc;
    public Interaction Inter;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)             //다른 콜리더와 부딛혔을때
    {
        if (other.gameObject.name.ToString() == Npc.name)
        {
            StartCoroutine("NpcNameFollow");
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.ToString() == Npc.name)
        {
            StopCoroutine("NpcNameFollow");
        }
        
    }

    // Update is called once per frame
    /*void OnTriggerStay(Collider other)
    {
        Debug.Log("이름표");
        this.transform.position = Camera.main.WorldToScreenPoint(other.transform.position + new Vector3(0, 2f, 0));
    }*/
    IEnumerator NpcNameFollow()
    {
        while (true)
        {
            Debug.Log("이름표");
            this.transform.position = Camera.main.WorldToScreenPoint(Npc.transform.position + new Vector3(0, 2f, 0));
            yield return new WaitForSeconds(0.1f);
        }
    }
}
