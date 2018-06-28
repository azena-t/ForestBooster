using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscilator : MonoBehaviour
{

	[SerializeField] Vector3 movementVector;
	[SerializeField] Vector3 velocity;
	private Vector3 startingPosition;
	private Boolean movingForward = true;
	private Vector3 currentOffset;
	
	// Use this for initialization
	void Start ()
	{
		startingPosition = transform.position;
		currentOffset = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 offset = velocity * Time.deltaTime;
		if (!movingForward)
		{
			offset = -offset;
		}

		if (GreaterThan(currentOffset, movementVector))
		{
			movingForward = false;
		}

		if (LessThan(currentOffset, Vector3.zero))
		{
			movingForward = true;
		}

		currentOffset += offset;
		
		transform.position = startingPosition + currentOffset;
	}

	private bool GreaterThan(Vector3 firstVector, Vector3 secondVector)
	{
		return firstVector.x > secondVector.x || firstVector.y > secondVector.y || firstVector.z > secondVector.z;
	}

	private bool LessThan(Vector3 firstVector, Vector3 secondVector)
	{
		return GreaterThan(secondVector, firstVector);
	}
}
