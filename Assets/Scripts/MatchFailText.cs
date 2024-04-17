using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchFailText : MonoBehaviour
{
    private IEnumerator Exist_Time;

    public void Fail()
    {
        Exist_Time = Visible_Time(0.5f);
        StartCoroutine(Exist_Time);
    }

    private IEnumerator Visible_Time(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        this.gameObject.SetActive(false);
    }
}
