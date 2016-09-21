using UnityEngine;
using System.Collections;

public class GameTime : MonoBehaviour {

	public Transform[] sun;
	private Sun[] _sunScript;
	public float dayCycleInMinutes = 1;

	private const float SECOND = 1;
	private const float MINUTE = 60 * SECOND;
	private const float HOUR = 60 * MINUTE;
	private const float DAY = 24 * HOUR;

	private const float DEGREES_PER_SECOND = 360 / DAY;

	private float _degreeRotation;

	private float _timeOfDay;

	// Use this for initialization
	void Start () {
		_sunScript = new Sun[sun.Length];
		for(int cnt = 0; cnt < sun.Length; cnt++){
			Sun temp = sun[cnt].GetComponent<Sun>();

			if(temp == null){
				Debug.LogWarning("Sun script not found. Adding it.");
				sun[cnt].gameObject.AddComponent<Sun>();
				temp = sun[cnt].GetComponent<Sun>();
			}
			_sunScript[cnt] = temp;
		}
		_timeOfDay = 0;
		_degreeRotation = DEGREES_PER_SECOND * DAY / (dayCycleInMinutes * MINUTE);
	}
	
	// Update is called once per frame
	void Update () {
		sun[0].Rotate (new Vector3(_degreeRotation, 0, 0) * Time.deltaTime);

		_timeOfDay += Time.deltaTime;
		Debug.Log(_timeOfDay);
	}
}
