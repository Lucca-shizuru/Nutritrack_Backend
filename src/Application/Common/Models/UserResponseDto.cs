namespace NutriTrack.src.Application.Common.Models
{
    public record UserResponseDto(
        Guid Id,
        string Name,
        string Email,
        string Token 
    );
    
}
