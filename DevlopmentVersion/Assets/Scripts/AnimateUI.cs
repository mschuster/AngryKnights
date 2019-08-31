/**
 * AnimateUI class
 *
 * Simple class to show and hide Artist information.
 * 
 * Author: Martin Schuster
 */

using System.Collections;
using UnityEngine;

public class AnimateUI : MonoBehaviour
{
    public GameObject artistPanel;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowPanel());
    }

    private IEnumerator ShowPanel()
    {
        yield return new WaitForSeconds(1f);
        artistPanel.SetActive(true);
        StartCoroutine(HidePanel());
    }

    private IEnumerator HidePanel()
    {
        yield return new WaitForSeconds(10f);
        artistPanel.SetActive(false);
    }
}
