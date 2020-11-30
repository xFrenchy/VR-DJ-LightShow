using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupSpotLightScript : MonoBehaviour
{
    private List<GameObject> spotLightObjList;  //First group 1-4, second group 5-8
    private List<Light> spotLightList;
    private List<Color> colorArr;
    private GameObject speedObj;
    private bool playing, on, even;
    private bool flashLights, evenOdd, randColors, sameColors;
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        playing = on = even = false;
        flashLights = evenOdd = randColors = sameColors = false;
        speed = 0.5f;
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
    public void play()
    {
        if (playing)
        {
            if (!on)
                turnOn();
            CancelInvoke(); //Might need a try catch if there is nothing to cancel

            if (flashLights)
            {
                resetLights();
                InvokeRepeating("flashAllLights", 0f, speed);
            }
            else if (evenOdd)
            {
                resetLights();
                InvokeRepeating("evenOddLights", 0f, speed);
            }

            if (randColors)
            {
                InvokeRepeating("randomColors", 0f, speed);
            }
            else if (sameColors)
            {
                InvokeRepeating("singularColor", 0f, speed);
            }
        }
    }

    //Function that turns off the system
    public void turnOff()
    {
        CancelInvoke(); //Might need a try catch if there is nothing to cancel
        for (int i = 0; i < 8; ++i)
        {
            spotLightList[i].enabled = false;
        }
        playing = false;
        on = false;
    }

    //Turn on the lights and sets them all to white, their original color.
    public void turnOn()
    {
        for (int i = 0; i < 8; ++i)
        {
            spotLightList[i].enabled = true;
            spotLightList[i].color = Color.white;
        }
        on = true;
        playing = true;
    }

    //Function that calcualtes the position of the object and maps it to a value for speed
    public void changeSpeed()
    {
        float normal = Mathf.InverseLerp(0.68f, 1.0f, speedObj.transform.position.x);
        speed = Mathf.Lerp(0.1f, 1.33f, normal);
        //Recall our current pattern with the new speed
        play();
    }

    //Function that repositions the object so that the local Y and local Z is constant
    public void restrictMovement()
    {
        //make sure that local Y = 0 and local Z = 1.35
        //Can't get local to work so I'm using y = 0.194 and Z = -0.1225
        if (speedObj.transform.position.x >= 1.0)
            speedObj.transform.localPosition= new Vector3(1.0f, 0.819f, -0.071f);

        else if (speedObj.transform.position.x <= 0.68)
            speedObj.transform.localPosition = new Vector3(0.68f, 0.819f, -0.071f);

        //Stop any momentum
        speedObj.GetComponent<Rigidbody>().velocity = Vector3.zero;
        speedObj.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        //speedObj.GetComponent<Rigidbody>() = Vector3.zero;
        print(speedObj.GetComponent<Rigidbody>().velocity);
    }

    //Function that sets the flag for the flashing lights pattern to play. Only one light pattern can be set at a time. FlashLight corresponds with flashAllLights
    public void flashLightsButton()
    {
        flashLights = true;
        evenOdd = false;
    }

    //Function that sets the flag for even odd flashing to play. Only one light pattern can be set at a time. Even odd corresponds with evenOddLights
    public void evenOddLightsButton()
    {
        evenOdd = true;
        flashLights = false;
    }

    //Function that sets the flag for random colors to play. and applies instantly. Only one color pattern can be set at a time. Corresponds with randomcolors
    public void randomColorsButton()
    {
        randColors = true;
        sameColors = false;
        randomColors();
    }

    //Function that sets the flag for same colors to play and applies instantly. Only one color pattern can be set at a time. Corresponds with singularColor
    public void sameColorsButton()
    {
        sameColors = true;
        randColors = false;
        singularColor();
    }







    //Assigns a random color to each individual light
    private void randomColors()
    {
        for(int i = 0; i < 8; ++i)
        {
            spotLightList[i].color = colorArr[Random.Range(0,8)];
        }
    }

    //Assigns the same singular color to each individual light
    private void singularColor()
    {
        int color = Random.Range(0, 8);
        for (int i = 0; i < 8; ++i)
        {
            spotLightList[i].color = colorArr[color];
        }
    }

    //Turns off or on the lights. Call this function multiple times to simulate a light turning on and off repeatidly
    private void flashAllLights()
    {
        for(int i = 0; i < 8; ++i)
        {
            spotLightList[i].enabled = !spotLightList[i].enabled;
        }
    } 

    //Function that will turn on or off the lights in an even/odd pattern. Call this function multiple times to simulate it turning on or off repeatidly
    private void evenOddLights()
    {
        for (int i = 0; i < 8; ++i)
        {
            if(even && i%2 == 0)
                spotLightList[i].enabled = true;
            else if(even && i%2 == 1)
                spotLightList[i].enabled = false;

            if(!even && i%2 ==1)
                spotLightList[i].enabled = true;
            else if(!even && i%2 == 0)
                spotLightList[i].enabled = false;
        }
        even = !even;
    }

    //Function that is used to reset all lights to on, different then turn on because we don't mess with the colors of the light or any other boolean vars
    private void resetLights()
    {
        for (int i = 0; i < 8; ++i)
        {
            spotLightList[i].enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
