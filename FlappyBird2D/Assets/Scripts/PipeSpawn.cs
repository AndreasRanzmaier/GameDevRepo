using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawn : MonoBehaviour
{
    [SerializeField] private GameObject Pipe;
    private Rigidbody2D RigidbodyComponent;

    [SerializeField] private int speed;
    private GameObject[] arrPipes;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool enoughSpace = true;
        foreach(Transform child in transform)
        {
            if(child.transform.position.x > 4)
            {
                enoughSpace = false;
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
        var PipeNew = Instantiate(Pipe, new Vector3(10.63f, 0, 0), Quaternion.identity);
        PipeNew.transform.parent = gameObject.transform;

        RigidbodyComponent = PipeNew.GetComponent<Rigidbody2D>();
        RigidbodyComponent.AddForce(Vector2.left * speed, ForceMode2D.Force);
    }

    void OnBecameInvisible()
    {
        // Destroy(GameObject);
    }
}