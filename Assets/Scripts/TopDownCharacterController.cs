using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TopDownCharacterController : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public Animator animator;
    public Transform astralCube;

    public TextMeshProUGUI healthVal;

    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public float health = 100f;
    public float healDist = 3f;

    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreLayerCollision(6, 10);
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        //movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        if(direction.magnitude>0)
        {
            animator.SetBool("running",true);
        }
        else
        {
            animator.SetBool("running", false);
        }

        if((astralCube.position-transform.position).magnitude<healDist)
        {
            if (health < 100)
            {
                health += 0.1f;
            }
        }
        healthVal.text = Mathf.Floor(health).ToString();

        if(health<=0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="enemyBullet")
        {
            health -= 20;
        }
    }
}
