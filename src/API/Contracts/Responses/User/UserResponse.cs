namespace API.Contracts.Responses.User;

public sealed record UserResponse(
    string Name,
    int Age,
    string Sex,
    int Height
);