﻿namespace PrototypeBackend.Entities;

public abstract class BaseEntity
{
    public Status Status { get; set; } = Status.Active;
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime Updated { get; set; } = DateTime.UtcNow;
}

public enum Status
{
    Active = 1,
    Disabled = 2,
    Deleted = 3,
    Revoked = 4,
}