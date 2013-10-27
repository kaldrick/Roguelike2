using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;

public class AIController : MonoBehaviour {
	public Transform target;
	/** How often to search for a new path */
	public float repathRate = 0.1F;
	
	/** The minimum distance to a waypoint to consider it as "reached" */
	public float pickNextWaypointDistance = 1F;
	
	/** The minimum distance to the end point of a path to consider it "reached" (multiplied with #pickNextWaypointDistance).
	 * This value is multiplied with #pickNextWaypointDistance before it is used. Recommended range [0...1] */
	public float targetReached = 0.2F;
	
	/** Units per second */
	public float speed2 = 5;
	
	/** How fast the AI can turn around */
	public float rotationSpeed = 1;
	
	public bool drawGizmos = false;
	
	/** Should paths be searched for.
	 * Setting this to false will make the AI not search for paths anymore, can save some CPU cycles.
	 * It will check every #repathRate seconds if it should start to search for paths again.
	 * \note It will not cancel paths which are currently being calculated */
	public bool canSearch = true;
	
	/** Can it move. Enables or disables movement and rotation */
	public bool canMove = true;
	
	/** Seeker component which handles pathfinding calls */
	protected Seeker seeker;
	
	/** CharacterController which handles movement */
	protected CharacterController controller;
	
	/** NavmeshController which handles movement if not null*/
	protected NavmeshController navmeshController;
	
	
	/** Transform, cached because of performance */
	protected Transform tr;
	
	protected float lastPathSearch = -9999;
	
	protected int pathIndex = 0;
	
	/** This is the path the AI is currently following */
	protected Vector3[] path2;
	//The point to move to
    public Vector3 targetPosition;
//    private Seeker seeker;
   // private CharacterController controller;
	public Vector3 prevPosition = new Vector3(0,0,0);
	public bool bShowMagnitude;
    //The calculated path
    public Path path;
    //The AI's speed per second
    public float speed = 100;
    
    //The max distance from the AI to a waypoint for it to continue to the next waypoint
    public float nextWaypointDistance = 6;
 	public bool bChangeWaypoint = true;
    //The waypoint we are currently moving towards
    private int currentWaypoint = 0;
 	private float fTimeToChangeWaypoint = 0.0f;
	public DungeonGenerator Dungeon;
	private int roomIndex;
	
	private GameObject player;
	private RoomXYContainer roomXY;
	private float fRedTime = 0.0f;
	// List of rooms
	public List<Room> rooms;
	public bool bHurt = false;
	public int HP = 60;
	
	
	
    public void Start () {
        seeker = GetComponent<Seeker>();
        controller = GetComponent<CharacterController>();
        //Start a new path to the targetPosition, return the result to the OnPathComplete function
       // seeker.StartPath (transform.position,targetPosition, OnPathComplete);
		tr = transform;
		Repath ();
    }
	/** Will make the AI give up it's current path and stop completely. */
	public void Reset () {
		path2 = null;
	}
    public void Awake()
	{
		roomXY = GameObject.Find ("RoomXYContainer").GetComponent<RoomXYContainer>();
		player = GameObject.FindGameObjectWithTag("Player");
	}
    public void OnPathComplete (Path p) {
		if(target == null)
		{
			p.Claim (this);
	      //  Debug.Log ("Yey, we got a path back. Did it have an error? "+p.error);
	        if (!p.error) {
				if (path != null) path.Release (this);
	            path = p;
	            //Reset the waypoint counter
	            currentWaypoint = 0;
	        }
			else {
	            p.Release (this);
				bChangeWaypoint = true;
	            Debug.Log ("Oh noes, the target was not reachable: "+p.errorLog);
	        }
		}
		else
		{
			/*if (Time.time-lastPathSearch >= repathRate) {
			Repath ();
			} else {*/
				StartCoroutine (WaitToRepath ());
			//}
			p.Claim (this);
			//If the path didn't succeed, don't proceed
			if (p.error) {
				p.Release (this);
				return;
			}
			else
			{
				//if(path != null) path.Release (this);
				//Get the calculated path as a Vector3 array
				path2 = p.vectorPath.ToArray();
				
				//Find the segment in the path which is closest to the AI
				//If a closer segment hasn't been found in '6' iterations, break because it is unlikely to find any closer ones then
				float minDist = Mathf.Infinity;
				int notCloserHits = 0;
				
				for (int i=0;i<path2.Length-1;i++) {
					float dist = Mathfx.DistancePointSegmentStrict (path2[i],path2[i+1],tr.position);
					if (dist < minDist) {
						notCloserHits = 0;
						minDist = dist;
						pathIndex = i+1;
					} else if (notCloserHits > 6) {
						break;
					}
				}
			}
		}
		
		
    } 
	/** Waits the remaining time until the AI should issue a new path request.
	 * The remaining time is defined by Time.time - lastPathSearch */
	public IEnumerator WaitToRepath () {
		float timeLeft = repathRate - (Time.time-lastPathSearch);
		
		yield return new WaitForSeconds (timeLeft);
		Repath ();
	}
	
