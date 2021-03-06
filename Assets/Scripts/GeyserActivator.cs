using UnityEngine;

public class GeyserActivator : MonoBehaviour
{
    [SerializeField]
    private Geyser geyser;

    private bool touched;

    private bool activated;

    private SwitchRenderer switchRenderer;

    private AudioSource audioSource;


    private void Awake()
    {
        switchRenderer = GetComponent<SwitchRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        bool pressed = Input.GetKey(KeyCode.E);

        if (!activated && touched && pressed)
            Activate();
        else if (activated && !(touched && pressed))
            Deactivate();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsPlayer(collision.gameObject))
        {
            touched = true;
        }

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (IsPlayer(collision.gameObject))
            touched = false;
    }

    private bool IsPlayer(GameObject obj)
    {
        return obj.CompareTag("Player");
    }

    private void Activate()
    {
        if (!activated)
        {
            audioSource.Play();
        }
        activated = true;

        geyser.Activate();

        switchRenderer.SwitchOn();
    }

    private void Deactivate()
    {
        activated = false;
        geyser.Deactivate();
        switchRenderer.SwitchOff();
    }
}
