using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MoveCore : MonoBehaviour
{
	public ePos currentPos;
	public HandleLandingPosition HandleLandingPos;

	private Player player;
	[SerializeField] private float moveSpeed = 0.5f; //how long it takes to move between spots
	private ePos startPos;
	private GameObject tempPos;
	private bool isPassGo = false;

	public void MovePlayer(int diceRoll)
	{
		player = PlayerManager.GetCurrentPlayer();

		startPos = currentPos;

		if (diceRoll == -99) //TODO needs implement
		{
			currentPos = ePos.JustVisiting;
			player.isInJail = true;
			Debug.Log("Is in Jail");
			PlayerManager.Instance.AdvanceToNextPlayer();
			//TODO needs coroutine to wait
			CanvasManager.Instance.HUD.EvaluateEndTurn();
			return;
		}

		currentPos += diceRoll;

		if (currentPos >= ePos.Terminator) //TODO needs change
		{
			isPassGo = true;
			currentPos -= ePos.Terminator;
		}

		StartCoroutine(MoveToSpot(diceRoll));
	}

	IEnumerator MoveToSpot(int _diceRoll)
	{
		CameraManager.SetCurrentCamera(eCameraPosition.center, player.PlayerPiece.transform);
		yield return new WaitForSeconds(1.0f);

		StartCoroutine(MovePlayerToSpot(isPassGo, _diceRoll));

		Debug.Log("Current Position: " + currentPos);
		yield return new WaitForSeconds(1.0f);
	}

	IEnumerator MovePlayerToSpot(bool _isPassGo, int _diceRoll)
	{
		PlayerManager pm = PlayerManager.Instance;

		ePos newPos = 0; //actual next spot to move into

		int totalSteps = currentPos - startPos;
		if (_isPassGo) //TODO handle passing Go
		{
			totalSteps = _diceRoll;
		}
		int currentStep = 0;

		while (currentStep < totalSteps)
		{
			yield return new WaitForSeconds(moveSpeed);
			currentStep++;
			newPos = (ePos)(((int)startPos + currentStep) % (int)ePos.Terminator);

			if (newPos == ePos.Go)
			{
				int passGoCashBonus = 200;
				player.AdjustPlayerMoney(passGoCashBonus);
			}
			if (newPos == ePos.GoToJail)
			{
				//TODO 29min into the Panopto Video
			}

			Transform spotToMoveFrom = Board.Instance.Spots[(int)startPos].transform;
			Transform spotToMoveTo = Board.Instance.Spots[(int)newPos].transform;
			//TODO optional
			//player.PlayerPiece.transform.SetPositionAndRotation(spotToMoveTo.position, spotToMoveTo.rotation);
			//player.PlayerPiece.transform.Translate(player.PlayerPiece.transform.right * (pm.Offsets[player.PlayerIndex] * pm.OffsetMultiplier), Space.World);
			StartCoroutine(SmoothMove((int)newPos));
		}
		yield return new WaitForSeconds(0.5f);

		HandleLandingPos.InitHandleLandingPosition(player, this);
		HandleLandingPos.SetLandingSpot();
	}

	IEnumerator SmoothMove(int _pos)
	{
		if (tempPos == null) tempPos = new GameObject("TempPos");
		tempPos.transform.position = player.PlayerPiece.transform.position;

		PlayerManager pm = PlayerManager.Instance;
		Transform t = Board.Instance.Spots[_pos].transform;

		tempPos.transform.SetPositionAndRotation(t.position, t.rotation);
		tempPos.transform.Translate(tempPos.transform.right * (pm.Offsets[player.PlayerIndex] * pm.OffsetMultiplier), Space.World);

		Vector3 startPos = player.PlayerPiece.transform.position;
		Quaternion startRot = player.PlayerPiece.transform.rotation;
		Vector3 finalPos = tempPos.transform.position;
		Quaternion finalRot = t.rotation;

		float i = 0f;
		float duration = moveSpeed;

		while (i < 1f)
		{
			i += Time.deltaTime / duration;

			player.PlayerPiece.transform.position = Vector3.Lerp(startPos, finalPos, i);
			player.PlayerPiece.transform.rotation = Quaternion.Slerp(startRot, finalRot, i);
			yield return null;
		}

		player.PlayerPiece.transform.position = finalPos;
		player.PlayerPiece.transform.rotation = finalRot;
	}
}
