using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform Player;

    public float YAxis;
    public float minLengthX;
    public float maxLengthX;
	
	void Update () {
        float playerPos = Player.transform.position.x;
        if(playerPos<maxLengthX&&playerPos>minLengthX)
        transform.position = new Vector3(playerPos,YAxis,-10);
	}
}
