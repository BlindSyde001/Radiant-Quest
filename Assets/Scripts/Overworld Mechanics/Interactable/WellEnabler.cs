using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WellEnabler : MonoBehaviour {

    [SerializeField] private GameObject wellBlocked;
    [SerializeField] private GameObject wellEnabled;

    public void SwitchWell() {
        wellBlocked.SetActive(false);
        wellEnabled.SetActive(true);
    }




}
