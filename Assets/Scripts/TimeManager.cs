﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {

    public void Pause() {
        Time.timeScale = 0f;
    }
    public void X1() {
        Time.timeScale = 1f;
    }

    public void X2() {
        Time.timeScale = 2f;
    }

    public void X3() {
        Time.timeScale = 3f;
    }

}