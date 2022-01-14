using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class EnemyAINav : MonoBehaviour
{

	public Transform[] points;
	private int destPoint = 0;
	private NavMeshAgent agent;
	public Transform player;
	public GameObject treasure;
	public bool playerDetected = false;
	public Transform retreatPoint;

	public GameObject playerChar;
	private PlayerManager playerScript;
	public GameObject self;


	void Start()
	{
		agent = GetComponent<NavMeshAgent>();

		// Disabling auto-braking allows for continuous movement
		// between points (ie, the agent doesn't slow down as it
		// approaches a destination point).
		agent.autoBraking = false;

		GotoNextPoint();
	}


	void GotoNextPoint()
	{
		// Returns if no points have been set up
		if (points.Length == 0)
			return;

		// Set the agent to go to the currently selected destination.
		agent.destination = points[destPoint].position;

		// Choose the next point in the array as the destination,
		// cycling to the start if necessary.
		destPoint = (destPoint + 1) % points.Length;
	}


	void Update()
	{

		playerScript = playerChar.GetComponent<PlayerManager>();
		// Choose the next destination point when the agent gets
		// close to the current one.
		if (!agent.pathPending && agent.remainingDistance < 0.5f)
			GotoNextPoint();

		if (playerDetected == true)
		{
			agent.destination = player.position;
		}

		if (playerScript.isFullPower == true)
		{
			self.GetComponent<DealDamage>().enabled = false;
		}

		else
		{
			//self.GetComponent<DealDamage>().enabled = true;
		}

		if(playerScript.isFullPower == true && playerDetected)
		{
			agent.destination = retreatPoint.position;
		}

		if(treasure.activeSelf == false && playerScript.isFullPower == false)
		{
			agent.destination = player.position;
		}
	}
}