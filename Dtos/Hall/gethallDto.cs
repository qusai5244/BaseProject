namespace BaseProject.Dtos.Hall
{
    public class gethallDto
    {
        public int Id { get; set; }
        public string HName { get; set; } = string.Empty;
        public int capcity { get; set; }
        public int cinema_id { get; set; }
        public string CName { get; set; } = string.Empty;
        public string CLocation { get; set; } = string.Empty;
    }
}