	/** Stops the AI.
	 * Also stops new search queries from being made
	 * \since Before 3.0.8 This does not prevent new path calls from making the AI move again
	 * \see #Resume
	 * \see #canMove
	 * \see #canSearch */
	public void Stop () {
		canMove = false;
		canSearch = false;
	}
	
	/** Resumes walking and path searching the AI.
	 * \since Added in 3.0.8
	 * \see #Stop
	 * \see #canMove
	 * \see #canSearch */
	public void Resume () {
		canMove = true;
		canSearch = true;
	}
	/** Recalculates the path to #target.
	  * Queries a path request to the Seeker, the path will not be calculated instantly, but will be put on a queue and calculated as fast as possible.
	  * It will wait if the current path request by this seeker has not been completed yet.
	  * \see Seeker.IsDone */
	public virtual void Repath () {
		lastPathSearch = Time.time;
		
		if (seeker == null || target == null || !canSearch || !seeker.IsDone ()) {
			StartCoroutine (WaitToRepath ());
			return;
		}
		
		//for (int i=0;i<1000;i++) {
			//MultithreadPath mp = new MultithreadPath (transform.position,target.position,null);
			//Path p = new Path (transform.position,target.position,null);
			//	AstarPath.StartPath (mp);
		//}
		//Debug.Log (AstarPath.pathQueue.Count);
		
		//StartCoroutine (WaitToRepath ());
		/*ConstantPath cpath = new ConstantPath(transform.position,null);
		//Must be set to avoid it from searching towards Vector3.zero
		cpath.heuristic = Heuristic.None;
		//Here you set how far it should search
		cpath.maxGScore = 2000;
		AstarPath.StartPath (cpath);*/
		//FloodPathTracer fpathTrace = new FloodPathTracer (transform.position,fpath,null);
		//seeker.StartPath (fpathTrace,OnPathComplete);
		
		Path p = ABPath.Construct(transform.position,target.position,null);
		seeker.StartPath (p,OnPathComplete);
		//Start a new path from transform.positon to target.position, return the result to the function OnPathComplete
		//seeker.StartPath (transform.position,target.position,OnPathComplete);
	}
	
	/** Start a new path moving to \a targetPoint */
	public void PathToTarget (Vector3 targetPoint) {
		lastPathSearch = Time.time;
		
		if (seeker == null) {
			return;
		}
		
		//Start a new path from transform.positon to target.position, return the result to OnPathComplete
		seeker.StartPath (transform.position,targetPoint,OnPathComplete);
	}
	
	/** Called when the AI reached the end of path.
	 * This will be called once for every path it completes, so if you have a really fast repath rate it will call this function often if when it stands on the end point.
	 */
	public virtual void ReachedEndOfPath () {
		//The AI has reached the end of the path
	}
	
