using UnityEngine;
using System.Collections;

public class ForWorldKeyCrosshairToggle : MonoBehaviour 
{

	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.C))
		{
			ForWorldKeyCrosshair.DisplayCrosshair = !ForWorldKeyCrosshair.DisplayCrosshair;
		}
	}
}
