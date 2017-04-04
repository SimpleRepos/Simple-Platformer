using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutlinedTextScript : MonoBehaviour {

    public string text {
        get { return myText; }
        set
        {
            myText = value;
            foreach (Text t in children) { t.text = myText; }
        }
    }

    private string myText;

    private List<Text> children;

	void Start () {
        children = new List<Text>(GetComponentsInChildren<Text>());
	}
	
}
