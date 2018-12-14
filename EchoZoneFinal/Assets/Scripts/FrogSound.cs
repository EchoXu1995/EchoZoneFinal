using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogSound : MonoBehaviour {
    private float radian = 0;
    private float perRadian = 0.7f;
    private float radius = 0.7f;
    private Vector3 oldPos;
    // Use this for initialization
    void Start () {
        oldPos = transform.position;
    
}
	
	// Update is called once per frame
	void Update () {
        radian += perRadian;
        float dy = Mathf.Cos(radian) * radius;
        transform.position = oldPos + new Vector3(0, dy, 0);
    }
}
