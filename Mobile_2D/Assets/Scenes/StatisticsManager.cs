/*****
*
* This Module keeps track of the real time statistics of the automated farm
* If the threshhold levels are not being met, then notifications are sent to 
* tell the farmer to respond to the changes in the  farm. 
*
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsManager : MonoBehaviour
{


    /* Constants acting as Thresholds */
    private const int TEMPERATURE_THRESHOLD  = 25; //Temperature is in degrees C
    private const int MOISTURE_THRESHOLD     = 500; // The soil is dry here 

    public int temperature; 
    public int moisture; 



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /* Handle the temperature Threshold Levels. */
        if(temperature <= TEMPERATURE_THRESHOLD){
            Debug.Log("The temperature is low.Turn on a heater\n");
        }else if(temperature >= TEMPERATURE_THRESHOLD){
            Debug.Log("The temperature is too high. Chill the farm a little.\n");
        }

        /* Handle the moisture Threshold Levels. */
        if(moisture <= MOISTURE_THRESHOLD){
            Debug.Log("The moisture is low.Turn on a heater\n");
        }else if(moisture >= MOISTURE_THRESHOLD){
            Debug.Log("The moisture is too high. Chill the farm a little.\n");
        }

    }


    /* Controlling the water pump */


}
