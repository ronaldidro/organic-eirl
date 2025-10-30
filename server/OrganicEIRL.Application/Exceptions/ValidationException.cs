﻿using FluentValidation.Results;

namespace OrganicEIRL.Application.Exceptions;

public class ValidationException : Exception
{
  public ValidationException() : base("Se han producido uno o más errores de validación.")
  {
    Errors = new Dictionary<string, string[]>();
  }

  public ValidationException(IEnumerable<ValidationFailure> failures) : this()
  {
    Errors = failures
        .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
        .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
  }

  public IDictionary<string, string[]> Errors { get; }
}