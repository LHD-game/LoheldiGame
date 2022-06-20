using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RunCountDown : MonoBehaviour
{
    public GameObject DifficultyPanel;
    public GameObject RunBtnPanel;

    public GameObject Num;
    /*public GameObject Num_1;
    public GameObject Num_2;
    public GameObject Num_3;*/
    private int timer = 3;
    public static bool CountEnd = false;

    public GameObject SoundManager;

    public GameObject TrafficLight;
    public GameObject[] Light;
    public Material Light_material;
    [SerializeField]
    private Material[] material;
    int MataNum = 0;                        //메터리얼 번호

    void Start()
    {
        ResetTimer();
    }
    
    public void CountNum()
    {
        DifficultyPanel.gameObject.SetActive(false);
        RunBtnPanel.SetActive(true);
        //Num.SetActive(true);

        InvokeRepeating("NumAppear", 0f, 1f);
    }

    private void NumAppear()
    {
        TrafficLight.gameObject.SetActive(true);
        if (timer == 3)
        {
            MataNum=0;
            Light_material = material[MataNum]; //0에 메테리얼 번호
            Light[MataNum].GetComponent<MeshRenderer>().material = Light_material;
            SoundManager.GetComponent<SoundEffect>().Sound("RunCount");
            //Num.SetActive(true);
        }
        else if(timer == 2)
        {
            MataNum = 1;
            Light_material = material[MataNum]; //0에 메테리얼 번호
            Light[MataNum].GetComponent<MeshRenderer>().material = Light_material;
            //Num_3.SetActive(true);
            SoundManager.GetComponent<SoundEffect>().Sound("RunCount");
        }
        else if (timer == 1)
        {
            MataNum = 2;
            Light_material = material[MataNum]; //0에 메테리얼 번호
            Light[MataNum].GetComponent<MeshRenderer>().material = Light_material;
            //Num_2.SetActive(true);
            SoundManager.GetComponent<SoundEffect>().Sound("RunCount");
        }
        else if (timer == 0)
        {
            Light_material = material[2];
            //for (int i = 0; i < 1; i++)
                Light[0].GetComponent<MeshRenderer>().material = Light_material;
                Light[1].GetComponent<MeshRenderer>().material = Light_material;
            //Num_1.SetActive(true);
            SoundManager.GetComponent<SoundEffect>().Sound("RunCountFinish");
        }
        else if(timer == -1)
        {
            //RunBtnPanel.SetActive(false);
            Num.SetActive(false);
            CountEnd = true;
            //SoundManager.GetComponent<SoundEffect>().Sound("RunCountFinish");
            CancelInvoke("NumAppear");
            TrafficLight.gameObject.SetActive(false);
        }
        timer--;
    }

    public void ResetTimer()
    {
        Light_material = material[3];
        timer = 3;
        CountEnd = false;
        Num.SetActive(false);
        Light[0].GetComponent<MeshRenderer>().material = Light_material;
        Light[1].GetComponent<MeshRenderer>().material = Light_material;
        Light[2].GetComponent<MeshRenderer>().material = Light_material;
        CancelInvoke("NumAppear");
        TrafficLight.gameObject.SetActive(false);
    }
}