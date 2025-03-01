namespace library_management_api.Models.Dto;

public record AddBookRequestDto(string? Title, string? Genre, int? PublicationYear, Guid? AuthorId);