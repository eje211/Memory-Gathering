using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBridge : MonoBehaviour
{
    public GameObject bridge;

    public void ShowBridge() {
        foreach (Transform block in transform) {
            if (!block.gameObject.activeSelf) {
                return;
            }
        }
        
        bridge.SetActive(true);
    }
}
