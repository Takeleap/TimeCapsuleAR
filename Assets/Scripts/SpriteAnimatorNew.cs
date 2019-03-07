using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteAnimatorNew : MonoBehaviour {

	public	Image		targetImage;
	public	float	frameRate = 24f;
	private	float	updateRate = 0f;
	public	Sprite[]		clips;

	int counter = 0;
	
	void Start () 
	{
		updateRate = 1f / frameRate;

		//StopCoroutine("StartAnimation");
		//StartCoroutine("StartAnimation");

		CancelInvoke("SpriteAnim");
		InvokeRepeating("SpriteAnim", 0f, updateRate);
	}
	
	void SpriteAnim()
	{
		targetImage.sprite = clips[counter];		

		counter += 1;

		if(counter == clips.Length)
		{
			counter = 0;
		}
	}
}
