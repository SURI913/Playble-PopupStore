using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UISet : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(LoadUI());
    }
    IEnumerator LoadUI()
    {
        gameObject.SetActive(true);
        gameObject.GetComponent<Animator>().SetTrigger("Open");
        yield return new WaitForSeconds(2f);
        gameObject.GetComponent<Animator>().SetTrigger("Close");
        yield return new WaitForSeconds(0.3f);
        gameObject.SetActive(false);
        yield return null;
    }
}
