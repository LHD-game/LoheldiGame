using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class ParsingJSON
{
    public T ParseBackendData<T>(JsonData json, bool isChart = false) where T : new()
    {
        T data = new T();
        var fields = typeof(T).GetFields();
        for (int i = 0; i < fields.Length; ++i)
        {
            if (fields[i].FieldType == typeof(int))
            {
                if (json.Keys.Contains(fields[i].Name))
                {
                    if (isChart)
                        fields[i].SetValue(data, int.Parse(json[fields[i].Name]["S"].ToString()));
                    else
                        fields[i].SetValue(data, int.Parse(json[fields[i].Name]["N"].ToString()));
                }
                else
                    fields[i].SetValue(data, 0);
            }
            else if (fields[i].FieldType == typeof(string))
            {
                if (json.Keys.Contains(fields[i].Name))
                    fields[i].SetValue(data, json[fields[i].Name]["S"].ToString());
                else
                    fields[i].SetValue(data, "");
            }
            else if (fields[i].FieldType == typeof(float))
            {
                if (json.Keys.Contains(fields[i].Name))
                {
                    if (isChart)
                        fields[i].SetValue(data, float.Parse(json[fields[i].Name]["S"].ToString()));
                    else
                        fields[i].SetValue(data, float.Parse(json[fields[i].Name]["N"].ToString()));
                }
                else
                    fields[i].SetValue(data, 0.0f);
            }
            else if (fields[i].FieldType == typeof(bool))
            {
                if (json.Keys.Contains(fields[i].Name))
                {
                    if (isChart)
                        fields[i].SetValue(data, bool.Parse(json[fields[i].Name]["S"].ToString()));
                    else
                        fields[i].SetValue(data, bool.Parse(json[fields[i].Name]["BOOL"].ToString()));
                }
                else
                    fields[i].SetValue(data, false);
            }
            else if (fields[i].FieldType == typeof(DateTime))
            {
                if (json.Keys.Contains(fields[i].Name))
                {
                    string time = json[fields[i].Name]["S"].ToString();
                    fields[i].SetValue(data, DateTime.Parse(time));
                }
                else
                    fields[i].SetValue(data, new DateTime(0));
            }
            else
                Debug.LogError("Wrong field type");
        }
        return data;
    }




}