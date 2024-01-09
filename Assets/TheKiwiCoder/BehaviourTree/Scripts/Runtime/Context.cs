using Cars;
using UnityEngine;
using UnityEngine.AI;

namespace TheKiwiCoder
{

	// The context is a shared object every node has access to.
	// Commonly used components and subsytems should be stored here
	// It will be somewhat specfic to your game exactly what to add here.
	// Feel free to extend this class 
	public class Context
	{
		public GameObject gameObject;
		public Transform transform;
		public Rigidbody body;
		public Pid_Controller pidCon;
		public AdvancedCarController carController;
		// Add other game specific systems here

		public static Context CreateFromGameObject(GameObject gameObject)
		{
			// Fetch all commonly used components
			Context context = new Context();
			context.gameObject = gameObject;
			context.transform = gameObject.transform;
			context.body = gameObject.GetComponent<Rigidbody>();
			context.carController = gameObject.GetComponent<AdvancedCarController>();
			context.pidCon = gameObject.GetComponent<Pid_Controller>();

			// Add whatever else you need here...

			return context;
		}
	}
}