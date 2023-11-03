using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDoubleClick : MonoBehaviour
{
    private Button myButton;
    void Start()
    {
        myButton = GetComponent<Button>();
    }
    int count = 0;
    void Update()
    {
        if (myButton.animator.GetBool(3)==true) 
        {
            count = 1;
        }
        if(count == 1 && myButton.animator.GetBool(1) == true) {
            count = 0;
            myButton.animator.SetTrigger("DoubleClick");
        }
    }
}
