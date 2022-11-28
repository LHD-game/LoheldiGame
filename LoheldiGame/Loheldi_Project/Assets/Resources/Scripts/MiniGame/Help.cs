using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Help : MonoBehaviour
{
    [SerializeField]
    private GameObject HelpPanel;

    public void ShowHelp()
    {
        HelpPanel.SetActive(true);
    }

    public void HideHelp()
    {
        Debug.Log("hidehelp");
        HelpPanel.SetActive(false);
    }
}
