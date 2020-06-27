using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private Toggle m_MenuToggle;
	private float m_TimeScaleRef = 1f;
    private float m_VolumeRef = 1f;
    private bool m_Paused;

    public GameObject pauseUI;


    void Awake()
    {
        m_MenuToggle = GetComponent <Toggle> ();
	}

    private void Start()
    {
        pauseUI.SetActive(false);
    }


    public void MenuOn ()
    {
        m_TimeScaleRef = Time.timeScale;
        Time.timeScale = 0f;

        //m_VolumeRef = AudioListener.volume;
        //AudioListener.volume = 0f;
        AudioListener.pause = true;
        m_Paused = true;
        pauseUI.SetActive(true);
    }


    public void MenuOff ()
    {
        Time.timeScale = m_TimeScaleRef;
        //AudioListener.volume = m_VolumeRef;
        AudioListener.pause = false;
        m_Paused = false;
        pauseUI.SetActive(false);
    }


    public void OnMenuStatusChange ()
    {
        if (m_MenuToggle.isOn && !m_Paused)
        {
            MenuOn();
        }
        else if (!m_MenuToggle.isOn && m_Paused)
        {
            MenuOff();
        }
    }


#if !MOBILE_INPUT
	void Update()
	{
		if(Input.GetKeyUp(KeyCode.Escape))
		{
		    m_MenuToggle.isOn = !m_MenuToggle.isOn;
            Cursor.visible = m_MenuToggle.isOn;//force the cursor visible if anythign had hidden it
		}
	}
#endif

}
