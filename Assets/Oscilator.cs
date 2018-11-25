﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscilator : MonoBehaviour {


    public double frequency = 440.0f;
    private double increment;
    private double phase;
    private double samplingFrequency = 48000.0;

    public float gain;
    public float volume = 0.1f;

    public float[] frequencies;
    public int thisFreq;

    private void Start()
    {
        frequencies = new float[8];
        frequencies[0] = 440;
        frequencies[1] = 494;
        frequencies[2] = 554;
        frequencies[3] = 587;
        frequencies[4] = 659;
        frequencies[5] = 740;
        frequencies[6] = 831;
        frequencies[7] = 880;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gain = volume;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            gain = 0.0f;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            thisFreq++;
            thisFreq %= frequencies.Length;
            frequency = frequencies[thisFreq];
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            thisFreq--;
            thisFreq = (thisFreq%frequencies.Length + frequencies.Length) % frequencies.Length;
            frequency = frequencies[thisFreq];
        }
    }

    private void OnAudioFilterRead(float[] data, int channels)
    {
        increment = frequency * 2.0 * Mathf.PI / samplingFrequency;

        for (int i = 0; i < data.Length; i += channels)
        {
            phase += increment;
            data[i] = (float)(gain * Mathf.Sin((float)phase));

            if(channels == 2)
            {
                data[i + 1] = data[i];
            }

            if(phase > Mathf.PI * 2)
            {
                phase = 0.0f;
            }
        }
    }

}
