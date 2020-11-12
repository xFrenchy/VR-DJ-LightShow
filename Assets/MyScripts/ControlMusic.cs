using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMusic : MonoBehaviour
{
    private bool playing;
    private GameObject songObj;
    private GameObject volumeObj;
    private AudioSource song;
    private float volume;   //0.00-1.00
    // Start is called before the first frame update
    void Start()
    {
        playing = false;
        songObj = GameObject.Find("MusicSet");
        volumeObj = GameObject.Find("MusicVolumeButton");
        song = songObj.GetComponent<AudioSource>();
        volume = 0.8f;
    }

    //Function that plays the song if it's not currently playing/paused, if it is playing, it will pause it
    public void changePlaying()
    {
        if (!playing)
        {
            song.Play();
            song.volume = volume;
            playing = true;
        }
        else
        {
            song.Pause();
            playing = false;
        }
    }

    //Based on the position of this object, we will map it to be the volume
    public void changeVolume()
    {
        float normal = Mathf.InverseLerp(1.25f, 1.5f, volumeObj.transform.position.x);
        volume = Mathf.Lerp(0.0f, 1.0f, normal);
        song.volume = volume;
    }

    //Function that repositions the object so that the local Y and local Z is constant
    public void restrictMovement()
    {
        //make sure that local Y = 0 and local Z = 1.35
        //Can't get local to work so I'm using y = 0.194 and Z = -0.1225
        if (volumeObj.transform.position.x >= 1.5)
            volumeObj.transform.position = new Vector3(1.5f, 0.914f, -0.818f);

        else if (volumeObj.transform.position.x <= 1.25)
            volumeObj.transform.position = new Vector3(1.25f, 0.914f, -0.818f);

        //Stop any momentum
        volumeObj.GetComponent<Rigidbody>().velocity = Vector3.zero;
        volumeObj.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
