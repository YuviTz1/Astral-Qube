using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowHighScore : MonoBehaviour
{
    //public Text highscore;
    public TextMeshProUGUI highscore;

    private void Update()
    {
        highscore.text = PlayerPrefs.GetInt("HighScore").ToString();
    }
}
