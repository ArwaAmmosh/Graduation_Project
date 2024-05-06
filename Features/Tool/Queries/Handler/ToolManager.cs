

public class ToolManager
{
    private readonly UNITOOLDbContext _context;

    public ToolManager(UNITOOLDbContext context)
    {
        _context = context;
    }

    // Method to retrieve all tools
    public async Task<List<Tool>> GetAllToolsAsync()
    {
        return await _context.Tools.ToListAsync();
    }

    // Method to retrieve a tool by ID
    public async Task<Tool> GetToolByIdAsync(int id)
    {
        return await _context.Tools.FindAsync(id);
    }

    // Method to add a new tool
    public async Task AddToolAsync(Tool tool)
    {
        _context.Tools.Add(tool);
        await _context.SaveChangesAsync();
    }

    // Method to update an existing tool
    public async Task UpdateToolAsync(int id, Tool updatedTool)
    {
        var existingTool = await _context.Tools.FindAsync(id);
        if (existingTool == null)
        {
            // Handle the case where the tool with the given ID doesn't exist
            return;
        }

        // Update properties of the existing tool
        existingTool.Name = updatedTool.Name;
        existingTool.Description = updatedTool.Description;
        existingTool.RentTime = updatedTool.RentTime;
        existingTool.Price = updatedTool.Price;
        existingTool.College = updatedTool.College;
        existingTool.University = updatedTool.University;
        existingTool.Category = updatedTool.Category;

        await _context.SaveChangesAsync();
    }

    // Method to delete a tool by ID
    public async Task DeleteToolAsync(int id)
    {
        var tool = await _context.Tools.FindAsync(id);
        if (tool == null)
        {
            // Handle the case where the tool with the given ID doesn't exist
            return;
        }

        _context.Tools.Remove(tool);
        await _context.SaveChangesAsync();
    }
}