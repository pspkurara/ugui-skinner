using System.Collections.Generic;
using UnityEngine;

namespace Pspkurara.UI.Skinner
{

	/// <summary>
	/// 見た目の大本データ
	/// <see cref="SkinParts"/>を統合管理する
	/// </summary>
	[System.Serializable]
	public sealed class SkinStyle
	{

		#region 変数

		[SerializeField] private string m_StyleKey = string.Empty;
		[SerializeField] private List<SkinParts> m_Parts;

		#endregion

		#region プロパティ

		/// <summary>
		/// スタイルを決める各要素
		/// </summary>
		public List<SkinParts> parts { get { return m_Parts; } }

		/// <summary>
		/// スタイルの文字列キー
		/// </summary>
		public string styleKey { get { return m_StyleKey; } }

		#endregion

		#region コンストラクタ

		public SkinStyle()
		{
			m_Parts = new List<SkinParts>();
		}

		public SkinStyle(SkinStyle baseObject) : this()
		{
			foreach (SkinParts p in baseObject.m_Parts)
			{
				m_Parts.Add(p.Clone());
			}
		}

		public SkinStyle(int initCount) : this()
		{
			for (int i = 0; i < initCount; i++)
			{
				m_Parts.Add(new SkinParts());
			}
		}

		#endregion

		#region メソッド

		/// <summary>
		/// 見た目を反映
		/// </summary>
		public void Apply()
		{
			foreach (SkinParts cParts in m_Parts)
			{
				cParts.Apply();
			}
		}

		/// <summary>
		/// インスペクター変更時コールバック
		/// </summary>
		[System.Diagnostics.Conditional("UNITY_EDITOR")]
		internal void OnValidate()
		{
			foreach (SkinParts cParts in m_Parts)
			{
				cParts.OnValidate();
			}
		}

		#endregion

	}

}
