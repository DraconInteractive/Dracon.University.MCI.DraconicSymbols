using UnityEngine;
using System.Collections;

public class knife : MonoBehaviour {
	public Vector3 normalposition;
	public float speed = 2f;
	public Transform player;


	public AudioSource myAudioSource;


	public AudioSource fireAudioSource;

	public AudioClip[] fireSounds;

	public AudioClip readySound;
	public AudioClip firespecialSound;

	public AnimationClip[] fireAnims;

	public AnimationClip idleAnim;
	public AnimationClip readyAnim;
	public AnimationClip hideAnim;
	public AnimationClip fireSpecialAnim;


	public float idleAnimSpeed = .4f;
	public float inaccuracy = 0.02f;
	public float force  = 500f;
	public float damage = 50f;
	public float range = 2f;



	public Vector3 retractPos;


	private bool retract = false;	

	public Camera weaponcamera;
	public float normalFOV  = 65f;
	public float weaponnormalFOV = 32f;
	public Transform rayfirer;
	public Transform grenadethrower;
	private Vector3 wantedrotation;
	public float runXrotation = 10;
	raycastfire weaponfirer;
	weaponselector inventory;
	playercontroller playercontrol ;
	Animation myanimation;

	void Awake()
	{
		weaponfirer = rayfirer.GetComponent<raycastfire>();
		playercontrol = player.GetComponent<playercontroller>();
		inventory = player.GetComponent<weaponselector>();
		myanimation = GetComponent<Animation>();

	}
	void Start () 
	{
		

		onstart();

	}
	
	// Update is called once per frame
	void Update () 
	{

		float step = speed * Time.deltaTime;
		float newField = Mathf.Lerp(Camera.main.fieldOfView, normalFOV , Time.deltaTime * 2);
		float newfieldweapon = Mathf.Lerp(weaponcamera.fieldOfView, weaponnormalFOV, Time.deltaTime * 2);
		Camera.main.fieldOfView = newField;
		weaponcamera.fieldOfView = newfieldweapon;
		inventory.currentammo = 0;
		//inventory.totalammo = 0;
		float Xtilt = Input.GetAxisRaw("Mouse Y") * 20f * Time.smoothDeltaTime;
		float Ytilt = Input.GetAxisRaw("Mouse X") * 20f * Time.smoothDeltaTime;
		if (retract) {
			transform.localPosition = Vector3.MoveTowards (transform.localPosition, retractPos, 5 * Time.deltaTime);
		}
		else 
		{
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, normalposition, step);
		}

		if (Input.GetButton("ThrowGrenade") && myanimation.IsPlaying (idleAnim.name) && inventory.grenade>0)
		{
			if(Time.timeSinceLevelLoad>(inventory.lastGrenade+1)){
				inventory.lastGrenade=Time.timeSinceLevelLoad;			
				StartCoroutine(setThrowGrenade());
			}
		}
		if (playercontrol.running)
		{
			

			wantedrotation = new Vector3(Xtilt + runXrotation,Ytilt,0f);

		}
		else
		{
			

			wantedrotation = Vector3.zero;

		}
		transform.localRotation = Quaternion.Lerp(transform.localRotation,Quaternion.Euler(wantedrotation),step * 3f);
	

		if (Input.GetButton("Fire1") || Input.GetAxis ("Fire1")>0.1  && myanimation.IsPlaying (idleAnim.name))
		{
			
			myanimation.Stop(idleAnim.name);
			if (Input.GetButton("Aim")|| 	Input.GetAxis("Aim") > 0.1)
			{
					fireSpecial();	
			}
			else
			{
				fire();
			}
		}
		else if (!myanimation.isPlaying)
		{
			
		
			myanimation[idleAnim.name].speed = idleAnimSpeed; 
			myanimation.CrossFade(idleAnim.name);

		}
	}



	void onstart()
	{
		myAudioSource.Stop();
		fireAudioSource.Stop();
		retract = false;

		GetComponent<Animation>().Stop();

		if(weaponfirer==null) weaponfirer = rayfirer.GetComponent<raycastfire>();
		weaponfirer.inaccuracy = inaccuracy;
		weaponfirer.damage = damage;
		weaponfirer.range = range;
		weaponfirer.force = force;
		weaponfirer.projectilecount = 1;

		if(inventory==null) inventory = player.GetComponent<weaponselector>();
		inventory.showAIM(false);
		myanimation.Play (readyAnim.name);
		myAudioSource.clip = readySound;
		myAudioSource.loop = false;

		myAudioSource.Play ();

	}

	void fire()
	{
		if (!myanimation.isPlaying)
		{
			fireAudioSource.clip = fireSounds[Random.Range(0,fireSounds.Length)];
			fireAudioSource.pitch = 0.98f + 0.1f *Random.value;
			fireAudioSource.Play();
			myanimation.clip = fireAnims[Random.Range(0,fireAnims.Length)];
			myanimation.Play();  
			StartCoroutine(firedelayed(0.3f));

		}

	}

	void fireSpecial()
	{
		if (!myanimation.isPlaying)
		{
			fireAudioSource.clip = firespecialSound;
			fireAudioSource.pitch = 0.98f + 0.1f *Random.value;
			fireAudioSource.Play();
			myanimation.Play(fireSpecialAnim.name);
			StartCoroutine(firedelayed(0.3f));
			StartCoroutine(firedelayed(.7f));
		}

	}

	void doRetract()
	{
		
		myanimation.Play(hideAnim.name);

	}
	void doNormal()
	{

		retract = false;
		onstart();
	}
	IEnumerator firedelayed(float waitTime)
	{
		yield return new WaitForSeconds (waitTime);
		weaponfirer.fireMelee();
	}

	IEnumerator setThrowGrenade()
	{
		retract = true;
		grenadethrower.gameObject.SetActive(true);
		grenadethrower.gameObject.BroadcastMessage("throwstuff");
		yield return new WaitForSeconds(grenadethrower.GetComponent<Animation>()["throwAnim"].length);
		retract = false;
		grenadethrower.gameObject.SetActive(false);
	}

}
