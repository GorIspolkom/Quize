using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DemoSizes : MonoBehaviour {

	public Slider slider;
	public Text icon;

	public void UpdateSize(){
		icon.fontSize = Mathf.RoundToInt(slider.value);
	}
}
