using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    public float speed = 0.1f;
    private float rotAmount;
    // private Rigidbody rb;
    private Vector3 movement;
    private Vector3 rvec;
    // Start is called before the first frame update
    void Start()
    {
        // rb = GetComponent<Rigidbody>();
        rvec = new Vector3(0,1,0);
    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector3(0,0,-1);
    }

    void FixedUpdate()
    {
        moveCharacter(movement);
    }

    void moveCharacter(Vector3 direction)
    {
        // rb.MovePosition(transform.position + (direction * speed * Time.deltaTime));
        transform.Translate(direction * speed * Time.deltaTime);

    }

    void OnCollisionEnter(Collision col)
    {
        if(col.collider.name == "Quad4" || col.collider.name == "Quad1" || col.collider.name == "Quad2" || col.collider.name == "Quad3")
        {
            rotAmount = Random.Range(90, 270);
            transform.Rotate(rvec, rotAmount);
        }

        if(col.collider.name == "Player")
        {
            SceneManager.LoadScene("SampleScene");
            Debug.Log("player");
        }
    } 

    void OnCollisionStay(Collision col)
    {
        if(col.collider.name == "Quad4" || col.collider.name == "Quad1" || col.collider.name == "Quad2" || col.collider.name == "Quad3")
        {
            rotAmount = Random.Range(90, 270);
            transform.Rotate(rvec, rotAmount);
        }
    } 

    private Vector3 RandomVector(float min, float max) {
        var x = Random.Range(min, max);
        var y = Random.Range(min, max);
        var z = Random.Range(min, max);
        return new Vector3(x, y, z);
    }
}
