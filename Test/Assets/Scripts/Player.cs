using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour{
    public int upForce = 5;
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        // KeyDown only checking it once
        if (Input.GetKeyDown(KeyCode.Space)){
            //Debug.Log("Space");
            Vector3 V3upForce = new Vector3(0,upForce,0);
            GetComponent<Rigidbody>().AddForce(V3upForce, ForceMode.VelocityChange);
        }
    }
}
