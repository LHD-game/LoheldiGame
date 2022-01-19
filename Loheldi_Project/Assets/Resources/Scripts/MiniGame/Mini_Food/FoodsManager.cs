using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodsManager : MonoBehaviour
{
    private static FoodsManager _instance;
    public static FoodsManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<FoodsManager>();
            }
            return _instance;
        }
    }

    private int gLevel;
    private float[] lvSpeed = new float[] {1.5f, 2.0f, 2.5f, 3.0f, 3.5f, 4.0f, 5.0f, 6.5f, 8.0f };

    private void Start()
    {
        gLevel = Timer.instance.GameLvUp();

    }

    private void Update()
    {
        FoodSpeed(gLevel);
    }

    private void FoodSpeed(int gLevel)
    {
        Vector3 nowpos = this.transform.position;
        Vector3 endpos = this.transform.position;
        endpos.y = 0.0f;
        float nowspeed = lvSpeed[gLevel];
        this.transform.position = Vector3.MoveTowards(nowpos, endpos, nowspeed*Time.deltaTime);
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        //food가 부딪힌게 Ground 태그를 가지고 있을 경우
        if(collision.gameObject.tag == "Ground")
        {
            Destroy(this.gameObject);
        }        
        //food가 부딪힌게 Player 태그를 가지고 있을 경우
        if(collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
            FoodChk();
        }

    }

    private void FoodChk()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb.gameObject.tag == "GoodFood")
        {
            GameManager.instance.ScoreCnt();
        }
        else
        {
            GameManager.instance.LifeCnt();
        }
    }

}
