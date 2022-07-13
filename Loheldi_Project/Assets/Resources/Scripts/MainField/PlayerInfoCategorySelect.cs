using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoCategorySelect : MonoBehaviour
{
    public GameObject ExercisePanel;
    public GameObject FoodPanel;
    public GameObject MindPanel;
    public GameObject HomePanel;


    void Start()
    {
        initPanel();
    }

    void initPanel()
    {
        ExercisePanel.SetActive(true);
        FoodPanel.SetActive(false);
        MindPanel.SetActive(false);
        HomePanel.SetActive(false);
    }

    public void PopExercise()
    {
        ExercisePanel.SetActive(true);
        FoodPanel.SetActive(false);
        MindPanel.SetActive(false);
        HomePanel.SetActive(false);
    }

    public void PopFood()
    {
        ExercisePanel.SetActive(false);
        FoodPanel.SetActive(true);
        MindPanel.SetActive(false);
        HomePanel.SetActive(false);
    }

    public void PopMind()
    {
        ExercisePanel.SetActive(false);
        FoodPanel.SetActive(false);
        MindPanel.SetActive(true);
        HomePanel.SetActive(false);
    }

    public void PopHome()
    {
        ExercisePanel.SetActive(false);
        FoodPanel.SetActive(false);
        MindPanel.SetActive(false);
        HomePanel.SetActive(true);
    }
}
