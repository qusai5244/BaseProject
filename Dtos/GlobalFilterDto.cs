﻿namespace BaseProject.Dtos
{
    public class GlobalFilterDto
    {
        public string Search { get; set; } = string.Empty;
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string fillter { get; set; } = string.Empty;
        public string SortOrder { get; set; } = string.Empty;
    }
}
