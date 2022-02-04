using System.Collections.Generic;

namespace Pspkurara.UI.Skinner
{

	/// <summary>
	/// GUIの要素のユニークIDを記録管理する
	/// </summary>
	internal sealed class GUIUniqueIdentifier
	{

		private List<int> m_NestedIdentifier = new List<int>();

		private List<int> m_LastRecordGuiId = new List<int>();

		/// <summary>
		/// 最後に触ったGUI番号が現在のGUI番号と一致しているか
		/// </summary>
		public bool isCurrentLastControlGuiId
		{
			get
			{
				if (m_LastRecordGuiId.Count == 0) return false;
				if (m_LastRecordGuiId.Count != m_NestedIdentifier.Count) return false;
				for (int i = 0; i < m_NestedIdentifier.Count; i++)
				{
					if (m_NestedIdentifier[i] != m_LastRecordGuiId[i])
					{
						return false;
					}
				}
				return true;
			}
		}

		public void ResetLastControlGuiId()
		{
			m_LastRecordGuiId.Clear();
		}

		public void Initialize()
		{
			m_NestedIdentifier.Clear();
			m_NestedIdentifier.Add(0);
		}

		/// <summary>
		/// ループネストを一段階潜る
		/// </summary>
		public void BeginNestedLoop()
		{
			m_NestedIdentifier.Add(0);
		}

		/// <summary>
		/// ループネストを一段階昇る
		/// </summary>
		public void EndNestedLoop()
		{
			if (m_NestedIdentifier.Count <= 1) return;
			m_NestedIdentifier.RemoveAt(m_NestedIdentifier.Count - 1);
		}

		/// <summary>
		/// GUI番号をカウントする
		/// </summary>
		public void Next()
		{
			m_NestedIdentifier[m_NestedIdentifier.Count - 1]++;
		}

		/// <summary>
		/// 最後に触ったGUI番号を記録する
		/// </summary>
		public void RecordLastGuiId()
		{
			m_LastRecordGuiId.Clear();
			m_LastRecordGuiId.AddRange(m_NestedIdentifier);
		}

	}

}
