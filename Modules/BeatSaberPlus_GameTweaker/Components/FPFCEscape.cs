using BGLib.Polyglot;
using System.Linq;
using TMPro;
using UnityEngine;
using System.Reflection;

namespace BeatSaberPlus_GameTweaker.Components
{
    /// <summary>
    /// FPFC Escape component
    /// </summary>
    internal class FPFCEscape : MonoBehaviour
    {
        /// <summary>
        /// Pause menu manager instance
        /// </summary>
        private PauseMenuManager m_IPauseMenuManager;
        private PauseMenuManager m_PauseMenuManager
        {
            get
            {
                if (!m_IPauseMenuManager)
                    m_IPauseMenuManager = Resources.FindObjectsOfTypeAll<PauseMenuManager>().FirstOrDefault();

                return m_IPauseMenuManager;
            }
        }
        /// <summary>
        /// Pause controller instance
        /// </summary>
        private PauseController m_IPauseController;
        private PauseController m_PauseController
        {
            get
            {
                if (!m_IPauseController)
                    m_IPauseController = Resources.FindObjectsOfTypeAll<PauseController>().FirstOrDefault();

                return m_IPauseController;
            }
        }
        /// <summary>
        /// Is an FPFC pause requested
        /// </summary>
        private bool m_FPFCPause = false;
        /// <summary>
        /// Back button text
        /// </summary>
        private string m_BackButtonText = "";

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Called every frame if the script is enabled.
        /// </summary>
        private void Update()
        {
            /// Don't activate in menu
            if (CP_SDK_BS.Game.Logic.ActiveScene != CP_SDK_BS.Game.Logic.ESceneType.Playing
                || CP_SDK_BS.Game.Logic.LevelData?.Type == CP_SDK_BS.Game.LevelType.Multiplayer)
                return;

            /// Wait for components
            if (m_PauseController == null || m_PauseMenuManager == null)
                return;

            try
            {
                var l_BackButtonTransform      = GetPauseMenuButtonTransform(m_PauseMenuManager, "_backButton", "backButton");
                var l_RestartButtonTransform   = GetPauseMenuButtonTransform(m_PauseMenuManager, "_restartButton", "restartButton");
                var l_ContinueButtonTransform  = GetPauseMenuButtonTransform(m_PauseMenuManager, "_continueButton", "continueButton");

                if (l_BackButtonTransform == null || l_RestartButtonTransform == null || l_ContinueButtonTransform == null)
                    return;

                var l_BackLocalized     = GetLocalizedLabel(l_BackButtonTransform);
                var l_RestartLocalized  = GetLocalizedLabel(l_RestartButtonTransform);
                var l_ContinueLocalized = GetLocalizedLabel(l_ContinueButtonTransform);

                var l_BackText     = GetTextLabel(l_BackButtonTransform);
                var l_RestartText  = GetTextLabel(l_RestartButtonTransform);
                var l_ContinueText = GetTextLabel(l_ContinueButtonTransform);

                /// Restore in case of not FPFC pause
                if (m_FPFCPause && !m_PauseMenuManager.enabled)
                {
                    if (l_BackLocalized != null)     l_BackLocalized.enabled = true;
                    if (l_RestartLocalized != null)  l_RestartLocalized.enabled = true;
                    if (l_ContinueLocalized != null) l_ContinueLocalized.enabled = true;

                    m_FPFCPause = false;
                }

                /// Is visible
                if (m_FPFCPause && m_PauseMenuManager.enabled)
                {
                    bool l_ShouldRestore = false;

                    /// Should resume
                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        TryCallPauseMenu(m_PauseMenuManager, "ContinueButtonPressed", "ContinueButtonWasPressed", "HandleContinueButtonPressed");
                        l_ShouldRestore = true;
                    }
                    /// Menu button
                    else if (Input.GetKeyDown(KeyCode.M))
                    {
                        TryCallPauseMenu(m_PauseMenuManager, "MenuButtonPressed", "MenuButtonWasPressed", "HandleMenuButtonPressed");
                        l_ShouldRestore = true;
                    }
                    /// Restart button
                    else if (Input.GetKeyDown(KeyCode.R))
                    {
                        TryCallPauseMenu(m_PauseMenuManager, "RestartButtonPressed", "RestartButtonWasPressed", "HandleRestartButtonPressed");
                        l_ShouldRestore = true;
                    }
                    /// Continue button
                    else if (Input.GetKeyDown(KeyCode.C))
                    {
                        TryCallPauseMenu(m_PauseMenuManager, "ContinueButtonPressed", "ContinueButtonWasPressed", "HandleContinueButtonPressed");
                        l_ShouldRestore = true;
                    }

                    if (l_ShouldRestore)
                    {
                        if (l_BackLocalized != null)     l_BackLocalized.enabled = true;
                        if (l_RestartLocalized != null)  l_RestartLocalized.enabled = true;
                        if (l_ContinueLocalized != null) l_ContinueLocalized.enabled = true;

                        m_FPFCPause = false;
                    }
                    else if (l_BackText != null)
                        l_BackText.text = m_BackButtonText;
                }
                /// Should pause
                else if (!m_PauseMenuManager.enabled && Input.GetKeyDown(KeyCode.Escape))
                {
                    /// Start pause
                    m_PauseController.Pause();
                    m_PauseMenuManager.ShowMenu();

                    if (l_BackLocalized != null)     l_BackLocalized.enabled = false;
                    if (l_RestartLocalized != null)  l_RestartLocalized.enabled = false;
                    if (l_ContinueLocalized != null) l_ContinueLocalized.enabled = false;

                    if (l_BackText == null || l_RestartText == null || l_ContinueText == null)
                        return;

                    /// Enable rich text
                    l_BackText.richText       = true;
                    l_RestartText.richText    = true;
                    l_ContinueText.richText   = true;

                    /// Update buttons
                    l_BackText.text       = "<color=\"red\"><b>[Key M]</b> <color=#FFFFFFBF>" + l_BackText.text;
                    l_RestartText.text    = "<color=\"red\"><b>[Key R]</b> <color=#FFFFFFBF>" + l_RestartText.text;
                    l_ContinueText.text   = "<color=\"red\"><b>[Key C]</b> <color=#FFFFFFBF>" + l_ContinueText.text;

                    m_BackButtonText = l_BackText.text;
                    m_FPFCPause      = true;
                }
            }
            catch (System.Exception)
            {

            }
        }

