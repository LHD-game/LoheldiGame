using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGenderator : MonoBehaviour
{
    float timer = 0.0f;
    // Start is called before the first frame update
    public GameObject StonePrefeb; //GameObject ����
    public QuestScript chat;

    // Update is called once per frame
    private void Start()
    {
        chat = GameObject.Find("chatManager").GetComponent<QuestScript>();
    }
    void FixedUpdate()
    {
        if(chat.note)
        if (timer>2)
        {   //stone�� �����ϰ� �߻�!
            GameObject stone = Instantiate(StonePrefeb, new Vector3(-95, 17, 147), Quaternion.Euler(-50, -90, 0));
            stone.GetComponent<Throw>().Shoot(new Vector3(UnityEngine.Random.Range(-300, 300), 200, -200));
            timer = 0;
            GameObject[] objs = GameObject.FindGameObjectsWithTag("note");
            if (objs.Length > 3)
            {
                Destroy(objs[0]);
            }
        }
        timer += Time.deltaTime;
    }
}
