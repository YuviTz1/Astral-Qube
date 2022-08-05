using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class humanoidEnemy : MonoBehaviour
{
    public Transform player;
    public Transform astralCube;
    private Transform target;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Animator animator;
    public GameObject shattered;

    public GameObject managerObj;
    public GameManager manager;

    public float moveSpeed = 10f;
    public int health = 30;
    public float minDist = 5f;
    public float bulletForce = 10f;
    public float hitDist = 10f;

    public float timeBetweenShot = 7f;
    public float lastShotTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        astralCube = GameObject.Find("targetPointAstralCube").transform;
        player = GameObject.Find("targetPoint").transform;

        managerObj = GameObject.Find("GameManager");
        manager = managerObj.GetComponent<GameManager>();

        float p = Random.Range(0.0f, 1.0f);
        if(p<=0.5)
        {
            target = astralCube;
        }
        else
        {
            target = player;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 followVector = target.position - transform.position;
        //followVector.y = 0.518f;
        if (followVector.magnitude <= minDist)
        {
            //stop
            animator.SetBool("walking", false);
        }
        else
        {
            //move animation
            animator.SetBool("walking", true);
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }

        //shoot
        if(Time.time-lastShotTime>=timeBetweenShot)
        {
            lastShotTime = Time.time;

            if (followVector.magnitude <= hitDist)
            {
                shoot(followVector.normalized);
            }
        }
        transform.LookAt(target);

        if(health<=0)
        {
            Destroy(gameObject);
            GameObject destroyed = Instantiate(shattered, transform.position, transform.rotation);
            Destroy(destroyed, 3f);

            manager.score += 100;

            if (PlayerPrefs.GetInt("HighScore") < manager.score)
            {
                PlayerPrefs.SetInt("HighScore", manager.score);
            }
        }
    }

    void shoot(Vector3 dir)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(dir * bulletForce, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="bullet")
        {
            health -= 10;
        }
        else if (other.gameObject.tag == "specialAttack")
        {
            health -= 30;
        }
    }
}
