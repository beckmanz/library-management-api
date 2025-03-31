namespace library_management_api.Models.Dto;

public class EditLibraryRequestDto
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? NewPassword { get; set; }
}