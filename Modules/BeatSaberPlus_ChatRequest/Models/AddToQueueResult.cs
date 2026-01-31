namespace BeatSaberPlus_ChatRequest.Models
{
    /// <summary>
    /// Add to queue result
    /// </summary>
    public class AddToQueueResult
    {
        public EAddToQueueResult    Result;
        public string               BSRKeyOrHash;
        public SongEntry            FoundOrCreatedSongEntry;
        public RequesterRateLimit   RateLimit;
        public string               FilterError;

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="result">Result code</param>
        /// <param name="bsrKey">Transformed BSR key or level hash</param>
        public AddToQueueResult(EAddToQueueResult result, string bsrKey)
        {
            Result          = result;
            BSRKeyOrHash    = bsrKey;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="result">Result code</param>
        /// <param name="bsrKeyOrHash">Transformed BSR key or level hash</param>
        /// <param name="foundOrCreatedSongEntry">Found or created song entry</param>
        public AddToQueueResult(EAddToQueueResult result, string bsrKeyOrHash, SongEntry foundOrCreatedSongEntry)
        {
            Result                  = result;
            BSRKeyOrHash            = bsrKeyOrHash;
            FoundOrCreatedSongEntry = foundOrCreatedSongEntry;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="result">Result code</param>
        /// <param name="bsrKeyOrHash">Transformed BSR key or level hash</param>
        /// <param name="foundOrCreatedSongEntry">Found or created song entry</param>
        /// <param name="filterError">Error from the filter</param>
        public AddToQueueResult(EAddToQueueResult result, string bsrKeyOrHash, SongEntry foundOrCreatedSongEntry, string filterError)
        {
            Result                  = result;
            BSRKeyOrHash            = bsrKeyOrHash;
            FoundOrCreatedSongEntry = foundOrCreatedSongEntry;
            FilterError             = filterError;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="result">Result code</param>
        /// <param name="bsrKeyOrHash">Transformed BSR key or level hash</param>
        /// <param name="rateLimit">Requester rate limit</param>
        public AddToQueueResult(EAddToQueueResult result, string bsrKeyOrHash, RequesterRateLimit rateLimit)
        {
            Result          = result;
            BSRKeyOrHash    = bsrKeyOrHash;
            RateLimit       = rateLimit;
        }

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Get readable error result
        /// </summary>
        /// <returns></returns>
        public string GetFormattedError()
        {
            switch (Result)
            {
                case EAddToQueueResult.OK:
                    return string.Empty;
                case EAddToQueueResult.QueueClosed:
                    return "Черга закрита";
                case EAddToQueueResult.MapBanned:
                    return "Мапа заборонена";
                case EAddToQueueResult.RequesterBanned:
                    return "Тобі заборонено запитувати мапи!";
                case EAddToQueueResult.MapperBanned:
                    return "Мапер заборонений";
                case EAddToQueueResult.AlreadyRequestedThisSession:
                    return "Вже запитано в цій сесії";
                case EAddToQueueResult.AlreadyInQueue:
                    return "Вже в черзі запитів";
                case EAddToQueueResult.RequestLimit:
                    return "Ліміт запитів вичерпано!";
                case EAddToQueueResult.NotFound:
                    return "Мапу не знайдено!";
                case EAddToQueueResult.FilterError:
                    return FilterError;
            }

            return "<невідомо>";
        }
    }
}
