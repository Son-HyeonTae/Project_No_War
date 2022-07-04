using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUseCamera : MonoBehaviour
{
    Camera CurrentUsingCam;
    Camera PrevUsedCam;

    // Start is called before the first frame update
    void Start()
    {
        PrevUsedCam = null;
        CurrentUsingCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeCamera(Camera camera)
    {
        if(CurrentUsingCam)
        {
            CurrentUsingCam.gameObject.SetActive(false);
            PrevUsedCam = CurrentUsingCam;
        }

        CurrentUsingCam = camera;
    }

}
