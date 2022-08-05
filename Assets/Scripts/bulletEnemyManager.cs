using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletEnemyManager : MonoBehaviour
{
    public GameObject hitEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < -80 || transform.position.z > 80)
        {
            Destroy(gameObject);
        }
        else if (transform.position.x < -80 || transform.position.x > 80)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Player")
        {
            Destroy(gameObject);
            GameObject effect = Instantiate(hitEffect, transform.position, transform.rotation);
            Destroy(effect, 0.2f);
        }
        else if(other.tag=="AstralCube")
        {
            Destroy(gameObject);
            GameObject effect = Instantiate(hitEffect, transform.position, transform.rotation);
            Destroy(effect, 0.2f);
        }
    }
}
