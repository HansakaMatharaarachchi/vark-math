using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Traingle : MonoBehaviour
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
			if (hit.transform.name == "Sphere" || hit.transform.name == "Sphere(Clone)" || hit.transform.name == "Sphere(Clone)")
			{
				Destroy(hit.transform.gameObject);
			Instantiate(smoke, hit.point, Quaternion.LookRotation(hit.normal));
				if (hit.transform.name == "Sphere" || hit.transform.name == "Sphere(Clone)") {
					
					Instantiate(textCube, hit.point, Quaternion.LookRotation(hit.normal));
				}
				 
		}
		}




}

}
