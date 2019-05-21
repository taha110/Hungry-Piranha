using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour {


	public GameObject [] enemyFishes;

	public AudioClip audio_bubble1;
	public AudioClip audio_bubble2;
	public AudioClip audio_bubble3;
	AudioSource audioSource;

	public float fishSpawnDelay;

	int rand;

	public List < int > fishListToSpawn = new List <int>() ;
	public int[] totalFish;
	public int[] selectedFish = new int[4];

	public List < int > fishHeightListToSpawn = new List <int>() ;
	public int[] totalFishHeight;
	public int[] selectedFishHeight = new int[4];

	public GameObject PirhanaFish;

	IEnumerator WaitAndPrint()
    {
		while (true)
		{
			SelectFishes();

			
			for(int i =0; i<4; i++){
				//int tempFish = Random.Range(0,4);

			if(PlayerPrefs.HasKey("FishSide")){
				if(PlayerPrefs.GetInt("FishSide") == 0){
				Instantiate(enemyFishes[selectedFish[i]] , new Vector3(-20, selectedFishHeight[i] , enemyFishes[selectedFish[i]].transform.position.z), Quaternion.identity);
				}else{
				Instantiate(enemyFishes[selectedFish[i]] , new Vector3(20, selectedFishHeight[i] , enemyFishes[selectedFish[i]].transform.position.z),  Quaternion.identity);
					}
				}

				

			}

			fishListToSpawn.Clear();
			fishHeightListToSpawn.Clear();
			
			//random for sound

			rand = Random.Range(0,2);
			switch (rand)
        	{
        	case 0:
						audioSource.PlayOneShot(audio_bubble1, 1F);
            break;
			case 1:
						audioSource.PlayOneShot(audio_bubble2, 1F);
            break;
			case 2:
						audioSource.PlayOneShot(audio_bubble3, 1F);
            break;
			}



			// suspend execution for 5 seconds
       	 	yield return new WaitForSeconds(fishSpawnDelay);
					
		}       
    }

    IEnumerator Start()
    {
        print("Starting " + Time.time);
			audioSource = GetComponent<AudioSource>();

        // Start function WaitAndPrint as a coroutine
        yield return StartCoroutine("WaitAndPrint");
        //print("Done " + Time.time);
		
    }

	public void SelectFishes(){
		for(int i=0; i<7; i++){
			fishListToSpawn.Add(totalFish[i]);
		}

		for(int j=0; j<4; j++){
			int tempint = fishListToSpawn.Count;
			int rand_select = Random.Range(0,tempint);
			selectedFish[j] = fishListToSpawn[rand_select];
			fishListToSpawn.RemoveAt(rand_select);
		}
		
		// special size conditions ...........
		if(PirhanaFish != null){
			// size 1 -------------------
		if(PirhanaFish.GetComponent<PlayerFish>().mysize == 1){

		int tempMinnowCount =0;

		for(int i=0; i<4; i++){
			if(selectedFish[i]==0 ){
				tempMinnowCount++;
			}
		}
		if(tempMinnowCount==0){
			selectedFish[0]=0;
			}
		}
		

			// size 2 ------------------------------
		if(PirhanaFish.GetComponent<PlayerFish>().mysize == 2){

		int tempBassCount =0;
		int tempSharkCount =0;

		for(int i=0; i<4; i++){
			if(selectedFish[i]==2 ){
				tempBassCount++;
			}
			if(selectedFish[i]==3 ){
				tempSharkCount++;
			}
		}
		if(tempBassCount==2 && tempSharkCount==1){
			selectedFish[0]=0;
			selectedFish[1]=1;

			}
		}
		
		}
		// ,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,
		for(int i=0; i<8; i++){
			fishHeightListToSpawn.Add(totalFishHeight[i]);
		}

		for(int j=0; j<4; j++){
			int tempHeightint = fishHeightListToSpawn.Count;
			int rand_select_height = Random.Range(0,tempHeightint);
			selectedFishHeight[j] = fishHeightListToSpawn[rand_select_height];
			fishHeightListToSpawn.RemoveAt(rand_select_height);
		}

	}

}
