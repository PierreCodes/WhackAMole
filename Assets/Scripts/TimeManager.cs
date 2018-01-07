using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TimeManager : MonoBehaviour {

    //we need to get access to the Time Text here
    public Text timeLabel;

    //set up a game over text to let the user know the game ended
    public Text gameOverLabel;

    //set the time for our game
    float maxTime = 60;//this will be in seconds

    //this is the public bool we will use in Button Manager to decide if the game continues, but we don't need it to be revealed in the Editor so we add the "Hide" to it
    [HideInInspector]//hides any public in the Editor, but keeps it public so we can still access it from other scripts
    public bool gameOver = false;

    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        //start the count down
        maxTime -= Time.deltaTime;// the -= operator counts down

        //convert to seconds so it displays nicely
        int seconds = Mathf.RoundToInt(maxTime % 60f);

        //make sure we haven't hit 0 
        if (seconds >= 0)
        {
            //update our label in string format with our "Time" plus seconds
            timeLabel.text = "Time: " + seconds.ToString("00");

        }
        else
        {
            //Do this check here so that it only gets called once in the Update!
            if (gameOver == false)
            {
                //when the time hits '0' our game is over
                gameOver = true;
                //set our annoying "GameOver" text object to active so the user knows why the scene is changing
                gameOverLabel.gameObject.SetActive(true);

                //after a short delay, change the scene
                StartCoroutine(ChangeScene());
            }

        }

    }

    IEnumerator ChangeScene()
    {
        //after a 2 second delay of the player seeing that the game is over, we switch scenes so they can see their score
        yield return new WaitForSeconds(2);
        //change the scene using our beautiful GameScenes Enums but we must convert it to a string first! 
        SceneManager.LoadSceneAsync(GameScenes.ScoreScene.ToString());

    }
}
