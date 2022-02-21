using System;
using UnityEngine;

public class SlideStickController : MonoBehaviour
{
	public GameObject electricPrefab;
	[HideInInspector] public GameObject electric;
	public GameObject shield;
	public GameObject shieldPoints;

	public static SlideStickController instance;

	private void Awake()
	{
		instance = this;
	}

	private void OnCollisionStay(Collision collision)
	{
		electric.transform.position = collision.contacts[0].point;

	}

	private void OnCollisionEnter(Collision collision)
	{
		electric = Instantiate(electricPrefab);
		Debug.Log("electric");
		for (int i = 0; i < DoorController.instance.points.Length; i++)
		{
			DoorController.instance.points[i].SetActive(false);
		}

		//shieldPoints.SetActive(true);
	}

	private void OnCollisionExit(Collision collision)
	{
		var emission = electric.GetComponent<ParticleSystem>().emission;
		emission.enabled = false;
		Destroy(electric);
		/*if (shield.activeSelf)
		{
			shield.SetActive(false);	
		}*/
		for (int i = 0; i < DoorController.instance.points.Length; i++)
		{
			DoorController.instance.points[i].SetActive(true);
		}
		//shieldPoints.SetActive(false);
	}
}
