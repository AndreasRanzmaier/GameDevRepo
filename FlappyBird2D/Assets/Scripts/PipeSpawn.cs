using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawn : MonoBehaviour
{
    [SerializeField] private GameObject Pipe;
    private Rigidbody2D RigidbodyComponent;
    [SerializeField] private int speed;

    // Update is called once per frame
    void FixedUpdate()
    {
        bool enoughSpace = true;
        foreach (Transform child in transform)
        {
            // check if we shall span another one
            if (child.transform.position.x > 4)
            {
                enoughSpace = false;
            }

            // delete if to far left
            if (child.transform.position.x < -11)
            {
                Destroy(child.gameObject);
            }
        }

        if (enoughSpace)
        {
            SpawnPipe();
        }
    }

    void SpawnPipe()
    {
        // spawning of Pipes
        // +-3.9 = max up/down 
        // 8.68 = max right
        // 10.63 = spawn right out of sight
        float sudoRandom = Random.Range(-1f, 1f);
        var PipeNew = Instantiate(Pipe, new Vector3(10.63f, sudoRandom, 0), Quaternion.identity);
        PipeNew.transform.parent = gameObject.transform;

        RigidbodyComponent = PipeNew.GetComponent<Rigidbody2D>();
        RigidbodyComponent.AddForce(Vector2.left * speed, ForceMode2D.Force);
    }
}