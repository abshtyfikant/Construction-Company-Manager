﻿namespace WebApi.Authentication
{
    public record LoginRequest(
       string Email,
       string Password
    );
}
