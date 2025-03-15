using Vital.Business.Shared.DomainObjects.AutoTestSource;

namespace Vital.Business.Shared.Filters
{
    public class StageAnnouncementsFilter : BaseFilter<StageAnnouncement>
    {
        /// <summary>
        /// Gets or sets the StageAnnouncementId.
        /// </summary>
        public int StageAnnouncementId { get; set; }
        
        /// <summary>
        /// Gets or sets the Key.
        /// </summary>
        public string Key { get; set; }
        
        /// <summary>
        /// Gets or sets the Text.
        /// </summary>
        public string Text { get; set; }
        
        /// <summary>
        /// Gets or sets the AudioPath.
        /// </summary>
        public string AudioPath { get; set; }
        
    }
}