using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLock : MonoBehaviour
{
	private float yVal = 0f;
	Vector3 dir = Vector3.zero;

	void LateUpdate()
    {
		yVal = transform.rotation.eulerAngles.y;
		dir = new Vector3(0f, yVal, 0f);
		transform.rotation = Quaternion.Euler(dir);
    }
}
