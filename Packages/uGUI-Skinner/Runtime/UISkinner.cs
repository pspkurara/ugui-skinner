using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Pspkurara.UI.Skinner;
using System;

namespace Pspkurara.UI
{

	[DisallowMultipleComponent]
	[AddComponentMenu("UI/Skinner")]
	public partial class UISkinner : UIBehaviour, ISkinStyleParent, ISerializationCallbackReceiver
	{

		[SerializeField] private int m_StyleIndex = 0;
		[SerializeField] private List<SkinStyle> m_Styles = null;
		private Lazy<Stack<ISkinStyleParent>> m_ApplyTrace = new Lazy<Stack<ISkinStyleParent>>();

		/// <summary>
		/// 全てのスキン
		/// </summary>
		public IReadOnlyList<SkinStyle> styles { get { return m_Styles; } }

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
			SetSkin(styleIndex, m_ApplyTrace.Value);
		}

		/// <summary>
		/// スキンを切り替える
		/// </summary>
		/// <param name="styleKey">スキンのスタイルキー</param>
		public void SetSkin(string styleKey)
		{
			SetSkin(styleKey, m_ApplyTrace.Value);
		}

		/// <summary>
		/// スキンを切り替える
		/// </summary>
		/// <param name="styleIndex">スキンのスタイル番号</param>
		/// <param name="applyTrace">呼び出し元のスキナー</param>
		internal void SetSkin(int styleIndex, Stack<ISkinStyleParent> applyTrace)
		{
			if (applyTrace.Contains(this)) return;
			m_StyleIndex = Mathf.Clamp(styleIndex, 0, Length);
			if (m_Styles.Count <= 0) return;
			applyTrace.Push(this);
			m_Styles[m_StyleIndex].Apply(applyTrace);
			applyTrace.Pop();
		}

		/// <summary>
		/// スキンを切り替える
		/// </summary>
		/// <param name="styleKey">スキンのスタイルキー</param>
		/// <param name="applyTrace">呼び出し元のスキナー</param>
		internal void SetSkin(string styleKey, Stack<ISkinStyleParent> applyTrace)
		{
			int index = m_Styles.FindIndex(s => s.styleKey == styleKey);
			if (index != -1)
			{
				SetSkin(index, applyTrace);
			}
		}

		/// <summary>
		/// スキンの長さ
		/// </summary>
		public int Length {
			get { return m_Styles.Count; }
		}

		/// <summary>
		/// スタイルキーを持つスキンスタイルのみを取得する
		/// </summary>
		/// <returns>スタイルキー持ちスキンスタイル (Key: <see cref="styles"/>におけるインデックス)</returns>
		public List<KeyValuePair<int, SkinStyle>> GetHasStyleKeyStyles()
		{
			var list = new List<KeyValuePair<int, SkinStyle>>();
			for (int i = 0; i < m_Styles.Count; i++)
			{
				if (string.IsNullOrEmpty(m_Styles[i].styleKey)) continue;
				list.Add(new KeyValuePair<int, SkinStyle>(i, m_Styles[i]));
			}
			return list;
		}

		/// <summary>
		/// 自身の全てのスキンスタイルの「親」を更新
		/// </summary>
		internal void SyncStyleParentWithLinkedSkinStyles()
		{
			foreach (var style in m_Styles)
			{
				style.SetStyleParent(this);
			}
		}

		/// <summary>
		/// スキンスタイルのインデックスを親のリストを元に取得する
		/// </summary>
		/// <param name="style">対象のスキンスタイル</param>
		/// <returns>インデックス</returns>
		int ISkinStyleParent.GetStyleIndexInParent(SkinStyle style)
		{
			return m_Styles.IndexOf(style);
		}

		void ISerializationCallbackReceiver.OnBeforeSerialize() { }

		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			SyncStyleParentWithLinkedSkinStyles();
		}

	}

}