using UnityEngine;

public class AcceleratorDebug : MonoBehaviour
{
    public KeyCode m_AcceleratorKeyCode=KeyCode.RightControl;
#if UNITY_EDITOR
    private void Update() {
        if(Input.GetKey(m_AcceleratorKeyCode))
        {
            Debug.Log("key");
            Time.timeScale=10.0f;
        }
        else
            Time.timeScale=1.0f;
    }    
#endif
}