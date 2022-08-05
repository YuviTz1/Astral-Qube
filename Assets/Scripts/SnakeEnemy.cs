using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeEnemy : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 10;
    public float turnSpeed = 45f;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(target);
        Vector3 newDir = Vector3.RotateTowards(transform.position, target.position, 0.1f, 0.5f);
        //transform.rotation = Quaternion.LookRotation(newDir);
        Quaternion targetRotation = Quaternion.LookRotation(newDir);
        targetRotation *= Quaternion.Euler(0, 90, 0);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed*Time.deltaTime);

        Vector3 followVector = target.position - transform.position;
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime); 
    }
}
