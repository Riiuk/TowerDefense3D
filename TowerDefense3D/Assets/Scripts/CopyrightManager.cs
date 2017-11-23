using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CopyrightManager : MonoBehaviour
{

    public string companyName;
    public string version;

    GameObject gameObjectText;
    Text text;

    CopyrightManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start ()
    {

        gameObjectText = GameObject.FindGameObjectWithTag("CopyrightText");
        text = gameObjectText.GetComponent<Text>();

        text.text = companyName + " || Versión " + version;
	}
}
