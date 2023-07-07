using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarBarController : MonoBehaviour
{
    Image currentStar;
    [SerializeField] float getStar;
    // Start is called before the first frame update
    void Start()
    {
        currentStar = this.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        currentStar.fillAmount = getStar*1/3;
    }
    public void AddStar(int add)
    {
        getStar = Mathf.Clamp(getStar + add, 0, 3);
    }
}
