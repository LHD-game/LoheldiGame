using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetE : MonoBehaviour
{
    
    

    public void Go()
    {
        List<Dictionary<string, object>> data_Dialog = CSVReader.Read("Scripts/Quest/Dialog");

        for (int i = 0; i < data_Dialog.Count; i++)
        {
            Debug.Log(data_Dialog[i]["dialog".ToString()]);
        }
    }
}
