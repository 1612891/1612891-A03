﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wait : MonoBehaviour
{
    public float wait_time = 5f;

    void Start()
    {
        StartCoroutine(WaitForIntro());
    }
    IEnumerator WaitForIntro()
    {
        yield return new WaitForSeconds(wait_time);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
