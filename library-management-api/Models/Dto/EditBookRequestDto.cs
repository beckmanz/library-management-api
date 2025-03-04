namespace library_management_api.Models.Dto;

public record EditBookRequestDto(Guid Id, string? Title, string? Genre, int? PublicationYear, string? AuthorId);