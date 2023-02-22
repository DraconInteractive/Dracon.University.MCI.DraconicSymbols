using UnityEngine;
using System.Collections;

public class meleeWeapon : MonoBehaviour {
	
	public AudioClip attackSound;
	public AudioSource myAudioSource;

	public Transform rayfirer;
	raycastfire weaponfirer;
	Animation myanimation;

	void Awake()
	{

		myanimation = GetComponent<Animation>();
		weaponfirer = rayfirer.GetComponent<raycastfire>();
	}
	void melee()
	{
		if (!myanimation.isPlaying) 
		{
			
			myAudioSource.clip = attackSound;
			myAudioSource.pitch = 0.9f + 0.1f * Random.value;
			myAudioSource.Play ();
			myanimation.Play ("knifeAttack");
			StartCoroutine(firedelayed(0.16f));
		}
	}
	IEnumerator firedelayed(float waitTime)
	{
		
		yield return new WaitForSeconds (waitTime);
		weaponfirer.fireMelee();
	}

}
