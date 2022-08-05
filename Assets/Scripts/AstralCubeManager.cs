using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class AstralCubeManager : MonoBehaviour
{
    float xRot, yRot, zRot;
    Vector3 rotateAmount;
    public static int health=100;

    public TextMeshProUGUI healthVal;

    // Start is called before the first frame update
    void Start()
    {
        xRot = Random.Range(0f, 90f);
        zRot = Random.Range(0f, 90f);
        yRot = Random.Range(0f, 90f);

        rotateAmount = new Vector3(xRot, yRot, zRot);
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotateAmount * Time.deltaTime);

        if(health<=0)
        {
            //explosion!!+gameover
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        healthVal.text = health.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Enemy")
        {
            health -= 10;
        }
        else if(other.gameObject.tag=="enemyBullet")
        {
            health -= 5;
        }
    }
}