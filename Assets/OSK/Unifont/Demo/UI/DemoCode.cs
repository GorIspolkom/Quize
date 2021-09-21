using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DemoCode : MonoBehaviour {

	public InputField field;
	public Text icon;

	public void SetIcon(){
		icon.text = Unifont.FindByName(field.text);
	}
}
