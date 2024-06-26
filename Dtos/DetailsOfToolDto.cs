namespace Graduation_Project.Dtos
{
    public class DetailsOfToolDto
    { 
    public int ToolId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string RentTime { get; set; }
    public string College { get; set; }
    public string University { get; set; }
    public string Acadmicyear { get; set; }
    public int Price { get; set; }
    public string Department { get; set; }
    public List<string> Photos { get; set; }
     public userData user { get; set; }

}
    public class userData
    {
        public string  FirstName{ get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string College { get; set; }

    }
}
