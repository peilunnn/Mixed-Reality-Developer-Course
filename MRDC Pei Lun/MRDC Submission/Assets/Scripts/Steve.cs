using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

public class Steve : MonoBehaviour, IMixedRealitySpeechHandler
{
    public Material SteveHeadMat;
    public bool SteveCreated = false;
    public int damping = 2;
    public Transform target;
    public Vector3 lookPos;


    // Start is called before the first frame update
    void Start()
    {
        CoreServices.InputSystem.RegisterHandler<IMixedRealitySpeechHandler>(this);
    }

    private void OnDestroy()
    {
        // Introduction to Voice: Exercise 4.6
        /************************************************************/
        // Unregister this component's IMixedRealitySpeechHandler with the Input System
        CoreServices.InputSystem.UnregisterHandler<IMixedRealitySpeechHandler>(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (SteveCreated)
        {
            lookPos = GameObject.Find("Main Camera").GetComponent<Transform>().position - GameObject.Find("SteveHead").GetComponent<Transform>().position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            GameObject.Find("SteveHead").GetComponent<Transform>().rotation = Quaternion.Slerp(GameObject.Find("SteveHead").GetComponent<Transform>().rotation, rotation, Time.deltaTime * damping);
            GameObject.Find("SteveHead").GetComponent<Transform>().position = Vector3.MoveTowards(GameObject.Find("SteveHead").GetComponent<Transform>().position, GameObject.Find("Main Camera").GetComponent<Transform>().position + new Vector3 (3,0,3), .005f);
        }
    }

    public void OnSpeechKeywordRecognized(SpeechEventData eventData)
    {
        if (eventData.Command.Keyword == "Create Steve")
        {
            SteveCreated = true;
            GameObject SteveHead = GameObject.CreatePrimitive(PrimitiveType.Cube);
            SteveHead.name = "SteveHead";
            SteveHead.GetComponent<Renderer>().material = SteveHeadMat;
            SteveHead.GetComponent<Transform>().position = new Vector3(0, 0.5f, 6);
            SteveHead.GetComponent<Transform>().eulerAngles = new Vector3(0, 0, 180);
        }

        if (eventData.Command.Keyword == "Destroy Steve")
        {
            SteveCreated = false;
            Destroy(GameObject.Find("SteveHead"));
            gameObject.GetComponent<AudioSource>().Play();
        }
    }

}
