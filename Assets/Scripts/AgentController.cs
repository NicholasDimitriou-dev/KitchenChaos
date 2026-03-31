using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class AgentController : MonoBehaviour
{
    public Transform destination;
    public Transform idleLocation;
    private float timer = 3f;
    private float maxTimer = 3f;
    public enum MouseButton
    {
        Left,
        Right
    }
    private NavMeshAgent agent;
    public MouseButton mouseButton;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0f)
        {
            agent.SetDestination(idleLocation.position);
            timer = maxTimer;
        }
        ButtonControl buttonControl = (mouseButton == MouseButton.Left) ? Mouse.current.leftButton : Mouse.current.rightButton;
        if (buttonControl.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.value);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                destination.position = hit.point;
                agent.SetDestination(destination.position);
            }
        }
    }
}