using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSphere : MonoBehaviour
{
    public Transform followPos;
    public Transform earthShatterSpot;
    public Camera cam;

    public GameObject bulletPrefab;
    public GameObject earthShatter;
    public GameObject collisionArea;
    //public Rigidbody rb;


    public float moveSpeed = 10;
    public float dampening = 1;
    public float bulletForce = 20;

    public float timeBetweenShot = 0.3f;
    public float lastShotTime = 0f;

    public float timeBetweenSpecialShot = 10f;
    public float lastSpecialShotTime = 0f;

    public float collAreaMoveSpeed = 2;

    Vector3 mousePos;
    Vector3 firePoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //follow
        Vector3 followVector = followPos.position - transform.position;
        float multiplier = followVector.magnitude;
        float step = moveSpeed * Time.deltaTime * (multiplier / dampening);
        transform.position = Vector3.MoveTowards(transform.position, followPos.position, step);

        //shooting
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray,out RaycastHit raycastHit))
        {
            firePoint = raycastHit.point;
        }

        if(Input.GetMouseButton(0))
        {
            if (Time.time - lastShotTime > timeBetweenShot)
            {
                lastShotTime = Time.time;

                firePoint.y = transform.position.y;
                Vector3 lookDir = firePoint - transform.position;
                Shoot(lookDir.normalized);
            }
        }

        if (Input.GetMouseButton(1))
        {
            if (Time.time - lastSpecialShotTime > timeBetweenSpecialShot)
            {
                lastSpecialShotTime = Time.time;

                firePoint.y = transform.position.y;
                Vector3 lookDir = firePoint - transform.position;
                ShootSpecial(lookDir);
            }
        }
    }

    void Shoot(Vector3 dir)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        Rigidbody rb=bullet.GetComponent<Rigidbody>();
        rb.AddForce(dir * bulletForce, ForceMode.Impulse);
    }

    void ShootSpecial(Vector3 dir)
    {

        float angle = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
        angle = -angle + 90;

        GameObject collArea = Instantiate(collisionArea, earthShatterSpot.position, Quaternion.identity * Quaternion.Euler(0.0f, angle, 0.0f));
        GameObject specialEffect = Instantiate(earthShatter, earthShatterSpot.position, Quaternion.identity * Quaternion.Euler(0.0f, angle, 0.0f));
        specialEffect.transform.parent = null;
        collArea.transform.parent = null;

        Rigidbody collAreaRb = collArea.GetComponent<Rigidbody>();
        collAreaRb.AddForce(dir * collAreaMoveSpeed, ForceMode.Impulse);

        Destroy(specialEffect, 4f);
        Destroy(collArea, 4f);
    }
}
