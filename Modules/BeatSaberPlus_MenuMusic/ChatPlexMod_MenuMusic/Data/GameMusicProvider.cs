using BeatSaberPlus_MenuMusic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace ChatPlexMod_MenuMusic.Data
{
    /// <summary>
    /// Game music provider
    /// </summary>
    public class GameMusicProvider : IMusicProvider
    {
        private List<Music> m_Musics    = new List<Music>();
        private bool        m_IsLoading = false;

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        public override MusicProviderType.E Type                => MusicProviderType.E.GameMusic;
        public override bool                IsReady             => !m_IsLoading;
#if BEATSABER
        public override bool                SupportPlayIt       => true;
        public override bool                SupportAddToQueue   => true;
#else
#error Missing game implementation
#endif
        public override List<Music>         Musics              => m_Musics;

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Constructor
        /// </summary>
        public GameMusicProvider()
        {
            m_IsLoading = true;
            CP_SDK.Unity.MTCoroutineStarter.Start(Coroutine_LoadGameSongs());
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Per game implementation of the Play It button
        /// </summary>
        /// <param name="music">Target music</param>
        /// <param name="viewController">Source view controller</param>
        public override void StartGameSpecificGamePlay(Music music, CP_SDK.UI.IViewController viewController)
        {
#if BEATSABER
            var l_BeatmapLevel = null as BeatmapLevel;
            try
            {
                if (music != null)
                {
                    var l_FullPath = music.GetSongPath();
                    if (l_FullPath.Contains("Beat Saber_Data/CustomLevels/"))
                    {
                        var l_RelativeFolder = l_FullPath.Substring(l_FullPath.IndexOf("Beat Saber_Data/CustomLevels/") + "Beat Saber_Data/CustomLevels/".Length);
                        if (l_RelativeFolder.Contains("/"))
                            l_RelativeFolder = l_RelativeFolder.Substring(0, l_RelativeFolder.LastIndexOf("/"));

                        l_BeatmapLevel = SongCore.Loader.CustomLevels.Where(x => x.Key.Contains(l_RelativeFolder)).Select(x => x.Value).FirstOrDefault();
                    }
                }
            }
            catch (System.Exception)
            {

            }

            if (l_BeatmapLevel == null)
            {
                CP_SDK.Unity.MTMainThreadInvoker.Enqueue(() =>
                {
                    if (viewController)
                        viewController.ShowMessageModal("Пісню не знайдено!");
                });

                return;
            }

            CP_SDK_BS.Game.LevelSelection.FilterToSpecificSong(l_BeatmapLevel);

            return;
#else
#error Missing game implementation
#endif
        }
        /// <summary>
        /// Per game implementation of the Add to queue button
        /// </summary>
        /// <param name="music">Target music</param>
        /// <param name="viewController">Source view controller</param>
        public override void AddToQueue(Music music, CP_SDK.UI.IViewController viewController)
        {
#if BEATSABER
            if (!ModulePresence.ChatRequest)
            {
                CP_SDK.Unity.MTMainThreadInvoker.Enqueue(() =>
                {
                    if (viewController)
                        viewController.ShowMessageModal("Модуль Chat Request не встановлено або вимкнено!");
                });
                return;
            }

            var l_BeatmapLevel = null as BeatmapLevel;
            try
            {
                if (music != null)
                {
                    var l_FullPath = music.GetSongPath();
                    if (l_FullPath.Contains("Beat Saber_Data/CustomLevels/"))
                    {
                        var l_RelativeFolder = l_FullPath.Substring(l_FullPath.IndexOf("Beat Saber_Data/CustomLevels/") + "Beat Saber_Data/CustomLevels/".Length);
                        if (l_RelativeFolder.Contains("/"))
                            l_RelativeFolder = l_RelativeFolder.Substring(0, l_RelativeFolder.LastIndexOf("/"));

                        l_BeatmapLevel = SongCore.Loader.CustomLevels.Where(x => x.Key.Contains(l_RelativeFolder)).Select(x => x.Value).FirstOrDefault();
                    }
                }
            }
            catch (System.Exception)
            {

            }

            if (l_BeatmapLevel == null)
            {
                CP_SDK.Unity.MTMainThreadInvoker.Enqueue(() =>
                {
                    if (viewController)
                        viewController.ShowMessageModal("Пісню не знайдено!");
                });

                return;
            }

            AddToChatRequestQueue(l_BeatmapLevel, viewController);
#else
#error Missing game implementation
#endif
        }
#if BEATSABER
        private bool AddToChatRequestQueue(BeatmapLevel beatmapLevel, CP_SDK.UI.IViewController viewController)
        {
            CP_SDK.Unity.MTMainThreadInvoker.Enqueue(() =>
            {
                if (viewController)
                    viewController.ShowLoadingModal();
            });

            BeatSaberPlus_ChatRequest.ChatRequest.Instance.AddToQueueFromBeatmapLevel(
                beatmapLevel:   beatmapLevel,
                requester:      null,
                onBehalfOf:     "$MenuMusic",
                forceNamePrefix:"🎵",
                asModAdd:       true,
                addToTop:       false,
                callback:       (p_Result) =>
                {
                    CP_SDK.Unity.MTMainThreadInvoker.Enqueue(() =>
                    {
                        if (!viewController)
                            return;

                        viewController.CloseLoadingModal();

                        switch (p_Result.Result)
                        {
                            case BeatSaberPlus_ChatRequest.Models.EAddToQueueResult.OK:
                                viewController.ShowMessageModal("Пісню додано до черги!");
                                break;

                            case BeatSaberPlus_ChatRequest.Models.EAddToQueueResult.NotFound:
                                viewController.ShowMessageModal("Пісню не знайдено на BeatSaver!");
                                break;

                            case BeatSaberPlus_ChatRequest.Models.EAddToQueueResult.AlreadyInQueue:
                                viewController.ShowMessageModal("Пісня вже в черзі!");
                                break;

                            default:
                                viewController.ShowMessageModal($"Error: {p_Result.Result.ToString()}");
                                break;
                        }
                    });
                }
            );

            return true;
        }
#endif
        /// <summary>
        /// Shuffle music collection
        /// </summary>
        public override void Shuffle()
        {
            for (var l_I = 0; l_I < m_Musics.Count; ++l_I)
            {
                var l_Swapped = m_Musics[l_I];
                var l_NewIndex = UnityEngine.Random.Range(l_I, m_Musics.Count);

                m_Musics[l_I] = m_Musics[l_NewIndex];
                m_Musics[l_NewIndex] = l_Swapped;
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Load game songs
        /// </summary>
        /// <returns></returns>
        private IEnumerator Coroutine_LoadGameSongs()
        {
#if BEATSABER
            yield return new WaitUntil(() => !SongCore.Loader.AreSongsLoading && SongCore.Loader.CustomLevels.Count > 0);

            foreach (var l_Current in SongCore.Loader.CustomLevels)
            {
                if (!(l_Current.Value.previewMediaData is FileSystemPreviewMediaData l_FileSystemPreviewMediaData))
                    continue;

                var l_PreviewAudioClipPath = PreviewMediaDataAccess.GetPreviewAudioClipPath(l_FileSystemPreviewMediaData);
                if (string.IsNullOrEmpty(l_PreviewAudioClipPath))
                    continue;

                var l_Extension = Path.GetExtension(l_PreviewAudioClipPath).ToLower();
                if (l_Extension != ".egg" && l_Extension != ".ogg")
                    continue;

                m_Musics.Add(new Music(
                    this,
                    l_PreviewAudioClipPath,
                    PreviewMediaDataAccess.GetCoverSpritePath(l_FileSystemPreviewMediaData),
                    l_Current.Value.songName,
                    l_Current.Value.songAuthorName
                ));
            }

            Shuffle();
#else
#error Missing game implementation
#endif

            m_IsLoading = false;
        }

        private static class PreviewMediaDataAccess
        {
            private const BindingFlags k_BindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

            private static bool s_Initialized;
            private static FieldInfo s_PreviewAudioClipPathField;
            private static PropertyInfo s_PreviewAudioClipPathProperty;
            private static FieldInfo s_CoverSpritePathField;
            private static PropertyInfo s_CoverSpritePathProperty;

            internal static string GetPreviewAudioClipPath(FileSystemPreviewMediaData p_Data)
            {
                if (!TryInit(p_Data))
                    return null;

                if (TryGetValue(p_Data, s_PreviewAudioClipPathField, s_PreviewAudioClipPathProperty, out string l_Value))
                    return l_Value;

                return null;
            }

            internal static string GetCoverSpritePath(FileSystemPreviewMediaData p_Data)
            {
                if (!TryInit(p_Data))
                    return null;

                if (TryGetValue(p_Data, s_CoverSpritePathField, s_CoverSpritePathProperty, out string l_Value))
                    return l_Value;

                return null;
            }

            private static bool TryInit(FileSystemPreviewMediaData p_Data)
            {
                if (s_Initialized || p_Data == null)
                    return s_Initialized;

                var l_Type = p_Data.GetType();
                s_PreviewAudioClipPathField    = FindField(l_Type, "_previewAudioClipPath", "previewAudioClipPath");
                s_PreviewAudioClipPathProperty = FindProperty(l_Type, "previewAudioClipPath");
                s_CoverSpritePathField         = FindField(l_Type, "_coverSpritePath", "coverSpritePath");
                s_CoverSpritePathProperty      = FindProperty(l_Type, "coverSpritePath");

                s_Initialized = true;
                return true;
            }

            private static FieldInfo FindField(Type p_Type, params string[] p_Names)
            {
                foreach (var l_Name in p_Names)
                {
                    var l_Field = p_Type.GetField(l_Name, k_BindingFlags);
                    if (l_Field != null)
                        return l_Field;
                }

                return null;
            }

            private static PropertyInfo FindProperty(Type p_Type, params string[] p_Names)
            {
                foreach (var l_Name in p_Names)
                {
                    var l_Property = p_Type.GetProperty(l_Name, k_BindingFlags);
                    if (l_Property != null)
                        return l_Property;
                }

                return null;
            }

            private static bool TryGetValue<T>(FileSystemPreviewMediaData p_Data, FieldInfo p_Field, PropertyInfo p_Property, out T p_Value)
            {
                p_Value = default;

                if (p_Field != null)
                {
                    var l_Value = p_Field.GetValue(p_Data);
                    if (l_Value is T l_Cast)
                    {
                        p_Value = l_Cast;
                        return true;
                    }
                }

                if (p_Property != null)
                {
                    var l_Value = p_Property.GetValue(p_Data);
                    if (l_Value is T l_Cast)
                    {
                        p_Value = l_Cast;
                        return true;
                    }
                }

                return false;
            }
        }
    }
}