 	public void Update()
	{
		fTimeToChangeWaypoint += Time.deltaTime;
		if(target == null)
		{
			if(fTimeToChangeWaypoint > 3.0f)
			{
				if(prevPosition == new Vector3(0,0,0))
				{
					prevPosition = transform.position;
				}
				else
				{
					if((transform.position.magnitude - prevPosition.magnitude) > 0.1 || (transform.position.magnitude - prevPosition.magnitude) < -0.1)
					{
						prevPosition = transform.position;	
					}
					else
					{
						Debug.Log ("Zmieniam!" + (transform.position.magnitude - prevPosition.magnitude));
						bChangeWaypoint = true;
					}
				}
					fTimeToChangeWaypoint = 0.0f;
			}
			//Debug.Log (Vector3.Distance(player.transform.position, transform.position));
			if(Vector3.Distance(player.transform.position, transform.position) < 5)
			{
				target = player.transform;
				
			}
			if(bChangeWaypoint)
			{
				roomIndex = Random.Range (0, roomXY.i);
				targetPosition = new Vector3(roomXY.X[roomIndex], 1.5f, roomXY.Y[roomIndex]);
				seeker.StartPath (transform.position,targetPosition, OnPathComplete);
				/*Random.seed = Random.Range (0, 65000);
				targetPosition = transform.position + new Vector3(Random.Range (0, 12.0f),0,Random.Range (0, 12.0f));
				Random.seed = Random.Range (0, 65000);
				targetPosition = transform.position - new Vector3(Random.Range (0, 12.0f),0,Random.Range (0, 12.0f));*/
				currentWaypoint = 0;
				bChangeWaypoint = false;
				//Debug.Log ("czy w tym momencie zaczynam sie ciac?" + name + path.vectorPath.Count + " currwayp " + currentWaypoint + " " + bChangeWaypoint);
			}
			if (path == null) {
	            //We have no path to move after yet
	            return;
	        }
	        if (currentWaypoint > path.vectorPath.Count) return; 
	        if (currentWaypoint == path.vectorPath.Count) {
	          //  Debug.Log ("End Of Path Reached");
				//Debug.Log ("Dotarlem do celu" + path.vectorPath.Count + " currwayp " + currentWaypoint + " " + bChangeWaypoint);
				bChangeWaypoint = true;
	            return;
	        }
	        
	        //Direction to the next waypoint
	        Vector3 dir = (path.vectorPath[currentWaypoint]-transform.position).normalized;
	        dir *= speed * Time.fixedDeltaTime;
	        controller.SimpleMove (dir);
	        
	        //Check if we are close enough to the next waypoint
	        //If we are, proceed to follow the next waypoint
	       if ( (transform.position-path.vectorPath[currentWaypoint]).sqrMagnitude < nextWaypointDistance*nextWaypointDistance) {
	            currentWaypoint++;
	            return;
	        }
		}
		else
		{
			if (path2 == null || pathIndex >= path2.Length || pathIndex < 0 || !canMove) {
			return;
			}
			//Change target to the next waypoint if the current one is close enough
			Vector3 currentWaypoint = path2[pathIndex];
			currentWaypoint.y = tr.position.y;
			while ((currentWaypoint - tr.position).sqrMagnitude < pickNextWaypointDistance*pickNextWaypointDistance) {
				pathIndex++;
				if (pathIndex >= path2.Length) {
					//Use a lower pickNextWaypointDistance for the last point. If it isn't that close, then decrement the pathIndex to the previous value and break the loop
					if ((currentWaypoint - tr.position).sqrMagnitude < (pickNextWaypointDistance*targetReached)*(pickNextWaypointDistance*targetReached)) {
						ReachedEndOfPath ();
						return;
					} else {
						pathIndex--;
						//Break the loop, otherwise it will try to check for the last point in an infinite loop
						break;
					}
				}
				currentWaypoint = path2[pathIndex];
				currentWaypoint.y = tr.position.y;
			}
			
			
			Vector3 dir2 = currentWaypoint - tr.position;
			
			// Rotate towards the target
			tr.rotation = Quaternion.Slerp (tr.rotation, Quaternion.LookRotation(dir2), rotationSpeed * Time.deltaTime);
			tr.eulerAngles = new Vector3(0, tr.eulerAngles.y, 0);
			
			Vector3 forwardDir = transform.forward;
			//Move Forwards - forwardDir is already normalized
			forwardDir = forwardDir * speed2;
			forwardDir *= Mathf.Clamp01 (Vector3.Dot (dir2.normalized, tr.forward));
			
			if (navmeshController != null) {
				navmeshController.SimpleMove (tr.position,forwardDir);
			} else if (controller != null) {
				controller.SimpleMove (forwardDir);
			} else {
				transform.Translate (forwardDir*Time.deltaTime, Space.World);
			}
			//Debug.Log ("Mam gracza i odleglosc: " + Vector3.Distance(player.transform.position, transform.position));
			if(Vector3.Distance(player.transform.position, transform.position) > 10)
			{
				Debug.Log ("dzialam?");
				Reset ();
				bChangeWaypoint = true;
				target = null;
			}
		}
		
		if(bHurt)
		{
			fRedTime += Time.deltaTime;
			if(fRedTime < 0.5f)
			{
				GetComponentInChildren<Renderer>().material.color = Color.red;
			}
			else
			{
				GetComponentInChildren<Renderer>().material.color = Color.white;
				fRedTime = 0.0f;
				bHurt = false;
			}
		}
		
		if(HP<=0)
		{
			gameObject.SetActive (false);	
		}
	}
    public void FixedUpdate () {
        
    }
} 