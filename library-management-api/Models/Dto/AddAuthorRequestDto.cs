using System.ComponentModel.DataAnnotations;

namespace library_management_api.Models.Dto;

public class AddAuthorRequestDto
{
    [Required(ErrorMessage = "Nome é obrigatorio!!")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Nacionalidade é obrigatorio!!")]
    public string Nacionality { get; set; }
}