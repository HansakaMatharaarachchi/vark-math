using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootScript : MonoBehaviour
{

	public GameObject arCamera;
	public GameObject smoke;
	public Text textCube;
 

	public void Shoot()
	{
		textCube.text = "correct, it's a cube";
		RaycastHit hit;
		if (Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out hit))
		{
			if (hit.transform.name == "Cube" || hit.transform.name == "Cube(Clone)" || hit.transform.name == "Cube(Clone)"){
				Destroy(hit.transform.gameObject);
			Instantiate(smoke, hit.point, Quaternion.LookRotation(hit.normal));
				if (hit.transform.name == "Cube" || hit.transform.name == "Cube(Clone)") {
					
					Instantiate(textCube, hit.point, Quaternion.LookRotation(hit.normal));
				}
				 
		}
		}




}

}
