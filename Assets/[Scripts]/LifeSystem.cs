using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LifeSystem : MonoBehaviour
{
    public TMP_Text TMPText;
    public int LifeCount;

    // Update is called once per frame
    void Update()
    {
        if (TMPText != null)
        {
            TMPText.text = LifeCount.ToString();
        }
    }
}
