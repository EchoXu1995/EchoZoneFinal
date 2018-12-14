using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

[RequireComponent (typeof(AudioSource))]
public class SoundShoot : MonoBehaviour {

    //Drag in the Bullet Emitter from the Component Inspector.
    public GameObject SoundBullet_Emitter;
    private GameObject Temporary_Bullet_Handler;
    //Drag in the Bullet Prefab from the Component Inspector.
    public GameObject SoundBullet;

    //Enter the Speed of the Bullet from the Component Inspector.
    public float SoundBullet_Forward_Force;

    private float timer = 0;

    //Microphone Input!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    public float sensitivity = 100;
    public float loudness = 0;
    AudioSource _audioSource;
    public AudioMixerGroup _mixerGroupMicrophone;

    public bool oneWaveSwitch = false;
    public bool debunk2 = false;


    // Use this for initialization
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        string[] micArray = Microphone.devices;
        oneWaveSwitch = false;
        if (micArray.Length != 0)   //List is not empty
        {
            _audioSource.outputAudioMixerGroup = _mixerGroupMicrophone;
            _audioSource.clip = Microphone.Start(micArray[0], true, 1, 44100);
            GetComponent<AudioSource>().loop = true; // Set the AudioClip to loop
            //GetComponent<AudioSource>().mute = true; // Mute the sound, we don't want the player to hear it
            if (_audioSource.clip != null)
            {
                while (!(Microphone.GetPosition(null) > 0)) { }
                _audioSource.Play();
                Debug.Log("Microphone connected!");
            }
            else
            {
                Debug.Log("No Microphone Input.");//Fail to use microphone
            }
        }
        else
        {
            Debug.Log("No Device List.");//Fail to get the list
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Get Microphone Data
        loudness = GetAveragedVolume() * sensitivity;
        Debug.Log(GetAveragedVolume());
        Debug.Log(loudness);
        //Debug.Log(oneWaveSwitch);

        //Shoot when making sounds
        if (!oneWaveSwitch)
        {
            debunk2 = true;
            if (loudness > 5 && debunk2 == true)

            {
                //The Bullet instantiation happens here.
                Temporary_Bullet_Handler = Instantiate(SoundBullet, SoundBullet_Emitter.transform.position, SoundBullet_Emitter.transform.rotation) as GameObject;
                Debug.Log("boom!");
                debunk2 = false;
                oneWaveSwitch = true;
                //oneWaveSwitch = false;
            }
        }

        else if(oneWaveSwitch){
            timer += Time.deltaTime;
            if (timer > 0.8f)
            {
                oneWaveSwitch = false;
                timer = 0;
            }
        }
                //GameObject Temporary_Bullet_Handler;
                //Temporary_Bullet_Handler = Instantiate(SoundBullet, SoundBullet_Emitter.transform.position, SoundBullet_Emitter.transform.rotation) as GameObject;

                //Sometimes bullets may appear rotated incorrectly due to the way its pivot was set from the original modeling package.
                //This is EASILY corrected here, you might have to rotate it from a different axis and or angle based on your particular mesh.
                Temporary_Bullet_Handler.transform.Rotate(Vector3.left * 90);

                //Retrieve the Rigidbody component from the instantiated Bullet and control it.
                Rigidbody Temporary_RigidBody;
                Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();

                //Tell the bullet to be "pushed" forward by an amount set by Bullet_Forward_Force.
                Temporary_RigidBody.AddForce(transform.forward * SoundBullet_Forward_Force);

                //Basic Clean Up, set the Bullets to self destruct after 10 Seconds, I am being VERY generous here, normally 3 seconds is plenty.
                Destroy(Temporary_Bullet_Handler, 1f);




    }

     float GetAveragedVolume()
     { 
         float[] data = new float[256];
         float a = 0;
         GetComponent<AudioSource>().GetOutputData(data,0);
         foreach(float s in data)
         {
             a += Mathf.Abs(s);
         }
         return a/256;
     }

}
