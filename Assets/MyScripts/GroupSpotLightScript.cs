using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupSpotLightScript : MonoBehaviour
{
    private List<GameObject> spotLightObjList;  //First group 1-4, second group 5-8
    private List<Light> spotLightList;
    private List<Color> colorArr;
    private GameObject speedObj;
    private bool playing, vertical, on;
    private float speed;
    private string currentFunctionName; //will be used to know what the current Invoke has called
    // Start is called before the first frame update
    void Start()
    {
        playing = false;
        vertical = false;
        on = false;
        speed = 0.5f;
        currentFunctionName = "";
        spotLightObjList = new List<GameObject>();
        spotLightList = new List<Light>();
        colorArr = new List<Color>();
        for(int i = 1; i <= 8; ++i)
        {
            spotLightObjList.Add(GameObject.Find(string.Concat("Spot Light", i)));
            spotLightList.Add(spotLightObjList[i - 1].GetComponent<Light>());
            spotLightList[i - 1].enabled = false;
        }
        speedObj = GameObject.Find("SpotLightGroupSpeedButton");
        colorArr.Add(Color.white);
        colorArr.Add(Color.cyan);
        colorArr.Add(Color.green);
        colorArr.Add(Color.blue);
        colorArr.Add(Color.magenta);
        colorArr.Add(Color.yellow);
        colorArr.Add(Color.red);
    }

    //Function that turns on the system and will call subroutines to create it's own light show randomly
    public void automatic()
    {
        playing = !playing;
        if (playing)
        {
            //Call a bunch of random functions in a calculated way :thinking: Basically not a pure chaotic randomness but not pseudo random either
            //InvokeRepeating("toggleAllLights", 0f, speed);
            //currentFunctionName = "toggleAllLights";
            if (!on)
                turnOn();
            InvokeRepeating("randomColors", 0f, speed);
            currentFunctionName = "randomColors";
        }
    }

    //Function that turns on or off the system
    public void turnOff()
    {
        CancelInvoke(); //Might need a try catch if there is nothing to cancel
        playing = false;
    }

    //Function that calcualtes the position of the object and maps it to a value for speed
    public void changeSpeed()
    {
        float normal = Mathf.InverseLerp(0.68f, 1.0f, speedObj.transform.position.x);
        speed = Mathf.Lerp(0.1f, 2.5f, normal);
        //Recall our current pattern with the new speed
        CancelInvoke();
        InvokeRepeating(currentFunctionName, 0f, speed);
    }

    //Function that repositions the object so that the local Y and local Z is constant
    public void restrictMovement()
    {
        //make sure that local Y = 0 and local Z = 1.35
        //Can't get local to work so I'm using y = 0.194 and Z = -0.1225
        if (speedObj.transform.position.x >= 1.0)
            speedObj.transform.position = new Vector3(1.0f, 0.819f, -0.071f);

        else if (speedObj.transform.position.x <= 0.68)
            speedObj.transform.position = new Vector3(0.68f, 0.819f, -0.071f);

        //Stop any momentum
        speedObj.GetComponent<Rigidbody>().velocity = Vector3.zero;
        speedObj.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        //speedObj.GetComponent<Rigidbody>() = Vector3.zero;
        print(speedObj.GetComponent<Rigidbody>().velocity);
    }

    //Assigns a random color to each individual light
    private void randomColors()
    {
        for(int i = 0; i < 8; ++i)
        {
            spotLightList[i].color = colorArr[Random.Range(0,8)];
        }
    }

    private void toggleAllLights()
    {
        for(int i = 0; i < 8; ++i)
        {
            spotLightList[i].enabled = !spotLightList[i].enabled;
        }

    } 

    //Function that will transition all the spotlights into vertical or horizontal
    private void transition()
    {
        if (vertical)
        {
            //Use a function to make the first group rotate to Y = 90 progressively

            //Use a function to make the second group rotate to X = 90 progressively
        }
        else
        {
            
        }
    }

    //Turn on all the lights
    private void turnOn()
    {
        for(int i = 0; i < 8; ++i)
        {
            spotLightList[i].enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
