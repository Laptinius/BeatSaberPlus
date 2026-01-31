# Build (стан робочих модулів)

## Останній запуск (Release)
Збірка виконувалась окремо по модулях (послідовно), щоб швидко локалізувати проблеми.

### Успішно зібрались і скопійовані в Beat Saber `Plugins`
- `ChatPlexSDK_BS`
- `Modules/BeatSaberPlus_ChatEmoteRain`
- `Modules/BeatSaberPlus_ChatRequest`
- `Modules/BeatSaberPlus_Chat`
- `Modules/BeatSaberPlus_GameTweaker`
- `Modules/BeatSaberPlus_NoteTweaker`
- `Modules/BeatSaberPlus_SongChartVisualizer`
- `Modules/BeatSaberPlus_ChatIntegrations`
- `Modules/BeatSaberPlus_MenuMusic`
- `Modules/BeatSaberPlus_SongOverlay`
- `Samples/BeatSaberPlus_ModuleTemplate`

### Попередження (не блокують білд)
- `MSB3101` про `AssemblyReference.cache` (Access denied) у деяких проєктах.
- `CS0618` у `MenuMusic/Data/Music.cs` (deprecated `UnityWebRequest.isNetworkError` / `isHttpError`).

### Примітки
- `PrivateModules/BeatSaberPlus_CustomEdition` прибрано з solution: `.csproj` відсутній на диску.

## Команда (ручний запуск)
```powershell
& 'C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe' Modules\BeatSaberPlus_ChatIntegrations\BeatSaberPlus_ChatIntegrations.csproj /t:Build /p:Configuration=Release /m:1 /v:minimal
```

## Скрипт для запуску
- `build.ps1` / `build.cmd` — запуск збірки; логи в `build\logs\`.
