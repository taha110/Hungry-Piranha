using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFishScript : MonoBehaviour {

	public float fishSpeed;
	public int fishSize;



	int rand = 0;
	AudioSource audioSource;
	
	public AudioClip audio_chomp1;
	public AudioClip audio_chomp2;
	public AudioClip audio_chomp3;

	public Animator anim;


	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
		anim = GetComponent<Animator>();

		if(PlayerPrefs.HasKey("FishSide")){
			if(PlayerPrefs.GetInt("FishSide") == 0){
			
			}
			else{
				this.gameObject.transform.eulerAngles  = new Vector3(0,180,0);

			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		//transform.Translate(Vector3.right * Time.deltaTime * fishSpeed);
		transform.Translate(Vector3.right * Time.deltaTime * fishSpeed);

		

		if(PlayerPrefs.HasKey("FishSide")){
			if(PlayerPrefs.GetInt("FishSide") == 0){

				if(this.gameObject.transform.position.x > 18){
				Destroy(this.gameObject);
				}
			}else{


				if(this.gameObject.transform.position.x < -18){
				Destroy(this.gameObject);
				}
			}
		}

	}

	//void OnCollisionEnter2D(Collision2D col){
		void OnTriggerEnter2D(Collider2D col) {


        if(col.gameObject.name.Contains("Pirana") && col.gameObject.GetComponent<PlayerFish>().mysize >= fishSize){
					        Destroy(this.gameObject);
		}
		else{

			// trigger eat anim
			print("trigger eat anim");
			anim.SetTrigger("eat");

									//random for sound

			rand = Random.Range(0,2);
			switch (rand)
        	{
        	case 0:
						audioSource.PlayOneShot(audio_chomp1, 0.7F);
            break;
			case 1:
						audioSource.PlayOneShot(audio_chomp2, 0.7F);
            break;
			case 2:
						audioSource.PlayOneShot(audio_chomp3, 0.7F);
            break;
			}
		}
    }

	


}