        private static Transform GetPauseMenuButtonTransform(PauseMenuManager manager, params string[] names)
        {
            if (!TryGetMember(manager, out Object l_ButtonObj, names))
                return null;

            if (l_ButtonObj is GameObject l_GameObject)
                return l_GameObject.transform;
            if (l_ButtonObj is Component l_Component)
                return l_Component.transform;

            return null;
        }

        private static LocalizedTextMeshProUGUI GetLocalizedLabel(Transform buttonTransform)
        {
            return buttonTransform != null ? buttonTransform.GetComponentInChildren<LocalizedTextMeshProUGUI>(true) : null;
        }

        private static TextMeshProUGUI GetTextLabel(Transform buttonTransform)
        {
            return buttonTransform != null ? buttonTransform.GetComponentInChildren<TextMeshProUGUI>(true) : null;
        }

        private static void TryCallPauseMenu(PauseMenuManager manager, params string[] names)
        {
            if (manager == null)
                return;

            var l_Type = manager.GetType();
            for (int l_I = 0; l_I < names.Length; ++l_I)
            {
                var l_Method = l_Type.GetMethod(names[l_I], BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (l_Method != null)
                {
                    l_Method.Invoke(manager, null);
                    return;
                }
            }
        }

        private static bool TryGetMember<T>(object obj, out T value, params string[] names)
        {
            value = default;
            if (obj == null)
                return false;

            var l_Type = obj.GetType();
            for (int l_I = 0; l_I < names.Length; ++l_I)
            {
                var l_Name = names[l_I];
                var l_Field = l_Type.GetField(l_Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (l_Field != null && l_Field.GetValue(obj) is T l_FieldValue)
                {
                    value = l_FieldValue;
                    return true;
                }

                var l_Prop = l_Type.GetProperty(l_Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (l_Prop != null && l_Prop.GetValue(obj) is T l_PropValue)
                {
                    value = l_PropValue;
                    return true;
                }
            }

            return false;
        }
    }
}
