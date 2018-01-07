using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The Score types we will save to the player prefs
/// </summary>
public enum ScoreTypes
{
    PlayerScore,
    PlayerHits,
    PlayerMisses

}

public class ScoreManager : MonoBehaviour {

    //connect the Score Label
    public Text scoreLabel;

    //create a variable here so that we can use it anywhere in this script, this will keep track of our score
    int score = 0;

	// Use this for initialization
	void Start()
	{
		//reset all the PlayerPrefs here! 
		PlayerPrefs.SetInt(ScoreTypes.PlayerHits.ToString(), 0);
		PlayerPrefs.SetInt(ScoreTypes.PlayerMisses.ToString(), 0);
		PlayerPrefs.SetInt(ScoreTypes.PlayerScore.ToString(), 0);


	}
    //We keep all the Player stats in this script to keep it clean.
    public void UpdateScore(int amount, bool addToScore){

        if(addToScore == true)
        {
			//Increment the players score

			//get the current score 
			score = PlayerPrefs.GetInt(ScoreTypes.PlayerScore.ToString());
			//reduce it with the -= operator by the amount that we passed to the method
			score += amount;
			//save the new score
			PlayerPrefs.SetInt(ScoreTypes.PlayerScore.ToString(), score);

			//log hits as well
            int hits = PlayerPrefs.GetInt(ScoreTypes.PlayerHits.ToString());
			hits += 1;
			PlayerPrefs.SetInt(ScoreTypes.PlayerHits.ToString(), hits);

		}
        else
        {
			//log misses so we can show the player her stats at the end of the game
            int misses = PlayerPrefs.GetInt(ScoreTypes.PlayerMisses.ToString());
			misses += 1;
			PlayerPrefs.SetInt(ScoreTypes.PlayerMisses.ToString(), misses);
        }

        //update the score label out side of the 'if' statement
        scoreLabel.text = "Score: " + score;

    }

}
