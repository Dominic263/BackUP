
/******************************************************************************
*  This module contains code necessary to instantiate an MQTT Client on the   *
*  Unity end. The M2MQtt client library adapted from Paho helps with this a   *
*  adaptation.                                                                *
*  Last Day Update : December 2 2021                                          *
*  Author          : Ndondo Dominic Tinashe                                   *
******************************************************************************/
using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using M2MqttUnity;

public class MQTT : M2MqttUnityClient
{
    /* Topics to publish from and subscribe to */
    public string [] publish_topics    = {"pump"};
    public string [] subscribe_topics  = {"sawyer"};
    public TMP_Text panel;
    private bool test                  = true;

    /* Private List of event messages */
    private List<string> eventMessages = new List<string>();

    /*Client Instance is already initialized in the base class*/

    /*
    *  Function: publish() sends a string message from the client to the broker
    *  Input   : input string message and input string topic on broker 
    *  Output  : no output (void)
    */
    public void publish(string topic, string message)
    {
        client.Publish(topic, System.Text.Encoding.UTF8.GetBytes(message), 
                               MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        Debug.Log(message + " published to topic:" + topic + "\n");
        panel.text = message + " published to topic:" + topic + "\n";
    }

    /* Override function calls base function on connecting to the broker */
    protected override void OnConnecting()
    {
        base.OnConnecting();
        Debug.Log("Connecting to broker on " + brokerAddress + ":" 
                                          + brokerPort + "...\n");
    }

    /* Override function calls base function once connected to broker */
    protected override void OnConnected()
    {
        base.OnConnected();
        Debug.Log("Connected to broker on " + brokerAddress + "\n");
        panel.text = "Connected to broker on " + brokerAddress + "\n";
    }

    /*
    * Function : SubcribeTopics() override function subscribes to topics 
    * Input    : list of topics to subscribe to 
    * Output   : no output (void)
    */
    protected override void SubscribeTopics()
    {
        client.Subscribe(subscribe_topics, new byte[] 
             { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
    }

    /*
    * Function : UnSubcribeTopics() override function unsubscribes from topics 
    * Input    : list of topics to unsubscribe from 
    * Output   : no output (void)
    */
    protected override void UnsubscribeTopics()
    {
        for(int i = 0; i < subscribe_topics.Length; i++)
        {
            client.Unsubscribe(new string [] {subscribe_topics[i]});
        }
        
    }

    /* Message to display when connection fails */
    protected override void OnConnectionFailed(string error_message)
    {
        Debug.Log("Connection Failed! " + error_message + "\n");
        panel.text = "Connection Failed! " + error_message + "\n";
    }

    /* Message to display on disconnected */
    protected override void OnDisconnected()
    {
        Debug.Log("Disconnected.\n");
        panel.text = "Disconnected.\n";
    }

    /* Message displayed on connection lost event */
    protected override void OnConnectionLost()
    {
        Debug.Log("Connection Lost! \n");
        panel.text = "Connection Lost! \n";
    }
   
    /*
    * Function : DecodeMessage() parses out the message received 
    * Input    : Byte of message and the corresponding topic
    * Output   : none
    */
    protected override void DecodeMessage(string topic, byte[] message)
    {
        /* Get string message from the byte array */
        string msg = System.Text.Encoding.UTF8.GetString(message);
        Debug.Log("Received: " + msg + "\n");
        panel.text = "Received: " + msg + "\n";

        StoreMessage(msg);

        if (topic == subscribe_topics[0])
        {
            /*Deal with the msg*/
        }
    }

    /* Store the event message */
    private void StoreMessage(string event_msg)
    {
        eventMessages.Add(event_msg);
    }

    /* Process the message received */
    private void ProcessMessage(string msg)
    {
        Debug.Log("Received: " + msg + "\n");
    }

    /* Destroy client */
    private void OnDestroy()
    {
        Disconnect();
    }

    /* START : Called before the first frame */
    protected override void Start()
    {

        Debug.Log("Ready to Connect. \n");
        base.Start();
        panel.text = "Ready to Connect.\n";
    }

    public void Pump(){
        publish(publish_topics[0], "Test Button Pressed.");
    }
    
    /* UPDATE: Called once per frame */
    protected override void Update()
    {
        
        string test_message = "Works!";
        
        if(client.IsConnected){
            
            if(test == true){
                publish(publish_topics[0], test_message);
                test = false;
            }
            
            base.Update(); // call ProcessMqttEvents()
            if (eventMessages.Count > 0)
            {
                foreach (string msg in eventMessages)
                {
                    ProcessMessage(msg);
                }
                eventMessages.Clear();
            }
        }
    }
}
