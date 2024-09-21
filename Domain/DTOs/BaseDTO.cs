namespace Domain.DTOs;
public class BaseAddDTO{
  public DateTime CreatedAt { get; set; }

  public DateTime UpdatedAt { get; set; }

  public string Status { get; set; }
}

public class BaseUpdateDTO{
  public Guid Id { get; set; }

  public DateTime CreatedAt { get; set; }

  public DateTime UpdatedAt { get; set; }

  public string Status { get; set; }
}
