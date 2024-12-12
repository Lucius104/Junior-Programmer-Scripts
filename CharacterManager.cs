using UnityEngine;
using System;
using System.Collections;


public class CharacterManager : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private string playerName;
    [Range(1,1000)] public float maxHealth;
    [Range(1, 1000)] public float maxMana;
    [Range(1, 100)] public float damage;
    [Range(1, 100)] public float defense;
    public enum Status {normal, poison, sap, paralize, magicSeal, attackUp, attackDn, defUp, defDn, bleed, dead};
    [SerializeField] private Status status;
    public float intStatus;

    public float currentHealth;
    public float currentMana;
    
    [Header("References")]
    [SerializeField] private Animator animator;

    private void Awake()
    {
        currentHealth = maxHealth;

        if (TryGetComponent(out animator))
        {
            animator = GetComponent<Animator>();
        }
        else
        {
            animator = null;
        }
        
    }
    void StatusEffects()
    {

        intStatus = Convert.ToInt32(status);
        switch (status)
        {
            case Status.normal:
                {
                    break;
                }
            case Status.poison:
                {
                    StartCoroutine(StatusTimer(30.0f));
                    break;
                }
            case Status.sap:
                {
                    StartCoroutine(StatusTimer(20.0f));
                    break;
                }
            case Status.paralize:
                {
                    StartCoroutine(StatusTimer(5.0f));
                    break;
                }
            case Status.magicSeal:
                {
                    StartCoroutine(StatusTimer(20.0f));
                    break;
                }
            case Status.attackUp:
                {
                    StartCoroutine(StatusTimer(20.0f));
                    break;
                }
            case Status.attackDn:
                {
                    StartCoroutine(StatusTimer(30.0f));
                    break;
                }
            case Status.defUp:
                {
                    StartCoroutine(StatusTimer(20.0f));
                    break;
                }
            case Status.defDn:
                {
                    StartCoroutine(StatusTimer(30.0f));
                    break;
                }
            case Status.bleed:
                {
                    StartCoroutine(StatusTimer(20.0f));
                    break;
                }
                case Status.dead: 
                {
                    break;
                }
}
    }
    private void Update()
    {
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            status = Status.dead;
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            currentHealth -= 10;
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            currentHealth += 10;
        }
    }
    

    IEnumerator StatusTimer(float timer)
    {
        yield return new WaitForSeconds (timer);
    }
}
