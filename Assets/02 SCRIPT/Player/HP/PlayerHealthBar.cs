using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] PlayerHealth playerHealth;

    [SerializeField] Image totalHealBar;
    [SerializeField] Image currentHealBar;
    private void Start()
    {
        totalHealBar.fillAmount = playerHealth.currentHealth / 10;
    }
    private void Update()
    {
        currentHealBar.fillAmount = playerHealth.currentHealth / 10; 
        // hien thi mau trong khoang min tu 0 den max la 1 theo kieu float
    }

}
