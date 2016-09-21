    //To use: Place this script on an empty movie clip with an Audio Source attached
    //Do NOT use pitch bending with this program. Pitch bending changes the duration of the clip, making the code inconsistent in its timing.
    //Script works best if source audio files are not 3D sounds.
     
    var audioClips: AudioClip[]; //What audio will be used? Increase the number to however many clips you need, and then drag the audio clips in.
    var playInSequence: boolean = true; //Should audio clips play in a specific sequence? If not, audio will be played randomly
     
    var introSequence: String; //Is there a predefined intro? If so, add audio ID numbers in order. Intro is not affected by randomness.
    var mainSequence: String; //What is the main sequence of audio? This must not be left blank.
     
    /*
    In either introSequence or mainSequence, add a series of numbers corresponding to the audio clips you have added, separated by commas.
    For example: 0, 1, 2, 2, 1, 2, 1, 3, 4, 1, 3, 2
     
    Intro will play first, then the main sequence will play, looping itself if doesSequenceLoop is toggled. If playInSequence is toggled, the main
    sequence of numbers will be randomly played, looping infinitely.
     
    If you wish to insert silence into either introSequence or mainSequence, add a float.
    For example: 0, 0.8, 1, 1.5, 2, 3.0, 3 will play clip 0, then 0.8 seconds, then clip 1, then 1.5 seconds, then clip 2, then 3.0 seconds, then clip 3.
    */
     
    private var introStringArray: Array;
    private var mainStringArray: Array;
    private var totalStringArray: Array;
    var doesSequenceLoop: boolean = true; //Does the sequence loop? This does nothing if playInSequence is false;
     
    private var audioTimer: float; //Timer, marks the beginning of each new audio clip
    private var timerOffset: float; //Offset, records the length of the current clip
    private var currentClip: int = 0;
     
    function Start () {
        if (mainSequence.length == 0) {
            Debug.LogError("Main Sequence must contain at least one number", transform);
        }
       
        introStringArray = introSequence.Split(", "[0]);
        mainStringArray = mainSequence.Split(", "[0]);
        totalStringArray = introStringArray.Concat(mainStringArray); //both sequences added together into one array
    }
     
    function Update () {
        //Debug.Log (Time.time - audioTimer);
        if (audioTimer + timerOffset < Time.time)
        { //if the end of the current audio clip is reached
            if (!playInSequence && introStringArray.length == 0)
            {
                currentClip = Random.Range (0, totalStringArray.length); //if there's no intro and it's set to random, choose the first clip
            }
           
            if (totalStringArray[currentClip].Contains(".")) { //if the chosen element of the array is a floating point number...
                timerOffset = parseFloat(totalStringArray[currentClip]); //set the length of silence to that float
            }
            else 
            {
                audio.clip = audioClips[parseInt(totalStringArray[currentClip])]; //find the correct audio clip to play
                timerOffset = audio.clip.length;
                audio.Play();
            }
            //Debug.Log("Clip is "+totalStringArray[currentClip]+" in sequence number "+currentClip);
           
            if (playInSequence)
            { //if audio is to be played in a specific sequence
                if (totalStringArray.length > currentClip+1) { //if we're not at the end of the sequence
                    currentClip++; //advance to the next clip
                } else {
                    if (doesSequenceLoop) { //otherwise, should we loop at all? If yes...
                        currentClip = introStringArray.length; //go back to the beginning of the main looping sequence
                    } else {
                        audio.Stop();
                    }
                }
            } else { //if we're playing audio randomly
                if (currentClip < introStringArray.length-1) { //if the next clip is still part of the intro (which is never played randomly)
                    currentClip++; //go to the next part of the intro
                } else {
                    currentClip = Random.Range(introStringArray.length, totalStringArray.length);
                }
            }
            audioTimer = Time.time;
        }
    }
     
    //Music Chain created by Chris Hendricks in 2011.
    //www.screenhog.com