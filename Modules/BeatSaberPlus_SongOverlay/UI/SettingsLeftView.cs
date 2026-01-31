using CP_SDK.XUI;
using UnityEngine;

namespace BeatSaberPlus_SongOverlay.UI
{
    /// <summary>
    /// Settings left view
    /// </summary>
    internal sealed class SettingsLeftView : CP_SDK.UI.ViewController<SettingsLeftView>
    {
        /// <summary>
        /// On view creation
        /// </summary>
        protected override sealed void OnViewCreation()
        {
            Templates.FullRectLayout(
                Templates.TitleBar("Інформація"),

                Templates.ScrollableInfos(55,
                    XUIText.Make($"Цей модуль надає сервер для веб‑оверлеїв пісень!")
                        .SetAlign(TMPro.TextAlignmentOptions.Left)
                ),

                Templates.ExpandedButtonsLine(
                    XUIPrimaryButton.Make("Документація", OnDocumentationButton)
                )
            )
            .SetBackground(true, null, true)
            .BuildUI(transform);
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Documentation button
        /// </summary>
        private void OnDocumentationButton()
        {
            ShowMessageModal("URL відкрито у вашому браузері.");
            Application.OpenURL(SongOverlay.Instance.DocumentationURL);
        }
    }
}
