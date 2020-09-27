using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Pspkurara.UI.Skinner;

namespace Pspkurara.UI
{

	[DisallowMultipleComponent]
	[AddComponentMenu("UI/Skinner")]
	public partial class UISkinner : UIBehaviour
	{

		[SerializeField] private int m_StyleIndex = 0;
		[SerializeField] private List<SkinStyle> m_Styles = null;

		/// <summary>
		/// 全てのスキン
		/// </summary>
		public List<SkinStyle> styles { get { return m_Styles; } }

		/// <summary>
		/// 現在のスキンのスタイル番号
		/// </summary>
		public int styleIndex { get { return m_StyleIndex; } }

		/// <summary>
		/// スキンを切り替える
		/// </summary>
		/// <param name="styleIndex">スキンのスタイル番号</param>
		public void SetSkin(int styleIndex)
		{
			m_StyleIndex = Mathf.Clamp(styleIndex, 0, Length);
			if (m_Styles.Count <= 0) return;
			m_Styles[m_StyleIndex].Apply();
		}

		/// <summary>
		/// スキンを切り替える
		/// </summary>
		/// <param name="styleKey">スキンのスタイルキー</param>
		public void SetSkin(string styleKey)
		{
			int index = m_Styles.FindIndex(s => s.styleKey == styleKey);
			if (index != -1) SetSkin(index);
		}

		/// <summary>
		/// スキンの長さ
		/// </summary>
		public int Length {
			get { return m_Styles.Count; }
		}

	}

}