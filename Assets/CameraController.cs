using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{   Transform Player; 
    public Vector3 Offset; 
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player").transform;     
    }

    // Update is called once per frame
    void Update()
    {
       transform.position =  Player.position - Offset;
    }
}
