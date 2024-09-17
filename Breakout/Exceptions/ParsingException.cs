namespace Breakout.Exceptions;
using System;

/// <summary>
/// Thrown if the level configuration being parsed is invalid.
/// </summary>
public class ParsingException : Exception {
    public ParsingException(string message) : base(message) { }
    public ParsingException(string message, Exception innerException) : base(message, innerException) { }
}
