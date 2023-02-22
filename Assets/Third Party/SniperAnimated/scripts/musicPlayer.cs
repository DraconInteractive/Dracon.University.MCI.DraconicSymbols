using UnityEngine;
using System.Collections;

public class musicPlayer : MonoBehaviour {
	//private AudioSource myaudio;
	public AudioClip[] musicLoops;   
	private audioFader fader;
	public float musicvolume = 0.5f;
	// Use this for initialization
	void Awake() {
		//myaudio = GetComponent<AudioSource>();
		fader = GetComponent<audioFader>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!fader.IsPlaying)
		{
			int n = Random.Range(1,musicLoops.Length);


			fader.Fade(musicLoops[n],musicvolume,false);

			musicLoops[n] = musicLoops[0];
			musicLoops[0] = fader.Clip;
		}
	}
}
