using UnityEngine;
using System.Collections;

public class CoRoutiner : MonoBehaviour {

	public IEnumerator DoCoroutine(IEnumerator cor)
	{
		while (cor.MoveNext())
			yield return cor.Current;
	}
}
