using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StageManager : MonoBehaviour
{
    public ClickUserInfo cu;

    public void OnClickBox()
    {
        string nowbutton = EventSystem.current.currentSelectedGameObject.name;
        if (nowbutton == "Userinfo") cu.Login = 1;
        else if (nowbutton == "clickBack") cu.Login = 2;

        if (cu.Login != 0) cu.ChangeUserInfo();
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
