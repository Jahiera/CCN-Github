using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TestingBypass : MonoBehaviour
{
        void Update()
        {
            if (Keyboard.current.iKey.wasPressedThisFrame)
            {
                SceneManager.LoadScene("Level1");
            }
            
            if (Keyboard.current.oKey.wasPressedThisFrame)
            {
                SceneManager.LoadScene("Level2");
            }

            if (Keyboard.current.pKey.wasPressedThisFrame)
            {
                SceneManager.LoadScene("Level3");
            }
        }
}
