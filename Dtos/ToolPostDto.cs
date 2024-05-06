namespace Graduation_Project.Dtos
{
    public class ToolPostDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string RentTime { get; set; }
        public string College { get; set; }
        public string University { get; set; }
        public int Price { get; set; }
        public string Category { get; set; }
        public string Acadmicyear { get; set; }
        public string Department { get; set; }
        public IFormFile ToolImages1 { get; set; }
        public IFormFile ToolImages2 { get; set; }
        public IFormFile ToolImages3 { get; set; }
        public IFormFile ToolImages4 { get; set; }
    }
}
