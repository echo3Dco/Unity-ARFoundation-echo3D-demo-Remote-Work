/**************************************************************************
* Copyright (C) echoAR, Inc. 2018-2020.                                   *
* echoAR, Inc. proprietary and confidential.                              *
*                                                                         *
* Use subject to the terms of the Terms of Service available at           *
* https://www.echoar.xyz/terms, or another agreement                      *
* between echoAR, Inc. and you, your company or other organization.       *
***************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomBehaviour : MonoBehaviour
{
    [HideInInspector]
    public Entry entry;
    GameObject text;

    /// <summary>
    /// EXAMPLE BEHAVIOUR
    /// Queries the database and names the object based on the result.
    /// </summary>

    // Use this for initialization
    void Start()
    {
        // Add RemoteTransformations script to object and set its entry
        this.gameObject.AddComponent<RemoteTransformations>().entry = entry;

        // Qurey additional data to get the name
        string value = "";
        if (entry.getAdditionalData() != null && entry.getAdditionalData().TryGetValue("name", out value))
        {
            // Set name
            this.gameObject.name = value;
        }
    }

    // Update is called once per frame
    void Update()
    {

        // Only apply for speech bubble
        if (this.gameObject.name.Contains("Speech")) {
            string textString;
            // Qurey additional data to get the text
            if (entry.getAdditionalData() != null && entry.getAdditionalData().TryGetValue("text", out textString)) {
                
                // Show speech bubble
                foreach (Renderer r in this.gameObject.GetComponentsInChildren<Renderer>())
                    r.enabled = true;
                // Update text
                if (text == null) {
                    // Add text
                    text = new GameObject();
                    text.AddComponent<TextMesh>();
                }
                TextMesh t = text.GetComponent<TextMesh>();
                text.name = "Text";
                t.color = Color.black;
                t.text = textString;
                t.fontSize = 200;
                text.transform.localScale = 0.1f * Vector3.one;
                text.transform.parent = this.transform.parent;
                text.transform.position = text.transform.parent.position + new Vector3 (-0.6f, 7.3f, 0)  * text.transform.parent.localScale.magnitude;
            } else {
                // No text, hide speech bubble
                foreach (Renderer r in this.gameObject.GetComponentsInChildren<Renderer>())
                    r.enabled = false;
                // Destroy text
                Destroy(text);
                text = null;
            }
        }
        
    }
}