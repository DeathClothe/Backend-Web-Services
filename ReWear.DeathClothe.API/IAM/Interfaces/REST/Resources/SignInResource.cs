﻿namespace ReWear.DeathClothe.API.IAM.Interfaces.REST.Resources;

public record SignInResource(
    string Email,
    string Password
);