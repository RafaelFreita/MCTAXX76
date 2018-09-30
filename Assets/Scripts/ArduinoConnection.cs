using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;

using UnityEngine;

public class ArduinoConnection : MonoBehaviour
{

    private enum MessageTypes
    {
        btn1,
        btn2,
        btn3,
        btn4,
        switch1,
        pot1,
        pot2
    }

    private struct Message
    {
        public MessageTypes key;
        public int value;
    }

    private SerialPort sp = new SerialPort("COM3", 9600);

    public AudioSource[] audioSources = new AudioSource[4];

    void Start()
    {
        sp.Open();
        sp.ReadTimeout = 1;
    }

    void Update()
    {

        if (sp.IsOpen)
        {
            try
            {
                //Debug.Log(sp.ReadByte());
                TreatMessage(GetNextMessage());
            }
            catch (System.Exception)
            {
                //throw;
            }
        }

    }

    private Message GetNextMessage()
    {
        return new Message { key = (MessageTypes)sp.ReadByte(), value = sp.ReadByte() };
    }

    private void TreatMessage(Message message)
    {
        switch (message.key)
        {
            case MessageTypes.btn1:
                //Debug.Log("Btn1: " + message.value);
                PlayAudioSource(0, message.value);
                break;
            case MessageTypes.btn2:
                //Debug.Log("Btn2: " + message.value);
                PlayAudioSource(1, message.value);
                break;
            case MessageTypes.btn3:
                //Debug.Log("Btn3: " + message.value);
                PlayAudioSource(2, message.value);
                break;
            case MessageTypes.btn4:
                //Debug.Log("Btn4: " + message.value);
                PlayAudioSource(3, message.value);
                break;
            case MessageTypes.switch1:
                //Debug.Log("Switch: " + message.value);
                ToggleAudioSources(message.value);
                break;
            case MessageTypes.pot1:
                //Debug.Log("Pot1: " + message.value);
                ControlPitch(message.value);
                break;
            case MessageTypes.pot2:
                //Debug.Log("Pot2: " + message.value);
                ControllVolume(message.value);
                break;
        }
    }

    private void PlayAudioSource(int index, int value)
    {
        if (value == 0)
        {
            Debug.Log("Play audio"  + index);
            if (!audioSources[index].isPlaying)
            {
                audioSources[index].Play();
            }
        }
        else
        {
            audioSources[index].Stop();
        }
    }

    private void ControlPitch(int value)
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].pitch = 1 + value / 255f;
        }
    }

    private void ToggleAudioSources(int value)
    {
        bool toggle = false;
        if (value == 0)
        {
            toggle = true;
        }
        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].enabled = toggle;
        }
    }

    private void ControllVolume(int value)
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].volume = value/255f;
        }
    }

}
