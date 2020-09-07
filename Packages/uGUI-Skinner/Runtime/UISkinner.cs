using System.Collections.Generic;
using UnityEngine;
using Pspkurara.UI.Skinner;

namespace Pspkurara.UI
{

	[DisallowMultipleComponent]
	[AddComponentMenu("UI/Skinner")]
	public partial class UISkinner
	{

		[SerializeField] private int m_StyleIndex = 0;
		[SerializeField] private List<SkinStyle> m_Styles = null;

		public List<SkinStyle> styles { get { return m_Styles; } }

		public int styleIndex { get { return m_StyleIndex; } }
		
		public void SetSkin(int styleIndex)
		{
			m_StyleIndex = Mathf.Clamp(styleIndex, 0, Length);
			if (m_Styles.Count <= 0) return;
			m_Styles[m_StyleIndex].Apply();
		}

		public void SetSkin(string styleKey)
		{
			int index = m_Styles.FindIndex(s => s.styleKey == styleKey);
			if (index != -1) SetSkin(index);
		}

		public int Length {
			get { return m_Styles.Count; }
		}

	}

}