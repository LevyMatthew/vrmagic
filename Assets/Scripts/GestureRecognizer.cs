using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PDollarGestureRecognizer;
using System.IO;

public class GestureRecognizer : MonoBehaviour{

	public Transform movementSource;
	public GameObject debugCubePrefab;
	public bool creationMode = true;
	public string newGestureName;

	public float newPositionThresholdDistance = 0.05f;

	private List<Gesture> trainingSet = new List<Gesture>();
	private bool isMoving = false;
	private bool isPressed = false;
	private List<Vector3> positionsList = new List<Vector3>();
	private string gesturesFolder;

	void Start(){
		gesturesFolder = Application.persistentDataPath + "/Gestures";
		if(!Directory.Exists(gesturesFolder)){
			Directory.CreateDirectory(gesturesFolder);
		}
		string[] gestureFiles = Directory.GetFiles(gesturesFolder, "*.xml");
		foreach(var item in gestureFiles){
			trainingSet.Add(GestureIO.ReadGestureFromFile(item));
		}
	}

	void Update(){
		isPressed = Input.GetButton("Fire1");
		if(!isMoving && isPressed){
			StartMovement();
		}
		else if(isMoving && !isPressed){
			EndMovement();
		}
		else if(isMoving && isPressed){
			UpdateMovement();
		}
	}

	void StartMovement(){
		isMoving = true;
		positionsList.Clear();
		positionsList.Add(movementSource.position);
		if(debugCubePrefab){
			Destroy(Instantiate(debugCubePrefab, movementSource.position, Quaternion.identity), 3);
		}
	}

	void EndMovement(){
		isMoving = false;

		Point[] pointArray = new Point[positionsList.Count];

		for(int i = 0; i < positionsList.Count; i++){
			Vector2 screenPoint = Camera.main.WorldToScreenPoint(positionsList[i]);
			pointArray[i] = new Point(screenPoint.x, screenPoint.y, 0);
		}

		Gesture newGesture = new Gesture(pointArray);

		if(creationMode){
			newGesture.Name = newGestureName;
			trainingSet.Add(newGesture);

			string fileName = gesturesFolder + "/" + newGestureName + ".xml";
			GestureIO.WriteGesture(pointArray, newGestureName, fileName);
		}
		else{
			Result result = PointCloudRecognizer.Classify(newGesture, trainingSet.ToArray());
			Debug.Log(result.GestureClass + result.Score);
		}
	}

	void UpdateMovement(){
		Vector3 lastPosition = positionsList[positionsList.Count - 1];
		if(Vector3.Distance(movementSource.position, lastPosition) > newPositionThresholdDistance){
			positionsList.Add(movementSource.position);
			if(debugCubePrefab){
				Destroy(Instantiate(debugCubePrefab, movementSource.position, Quaternion.identity), 3);
			}
		}
	}
}
