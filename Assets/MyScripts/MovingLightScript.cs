using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLightScript : MonoBehaviour
{
    private GameObject speedObj;
    private List<GameObject> movingSpotLightObjList;   //First group 1-4 second group 5-8
    private List<Light> movingLightList;
    private List<Color> colorArr;
    private bool vertical, horizontal, playing;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        speedObj = GameObject.Find("MovingLightSpeedButton");
        vertical = horizontal = playing = false;
        movingSpotLightObjList = new List<GameObject>();
        movingLightList = new List<Light>();
        colorArr = new List<Color>();
        colorArr.Add(Color.white);
        colorArr.Add(Color.cyan);
        colorArr.Add(Color.green);
        colorArr.Add(Color.blue);
        colorArr.Add(Color.magenta);
        colorArr.Add(Color.yellow);
        colorArr.Add(Color.red);
        for (int i = 1; i <= 8; ++i)
        {
            movingSpotLightObjList.Add(GameObject.Find(string.Concat("MovingLight", i)));
            movingLightList.Add(movingSpotLightObjList[i - 1].GetComponent<Light>());
            movingLightList[i - 1].enabled = false;
        }
        speed = 20f;
    }

    //Turn on the lights and sets them all to white, their original color.
    public void turnOn()
    {
        for (int i = 0; i < 8; ++i)
        {
            movingLightList[i].enabled = true;
            movingLightList[i].color = Color.white;
        }
    }

    //Function that turns off the system and resets playing to off.
    public void turnOff()
    {
        for (int i = 0; i < 8; ++i)
        {
            movingLightList[i].enabled = false;
        }
        playing = false;
    }

    //Function that calcualtes the position of the object and maps it to a value for speed
    public void changeSpeed()
    {
        float normal = Mathf.InverseLerp(-0.68f, -0.47f, speedObj.transform.position.z);
        speed = Mathf.Lerp(8f, 178f, normal);
    }

    //Function that repositions the object so that the local Y and local X is constant
    public void restrictMovement()
    {
        //make sure that local Y = 0.75 and local X = 0.323
        if (speedObj.transform.localPosition.z >= -0.47f)
            speedObj.transform.localPosition = new Vector3(0.323f, 0.75f, -0.47f);

        else if (speedObj.transform.localPosition.z <= -0.68)
            speedObj.transform.localPosition = new Vector3(0.323f, 0.75f, -0.68f);

        //Stop any momentum
        speedObj.GetComponent<Rigidbody>().velocity = Vector3.zero;
        speedObj.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    //Function that sets the flag for our horizontal/vertical spotlight group to move up and down
    public void upNDownButton()
    {
        vertical = true;
        horizontal = false;
        playing = true;
    }

    //Function that sets the flag for our horizontal/vertical spotlight group to move side to side
    public void sideToSideButton()
    {
        horizontal = true;
        vertical = false;
        playing = true;
    }

    //Assigns the same singular color to each individual light
    public void changeColorButton()
    {
        int color = Random.Range(0, 8);
        for (int i = 0; i < 8; ++i)
        {
            movingLightList[i].color = colorArr[color];
        }
    }

    //Assigns the a random color to each individual light
    public void randColorButton()
    {
        for (int i = 0; i < 8; ++i)
        {
            movingLightList[i].color = colorArr[Random.Range(0, 8)];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (vertical)
        {
            for (int i = 0; i < 8; ++i)
            {
                if (i < 4)
                {
                    movingLightList[i].transform.localRotation = Quaternion.Euler(90f + Mathf.PingPong(Time.time, 60/speed)*speed, 0, 0);
                }
                else //So the second group 4,6,7,8 needs to have their y set to 90 for this to work sooooo
                {
                    movingLightList[i].transform.localRotation = Quaternion.Euler(90f + Mathf.PingPong(Time.time, 60 / speed) * speed, 90, 0);
                }
            }
        }
        else if (horizontal)
        {
            for (int i = 0; i < 8; ++i)
            {
                //I think the sweet spot is 130 x and -30 to 30 y
                //But here's the funny thing, that's only for the first 4, the second group is 120 to 60 for y still at 130 for x
                //Yeah I guess there's nothing funny about that
                if (i < 4)
                {
                    movingLightList[i].transform.localRotation = Quaternion.Euler(130f, -30 + Mathf.PingPong(Time.time, 60 / speed) * speed, 0);
                }
                else
                {
                    movingLightList[i].transform.localRotation = Quaternion.Euler(130f, 60 + Mathf.PingPong(Time.time, 60 / speed) * speed, 0);
                }
            }
        }
    }
}
