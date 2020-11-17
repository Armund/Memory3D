using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{
	public Image picture;
	public int value;

	public bool IsStateUp = true;
	private Animation turnAnimation;

	public void Turn() {
		if (IsStateUp) {
			turnAnimation.Play("TurnDownAnimation");
		} else {
			turnAnimation.Play("TurnUpAnimation");
		}
		IsStateUp = !IsStateUp;
	}

 
    void Awake()
    {
		turnAnimation = GetComponent<Animation>();
	}
}
