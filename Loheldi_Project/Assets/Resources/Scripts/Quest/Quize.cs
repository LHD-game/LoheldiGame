using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class Quize : MonoBehaviour
{
    public GameObject Button;
    public GameObject OImage;
    public GameObject XImage;
    public Text text;

    public void O()
    {
        OImage.SetActive(true);
        Button.SetActive(false);

    }

    public void X()
    {
        XImage.SetActive(true);
        Button.SetActive(false);
        text.text = "�ٽ� �����غ���!";

    }

    public void Xback()
    {
        XImage.SetActive(false);
        Button.SetActive(false);
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
