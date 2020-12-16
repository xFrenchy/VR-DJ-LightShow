using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class i_oLightScript : MonoBehaviour
{
    private GameObject speedObj, brightnessObj, playObj;
    private Light light;
    private bool on;
    private float brightness, speed;

    // Start is called before the first frame update
    void Start()
    {
        speedObj = GameObject.Find("i_oSpeedButton");
        brightnessObj = GameObject.Find("i_oBrightnessButton");
        playObj = GameObject.Find("i_oButton");
        light = GameObject.Find("NameLight").GetComponent<Light>();
        on = false;
        brightness = 0;
        speed = 0;
    }

    //Function that toggles the on/off value, if toggled to on, call a function to flash the light with a specific brightness at a specific speed
    public void toggle()
    {
        on = !on;
        if (on)
        {
            InvokeRepeating("flash", 0f, speed);
        }
        else
        {
            CancelInvoke();
        }
    }

    //Function to change the speed
    public void changeSpeed()
    {
        float normal = Mathf.InverseLerp(0.96f, 0.7f, speedObj.transform.localPosition.x);
        speed = Mathf.Lerp(1.75f, 0.1f, normal);
        if (on)
        {
            CancelInvoke();
            InvokeRepeating("flash", 0f, speed);
        }
    }

    //Function to change the brightness
    public void changeBrightness()
    {
        float normal = Mathf.InverseLerp(-1.08f, -1.30f, brightnessObj.transform.localPosition.z);
        brightness = Mathf.Lerp(0f, 5000f, normal);
        light.intensity = brightness;
    }

    //Function that restricts movement only allowing change in the X direction
    public void restrictMovementX()
    {
        //make sure that local Y = 0.817 and local Z = -1.0445
        if (speedObj.transform.localPosition.x >= 0.96f)
            speedObj.transform.localPosition = new Vector3(0.96f, 0.817f, -1.0445f);

        else if (speedObj.transform.localPosition.x <= 0.7)
            speedObj.transform.localPosition = new Vector3(0.7f, 0.817f, -1.0445f);

        //Stop any momentum
        speedObj.GetComponent<Rigidbody>().velocity = Vector3.zero;
        speedObj.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }


    //Function that restricts movment only allowing change in the Z direction
    public void restrictMovementZ()
    {
        //make sure that local Y = 0.8226 and local X = 0.619
        if (brightnessObj.transform.localPosition.z >= -1.08f)
            brightnessObj.transform.localPosition = new Vector3(0.619f, 0.8226f, -1.08f);

        else if (brightnessObj.transform.localPosition.z <= -1.30f)
            brightnessObj.transform.localPosition = new Vector3(0.619f, 0.8226f, -1.30f);

        //Stop any momentum
        brightnessObj.GetComponent<Rigidbody>().velocity = Vector3.zero;
        brightnessObj.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }


    //Flashes the light on the i_o structure
    private void flash()
    {
        light.enabled = !light.enabled;
    }


    // Update is called once per frame
    void Update()
    {
    
    }
}
