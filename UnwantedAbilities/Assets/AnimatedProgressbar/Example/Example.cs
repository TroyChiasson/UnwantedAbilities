using System.Collections;
using UnityEngine;

public class Example : MonoBehaviour
{

	[SerializeField] private AnimatedProgressbar	m_progressbarUI	= null	;

	private float m_fillAmount;
	public GameObject player;
	public double stamina;
    private IEnumerator Start()
	{

        player = GameObject.FindWithTag("Player");
		
		stamina = player.GetComponent<double>();
        print(stamina);
        while ( m_fillAmount < 1 )
		{
			print(stamina);
			m_fillAmount = (float)stamina/100;
			m_progressbarUI.FillAmount = m_fillAmount;
			yield return null;
		}
	}
}
