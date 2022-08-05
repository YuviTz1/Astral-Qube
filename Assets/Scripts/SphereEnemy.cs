using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereEnemy : MonoBehaviour
{
    public Transform sphere1;
    public Transform sphere2;
    public Transform sphere3;
    public Transform sphere4;
    public Transform sphere5;
    public Transform sphere6;

    public Transform target;
    public GameObject shattered;

    public GameObject managerObj;
    public GameManager manager;

    public float moveSpeed = 10f;
    public int health = 20;

    void Start()
    {
        target = GameObject.Find("AstralCube").transform;
        managerObj = GameObject.Find("GameManager");
        manager = managerObj.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        rotate_sphere1();
        rotate_sphere2();
        rotate_sphere3();
        rotate_sphere4();
        rotate_sphere5();
        rotate_sphere6();

        //update position of gameObject
        Vector3 followVector = target.position - transform.position;
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        if (health <= 0)
        {
            //explosion effect!!
            Destroy(gameObject);
            GameObject destroyed = Instantiate(shattered, transform.position, transform.rotation);
            Destroy(destroyed, 3f);

            manager.score += 100;

            if (PlayerPrefs.GetInt("HighScore") < manager.score)
            {
                PlayerPrefs.SetInt("HighScore", manager.score);
            }
        }

        if (followVector.magnitude <= 0.5f)
        {
            Destroy(gameObject);
            GameObject destroyed = Instantiate(shattered, transform.position, transform.rotation);
            Destroy(destroyed, 3f);

            AstralCubeManager.health -= 10;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bullet")
        {
            health -= 10;
        }
        else if(other.gameObject.tag == "specialAttack")
        {
            health -= 20;
        }
        else if (other.gameObject.tag == "AstralCube")
        {
            Destroy(gameObject);
            GameObject destroyed = Instantiate(shattered, transform.position, transform.rotation);
            Destroy(destroyed, 3f);
        }
    }

    void rotate_sphere1()
    {
        Vector3 rotation = new Vector3(Mathf.Cos(Time.time), 0, Mathf.Sin(Time.time));
        sphere1.position = transform.position + rotation;
    }

    void rotate_sphere2()
    {
        Vector3 rotation = new Vector3(0, Mathf.Sin(Time.time), Mathf.Cos(Time.time));
        sphere2.position = transform.position + rotation;
    }

    void rotate_sphere3()
    {
        Vector3 rotation = new Vector3(Mathf.Cos(Time.time + 3.14f), 0, Mathf.Sin(Time.time + 3.14f));
        sphere3.position = transform.position + rotation;
    }

    void rotate_sphere4()
    {
        Vector3 rotation = new Vector3(0, Mathf.Sin(Time.time + 3.14f), Mathf.Cos(Time.time + 3.14f));
        sphere4.position = transform.position + rotation;
    }

    void rotate_sphere5()
    {
        Vector3 rotation = new Vector3(Mathf.Cos(Time.time - 1.585f), 0, Mathf.Sin(Time.time - 1.585f));
        sphere5.position = transform.position + rotation;
    }

    void rotate_sphere6()
    {
        Vector3 rotation = new Vector3(Mathf.Cos(Time.time + 1.585f), 0, Mathf.Sin(Time.time + 1.585f));
        sphere6.position = transform.position + rotation;
    }
}
