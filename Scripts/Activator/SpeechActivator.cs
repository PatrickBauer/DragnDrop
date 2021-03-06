﻿using UnityEngine;
using System.Collections;

public class SpeechActivator : DragDropActivator {
    public SpeechManager speechManager;

    // Use this for initialization
    override public void Start () {
        base.Start();
        Debug.Log("Speech Activator startet");
	}
	
	// Update is called once per frame
	override public void Update() {
        base.Update();

        // get the speech manager instance
        if (speechManager == null)
        {
            speechManager = SpeechManager.Instance;
        }

        if (speechManager == null || !speechManager.IsSapiInitialized())
        {
            Debug.Log("Speech Manager nicht initialisiert");
        }
        
        if (speechManager != null && speechManager.IsSapiInitialized())
        {
            if (speechManager.IsPhraseRecognized())
            {
                string sPhraseTag = speechManager.GetPhraseTagRecognized();

                Debug.Log("Erkenne " + sPhraseTag);

                if (sPhraseTag == "START")
                {
                    isActive = true;
                    changedSinceLastFrame = true;
                    speechManager.ClearPhraseRecognized();
                    return;
                }

                if (sPhraseTag == "ENDE")
                {
                    isActive = false;
                    changedSinceLastFrame = true;
                    speechManager.ClearPhraseRecognized();
                    return;
                }

                speechManager.ClearPhraseRecognized();
            }

            changedSinceLastFrame = false;
        }
    }
    
}
