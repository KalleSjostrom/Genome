using UnityEngine;
using System.Collections;

public class TrackCollision : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D c) {
		Car car = c.GetComponent<Car>();
		car.OutsideTrack();
	}
}
