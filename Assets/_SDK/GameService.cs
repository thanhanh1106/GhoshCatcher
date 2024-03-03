using AppsFlyerSDK;
using System;
using System.Collections;
using System.Collections.Generic;
using Unicorn;
using UnityEngine;

enum VerifyFirebase
{
    Verifying,
    Done,
    Error
}

[Singleton("GameService", true)]
public class GameService : Singleton<GameService>
{
    private VerifyFirebase firebaseReady = VerifyFirebase.Verifying;
    public bool FirebaseInitialized = false;
    public bool IsLoadRemoteConfigSucces = false;
    // Start is called before the first frame update
    void Start()
    {
        InitData();
    }

    private void InitData()
    {

        

        StartCoroutine(InitFirebase());
    }

    private IEnumerator InitFirebase()
    {
        while (firebaseReady == VerifyFirebase.Verifying)
        {
            yield return null;
        }

        if (firebaseReady == VerifyFirebase.Done)
            InitializeFirebase();
    }

    void InitializeFirebase()
    {
        
    }
}
