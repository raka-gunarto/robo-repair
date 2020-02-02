using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigRobotWinChecker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameObject.Find("WinText").GetComponent<Text>().text = other.gameObject.name + " wins!";
        GameObject.Find("WinText").GetComponent<Text>().enabled = true;
    }
}
