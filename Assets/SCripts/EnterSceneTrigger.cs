using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EnterSceneTrigger : MonoBehaviour
{
    public TextMeshProUGUI interactText;
    public string sceneName;

    [SerializeField] WorldState worldState;
    private bool canEnter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactText.gameObject.SetActive(true);
            canEnter = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactText.gameObject.SetActive(false);
            canEnter = false;
        }
    }

    private void Update()
    {
        if (canEnter && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(sceneName);
            worldState.bossDead = true;
        }
    }
}
