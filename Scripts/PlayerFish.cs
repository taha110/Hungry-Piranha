using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerFish : MonoBehaviour {

	public Animator anim;
	public Rigidbody2D rb;
	public float thrust;
	public Text scoreText;
	public Text objectiveScoreText;
	public Text highScoreText;


	int currentScore;

	public int[] levelPoints;
	public float[] sizes;

	public GameObject gameOverScreen;
	public GameObject gameOverText1;
	public GameObject gameOverText2;
	public GameObject gameOverText3;

	public int mysize;

	// ////////////// physics ////////////
	public float vertSpeed;
	public float jumpSpeed;
	public float fallingConstant;
	public float YSpeed;

	public AudioClip audio_move1;
	public AudioClip audio_move2;
	public AudioClip audio_move3;

	public AudioClip audio_chomp1;
	public AudioClip audio_chomp2;
	public AudioClip audio_chomp3;

	public AudioClip audio_new_record;



	int rand = 0;
	AudioSource audioSource;
	
	// Use this for initialization
	void Start () {
	
	anim = GetComponent<Animator>();
	rb = GetComponent<Rigidbody2D>();
	currentScore=0;

	highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();

	gameOverScreen.SetActive(true);

	audioSource = GetComponent<AudioSource>();

	if(PlayerPrefs.HasKey("FishSide")){
		if(PlayerPrefs.GetInt("FishSide") == 0){

		}else{
			this.gameObject.transform.position = new Vector3(-6 , 0 , -5);
			this.gameObject.transform.eulerAngles  = new Vector3(0,180,0);
			        Debug.Log(" fishhhhhhhhh   rotation");
		}
	}

	}
	
	// Update is called once per frame
	void Update () {
	
	//Debug.Log(PlayerPrefs.GetInt("FishSide"));

		if (Input.GetKeyDown("space")){
//            print("space key was pressed");

			//anim.SetTrigger("eat");

			//rb.AddForce(transform.up * thrust);
			vertSpeed = jumpSpeed;


			//random for sound

			rand = Random.Range(0,2);
			switch (rand)
        	{
        	case 0:
						audioSource.PlayOneShot(audio_move1, 0.5F);
            break;
			case 1:
						audioSource.PlayOneShot(audio_move2, 0.5F);
            break;
			case 2:
						audioSource.PlayOneShot(audio_move3, 0.5F);
            break;
			}


    	}

		 for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
				            //print("space key was pressed");

				//rb.AddForce(transform.up * thrust);

				vertSpeed = jumpSpeed;

			rand = Random.Range(0,2);
			switch (rand)
        	{
        	case 0:
						audioSource.PlayOneShot(audio_move1, 0.5F);
            break;
			case 1:
						audioSource.PlayOneShot(audio_move2, 0.5F);
            break;
			case 2:
						audioSource.PlayOneShot(audio_move3, 0.5F);
            break;
			}

			}
		}


		this.gameObject.transform.position  = new Vector3(this.gameObject.transform.position.x , (YSpeed) , this.gameObject.transform.position.z);

				vertSpeed -= fallingConstant * Time.deltaTime;

				if(YSpeed < -5.2f && vertSpeed < 0){     			
				
				}else if(YSpeed > 5f && vertSpeed > 0){

				}else{
				YSpeed +=  vertSpeed * Time.deltaTime;

				}




	}

			void OnTriggerEnter2D(Collider2D col) {
 
				if(col.gameObject.name.Contains("fish") && col.gameObject.GetComponent<EnemyFishScript>().fishSize > mysize ){
						gameOverScreen.GetComponent<EasyTween>().OpenCloseObjectAnimation();
						VisiblityTrue(gameOverText1);
						VisiblityTrue(gameOverText2);
						VisiblityTrue(gameOverText3);
						gameOverText2.GetComponent<Text>().text=currentScore.ToString();
						GamePlayUI_Manager._GameOver = true;


						if(PlayerPrefs.GetInt("HighScore") < currentScore){
							        PlayerPrefs.SetInt("HighScore", currentScore);
						}

						Destroy(this.gameObject);

				}
        		else if(col.gameObject.name.Contains("fish")){
					anim.SetTrigger("eat");			
					
					if(col.gameObject.name.Contains("Minnow_fish")){
					currentScore +=1;							
					scoreText.text = currentScore.ToString();
					} else if(col.gameObject.name.Contains("Carp_fish")){
					currentScore +=3;							
					scoreText.text = currentScore.ToString();
					} else if(col.gameObject.name.Contains("Bass_fish")){
					currentScore +=10;							
					scoreText.text = currentScore.ToString();
					} else if(col.gameObject.name.Contains("Shark_fish")){
					currentScore +=30;							
					scoreText.text = currentScore.ToString();
					} 

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

					HighScoreBreak();
					PlayerFishLevelCheck();
				}
    		}

	
	
	public void VisiblityTrue(GameObject obj){
			obj.SetActive(true);

	}

	public void HighScoreBreak() {

		if(currentScore == PlayerPrefs.GetInt("HighScore") + 1){
			audioSource.PlayOneShot(audio_new_record, 0.7F);
		}
	}


	public void PlayerFishLevelCheck(){
		if(currentScore == 25){
			mysize = 2;
			this.gameObject.transform.localScale = new Vector3(0.3f , 0.3f , 0.3f); 
									audioSource.PlayOneShot(audio_new_record, 0.7F);
									objectiveScoreText.text = "/ 250";


		}else if(currentScore == 250){
			mysize = 3;
			this.gameObject.transform.localScale = new Vector3(0.5f , 0.5f , 0.5f); 
									audioSource.PlayOneShot(audio_new_record, 0.7F);
									objectiveScoreText.text = "/ 750";
		}else if(currentScore == 750){
			mysize = 4;
			this.gameObject.transform.localScale = new Vector3(0.8f , 0.8f , 0.8f); 	
									audioSource.PlayOneShot(audio_new_record, 0.7F);
									objectiveScoreText.text = "/ 1000";
		}else if(currentScore == 1000){
			mysize = 5;
			this.gameObject.transform.localScale = new Vector3(0.9f , 0.9f , 0.9f); 
									audioSource.PlayOneShot(audio_new_record, 0.7F);

		}
	}


}	
