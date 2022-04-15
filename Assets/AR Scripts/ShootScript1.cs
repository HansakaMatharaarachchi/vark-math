using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootScript1 : MonoBehaviour
{

	public GameObject arCamera;
	public GameObject smoke;
	public GameObject textCube;
 

	public void Shoot()
	{ 
		RaycastHit hit;
		if (Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out hit))
		{
			if (hit.transform.name == "Spider1" || hit.transform.name == "Spider1(Clone)" || hit.transform.name == "Spider2(Clone)"){
				Destroy(hit.transform.gameObject);
			Instantiate(smoke, hit.point, Quaternion.LookRotation(hit.normal));
				if (hit.transform.name == "Spider1" || hit.transform.name == "Spider1(Clone)") {
					Instantiate(textCube, hit.point, Quaternion.LookRotation(hit.normal));
					 
					 
				}
				 
		}
		}




}

}
