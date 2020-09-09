using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CountDownTimer : MonoBehaviour

{

    float currentTime = 0f;
    float startTime = 15f;

    public Text CountDownText;

    void Start()

    {

        currentTime = startTime;

    }



    // Update is called once per frame

    void Update()

    {

        if(currentTime>1)

        {

        currentTime -= 1 * Time.deltaTime;

        print (currentTime);

        CountDownText.text = currentTime.ToString ("0");

        }
        else
        {
             CountDownText.text = "OOPS!";
        }

        if(currentTime<5.5f){CountDownText.color = Color.red;}

        

    }

}