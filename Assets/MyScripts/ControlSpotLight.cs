using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSpotLight : MonoBehaviour
{
    private bool playing;
    private GameObject lightObj;
    private GameObject speedObj;
    private Light light;
    private List<Color> colorArr;
    private int colorIndex;
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        playing = false;
        speedObj = GameObject.Find("SpotLightSpeedButton");
        lightObj = GameObject.Find("SpotLight");
        light = lightObj.GetComponent<Light>();
        colorArr = new List<Color>();
        colorArr.Add(Color.white);
        colorArr.Add(Color.cyan);
        colorArr.Add(Color.green);
        colorArr.Add(Color.blue);
        colorArr.Add(Color.magenta);
        colorArr.Add(Color.yellow);
        colorArr.Add(Color.red);
        colorIndex = 0;
        speed = 0.5f;
    }

    //Function that calls an invokeRepeating to change the lights or stops it
    public void changePlaying()
    {
        if (!playing)
        {
            InvokeRepeating("changeSpotLightColor", 0f, speed);
            playing = true;
        }
        else
        {
            CancelInvoke();
            playing = false;
        }

    }

    //Function that changes the color of the spotlight
    void changeSpotLightColor()
    {
        light.color = colorArr[colorIndex];
        colorIndex = (colorIndex + 1) % colorArr.Count;

    }

    //Function that calculates what the speed of the spotlight changing colors should be based on the x cordinate of the speed button
    public void changeSpeed()
    {
        float normal = Mathf.InverseLerp(1.25f, 1.5f, speedObj.transform.position.x);
        speed = Mathf.Lerp(0.1f, 1.5f, normal);
        //speed = 1.61f - speedObj.transform.position.x;
        print(speed);
        if (playing)
        {
            //If it's already playing, cancel the current invoke, and call another one with the updated speed
            CancelInvoke();
            InvokeRepeating("changeSpotLightColor", 0f, speed);
        }
    }

    //Function that repositions the object so that the local Y and local Z is constant
    public void restrictMovement()
    {
        //make sure that local Y = 0 and local Z = 1.35
        //Can't get local to work so I'm using y = 0.194 and Z = -0.1225
        if(speedObj.transform.position.x >= 1.5)
            speedObj.transform.position = new Vector3(1.5f, 0.914f, -0.1225f);

        else if(speedObj.transform.position.x <= 1.25)
            speedObj.transform.position = new Vector3(1.25f, 0.914f, -0.1225f);

        //Stop any momentum
        speedObj.GetComponent<Rigidbody>().velocity = Vector3.zero;
        speedObj.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        //speedObj.GetComponent<Rigidbody>() = Vector3.zero;
        print(speedObj.GetComponent<Rigidbody>().velocity);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
