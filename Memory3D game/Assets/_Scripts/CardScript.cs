using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{
	public Image picture;
	public int value;

	private bool stateUp = true;
	private Animation turnAnimation;

	public void Turn() {
		if (stateUp) {
			turnAnimation.Play("TurnDownAnimation");
		} else {
			turnAnimation.Play("TurnUpAnimation");
		}
		stateUp = !stateUp;
	}

 
    void Awake()
    {
		turnAnimation = GetComponent<Animation>();
	}
}
