using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PolicyTreeScript : MonoBehaviour
{
    public TMPro.TMP_Text policyTimerText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject[] policies = GameObject.FindGameObjectsWithTag("Policy");
        PolicyBasicScript[] policyScripts = new PolicyBasicScript[policies.Length];
        for (int i = 0; i < policies.Length; i++)
        {
            policyScripts[i] = policies[i].GetComponent<PolicyBasicScript>();
        }
        Array.Sort(policyScripts);

        for (int i = 0; i < policies.Length; i++)
        {
            Debug.Log("GOT " + i + " POLICY!!");
            policyScripts[i].CheckIfChoseable();
        }
        ResetPolicyTimer();
    }

    public void ReturnToTown()
    {
        GameObject[] policies = GameObject.FindGameObjectsWithTag("Policy");
        for (int i = 0; i < policies.Length; i++)
        {
            PlayerStats.SavePolicy(policies[i].GetComponent<PolicyBasicScript>());
        }
        SceneManager.LoadScene("TownScene");
    }

    public void ResetPolicyTimer()
    {
        policyTimerText.SetText(PlayerStats.GetPolicyTimer().ToString());
    }
}
