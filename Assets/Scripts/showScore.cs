using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class showScore : MonoBehaviour
{
    public TextMeshProUGUI score;
    public GameObject managerObj;
    GameManager manager;

    private void Start()
    {
        manager = managerObj.GetComponent<GameManager>();
    }

    private void Update()
    {
        score.text = manager.score.ToString();
    }
}
