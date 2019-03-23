using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscilator : MonoBehaviour
{

	[SerializeField] Vector3 movementVector;
    [SerializeField] float period;
	private Vector3 startingPosition;
    float movementFactor;
	
	// Use this for initialization
	void Start ()
	{
		startingPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
        if(period <= float.Epsilon)
        {
            return;
        }
        var tau = Mathf.PI * 2f;
        float cycles = Time.time / period;
        var rawSin = Mathf.Sin(cycles * tau);
        movementFactor = rawSin / 2f + 0.5f;
        var offset = movementFactor * movementVector;
        transform.position = startingPosition + offset;
	}
    
}
