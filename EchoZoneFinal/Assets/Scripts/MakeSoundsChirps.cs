using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeSoundsChirps : MonoBehaviour {
    private float radian = 0; 
    private float perRadian = 0.08f;
    private float radius = 0.1f; 
    Vector3 oldPos;
    public bool isTrigger;
    public AudioSource Chirps;


    // Use this for initialization
    void Start () {
        oldPos = transform.position;
        isTrigger = true;
        Chirps.Play(0);
    }
	
	// Update is called once per frame
	void Update () {
        if(isTrigger){
            radian += perRadian;
            float dy = Mathf.Cos(radian) * radius;
            transform.position = oldPos + new Vector3(0, dy, 0);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTrigger = false;
            Chirps.Pause();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTrigger = true;
            Chirps.UnPause();
        }
    }
}
