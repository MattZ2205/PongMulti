using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text pointP1Txt;
    [SerializeField] TMP_Text pointP2Txt;

    public static GameManager Instance;

    int pointP1 = 0;
    int pointP2 = 0;

    private void Awake()
    {
        if (Instance != null) Destroy(this);
        else Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    //private void Update()
    //{
    //    Debug.Log("Points Host: " + pointP1 + " points P2: " + pointP2);
    //}

    void ResetPoints()
    {
        pointP1 = 0;
        pointP2 = 0;
    }

    public void PointHost()
    {
        pointP1++;
        pointP1Txt.text = pointP1.ToString();
    }

    public void PointP2()
    {
        pointP2++;
        pointP2Txt.text = pointP2.ToString();
    }
}
