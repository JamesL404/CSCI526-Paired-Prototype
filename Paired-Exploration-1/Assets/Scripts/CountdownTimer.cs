using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    private float currentTime = 0.0f;
    private float startingTime = 41.0f;

    [SerializeField] private TMP_Text countdownText;

    [SerializeField] private TMP_Text instructions;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            instructions.text = "";
        }
        currentTime -= 1 * Time.deltaTime;
        int roundTime = (int)currentTime;
        countdownText.text = roundTime.ToString();
    }
}