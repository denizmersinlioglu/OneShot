using UnityEngine;
using UnityEngine.EventSystems;

public class LauncherController : MonoBehaviour
{

	[SerializeField] private LauncherDragController launcher;

	private LauncherDragController launcherInstance;
	
	private void PointerDrag (PointerEventData eventData)
	{
		launcherInstance.drag();
	}


	private void PointerDown (PointerEventData eventData)
	{
		launcherInstance.pointerDown();
	}
	

	private void PointerUp (PointerEventData eventData)
	{
		launcherInstance.pointerUp();
	}

	private void Awake()
	{
		launcherInstance = Instantiate(launcher, Vector3.zero, Quaternion.identity);
		var eventTrigger = GetComponent<EventTrigger> ();
		eventTrigger.AddListener (EventTriggerType.PointerDown, PointerDown);
		eventTrigger.AddListener (EventTriggerType.PointerUp, PointerUp);
		eventTrigger.AddListener(EventTriggerType.Drag, PointerDrag);
	}
}

public static class ExtensionMethods
{
	public static void AddListener (this EventTrigger trigger, EventTriggerType eventType, System.Action<PointerEventData> listener)
	{
		var entry = new EventTrigger.Entry {eventID = eventType};
		entry.callback.AddListener(data => listener.Invoke((PointerEventData)data));
		trigger.triggers.Add(entry);
	}
}
