using Graduation_Project.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Graduation_Project.Entities
{
    public class Tool
    {
        [Key]
        public int ToolId { get; set; }
#pragma warning disable CS8618 // Non-nullable property 'Name' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public string Name { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'Name' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'Description' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public string Description { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'Description' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'RentTime' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public string RentTime { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'RentTime' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public int Price { get; set; }
#pragma warning disable CS8618 // Non-nullable property 'Category' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public string Category { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'Category' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.

#pragma warning disable CS8618 // Non-nullable property 'College' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public string College { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'College' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'Acadmicyear' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public string Acadmicyear { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'Acadmicyear' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'University' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public string University { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'University' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.

        [ForeignKey("User")]
        public int UserId { get; set; }
#pragma warning disable CS8618 // Non-nullable property 'User' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public User User { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'User' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'FavoriteTool' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public List<FavoriteTool> FavoriteTool { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'FavoriteTool' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.

#pragma warning disable CS8618 // Non-nullable property 'ToolPhoto' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public List<ToolPhoto> ToolPhoto { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'ToolPhoto' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
    }
}
