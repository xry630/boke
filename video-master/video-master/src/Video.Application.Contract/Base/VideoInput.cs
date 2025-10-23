using System;

namespace Video.Application.Contract.Base
{
    public class VideoInput : PagedRequestDto
    {
        /// <summary>
        /// 关键字
        /// </summary>
        public string? Keywords { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }
    }
}