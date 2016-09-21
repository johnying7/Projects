using UnityEngine;
using System.Collections;

public class music : MonoBehaviour { //attach to BGM object (child of player character)
	
	public int numSongs;
    public AudioClip[] bgmList;
	
	private float startSongTime;
	private float endSongTime;
	private int currentSong;
	
	// Use this for initialization
	void Start () {
		currentSong = 0;
	}
     
    void Update () {
		
		if (Time.time > startSongTime + endSongTime){ //if the current time is over the end of the song
			
			if (currentSong == numSongs){ //if the song is at the end of the list
				currentSong = 0;
			}
			audio.clip = bgmList[currentSong]; //set the current audio clip to the current song in the list
			endSongTime = audio.clip.length; //set the end of the song's time to the length of the song
			audio.volume = 0.05f;
			audio.Play(); //play the song
			currentSong++; //go to the next song in the list
			startSongTime = Time.time; //set the starting song time to the real time
		}
		
    } //end update
} //end music class
