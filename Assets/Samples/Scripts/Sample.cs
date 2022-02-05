using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pspkurara.UI.Skinner;

namespace Pspkurara.UI.Skinner.Sample
{

	public class Sample : MonoBehaviour
	{

		[SerializeField]
		private UISkinner m_Skinner = null;

		[SerializeField]
		private Button m_SetNormal = null;

		[SerializeField]
		private Button m_SetColored = null;

		private void Start()
		{
			m_SetNormal.onClick.AddListener(() =>
			{
				m_Skinner.SetSkin("Normal");
			});
			m_SetColored.onClick.AddListener(() =>
			{
				m_Skinner.SetSkin("Colored");
			});
		}

	}

}
